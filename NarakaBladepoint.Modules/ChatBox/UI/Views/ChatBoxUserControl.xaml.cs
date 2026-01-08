using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace NarakaBladepoint.Modules.ChatBox.UI.Views
{
    /// <summary>
    /// ChatBoxUserControl.xaml 的交互逻辑
    /// </summary>
    public partial class ChatBoxUserControl : UserControlBase
    {
        private CancellationTokenSource _activityCts;
        private Task _activityMonitorTask;
        private DateTime _lastActivityTime;
        private bool _isFadingOut = false;
        private readonly object _activityLock = new object();

        private const double FadeOutDuration = 1.5;
        private const double InactivityTimeout = 3.0;
        private const int CheckInterval = 100;

        public ChatBoxUserControl()
        {
            InitializeComponent();
            SetupWeakEventHandlers();
            this.IsVisibleChanged += ChatBoxUserControl_IsVisibleChanged;
        }

        private void SetupWeakEventHandlers()
        {
            // 使用 WeakEventManager 注册事件
            WeakEventManager<UIElement, RoutedEventArgs>.AddHandler(
                inputTextBox,
                "GotFocus",
                OnAnyActivity
            );
            WeakEventManager<UIElement, RoutedEventArgs>.AddHandler(
                inputTextBox,
                "LostFocus",
                OnAnyActivity
            );
            WeakEventManager<TextBox, TextChangedEventArgs>.AddHandler(
                inputTextBox,
                "TextChanged",
                OnAnyActivity
            );
            WeakEventManager<UIElement, KeyEventArgs>.AddHandler(
                inputTextBox,
                "KeyDown",
                OnAnyActivity
            );
            WeakEventManager<UIElement, MouseButtonEventArgs>.AddHandler(
                inputTextBox,
                "PreviewMouseDown",
                OnAnyActivity
            );
            WeakEventManager<UIElement, MouseEventArgs>.AddHandler(
                inputTextBox,
                "MouseEnter",
                OnAnyActivity
            );
            WeakEventManager<UIElement, MouseEventArgs>.AddHandler(
                inputTextBox,
                "MouseLeave",
                OnAnyActivity
            );

            WeakEventManager<UIElement, MouseEventArgs>.AddHandler(
                listbox,
                "MouseEnter",
                OnAnyActivity
            );
            WeakEventManager<UIElement, MouseEventArgs>.AddHandler(
                listbox,
                "MouseLeave",
                OnAnyActivity
            );
            WeakEventManager<UIElement, MouseWheelEventArgs>.AddHandler(
                listbox,
                "PreviewMouseWheel",
                OnAnyActivity
            );
            WeakEventManager<UIElement, MouseButtonEventArgs>.AddHandler(
                listbox,
                "PreviewMouseDown",
                OnAnyActivity
            );
        }

        private void ChatBoxUserControl_IsVisibleChanged(
            object sender,
            DependencyPropertyChangedEventArgs e
        )
        {
            if (e.NewValue is bool isVisible)
            {
                if (isVisible)
                {
                    inputTextBox.Focus();
                    RecordActivity();
                    StartActivityMonitor();
                }
                else
                {
                    StopActivityMonitor();
                }
            }
        }

        #region 后台活动监控
        private void StartActivityMonitor()
        {
            StopActivityMonitor();

            _activityCts = new CancellationTokenSource();
            _activityMonitorTask = Task.Run(
                async () =>
                {
                    try
                    {
                        while (!_activityCts.Token.IsCancellationRequested)
                        {
                            await Task.Delay(CheckInterval, _activityCts.Token);
                            CheckForInactivity();
                        }
                    }
                    catch (TaskCanceledException)
                    {
                        // 正常取消
                    }
                },
                _activityCts.Token
            );
        }

        private void StopActivityMonitor()
        {
            _activityCts?.Cancel();
            _activityMonitorTask = null;
            _activityCts = null;
        }

        private void RecordActivity()
        {
            lock (_activityLock)
            {
                _lastActivityTime = DateTime.Now;
            }

            CheckForRecovery();
        }

        private void CheckForInactivity()
        {
            if (Dispatcher?.CheckAccess() == true)
            {
                CheckForInactivityCore();
            }
            else
            {
                Dispatcher?.Invoke(CheckForInactivityCore);
            }
        }

        private void CheckForInactivityCore()
        {
            if (
                !this.IsVisible
                || inputTextBox.IsFocused
                || inputTextBox.IsMouseOver
                || listbox.IsMouseOver
            )
                return;

            double inactiveSeconds;
            lock (_activityLock)
            {
                inactiveSeconds = (DateTime.Now - _lastActivityTime).TotalSeconds;
            }

            if (inactiveSeconds >= InactivityTimeout && !_isFadingOut)
            {
                StartFadeOutAnimation();
            }
        }

        private void CheckForRecovery()
        {
            if (Dispatcher?.CheckAccess() == true)
            {
                CheckForRecoveryCore();
            }
            else
            {
                Dispatcher?.Invoke(CheckForRecoveryCore);
            }
        }

        private void CheckForRecoveryCore()
        {
            if (_isFadingOut || listbox.Opacity < 1.0)
            {
                StopFadeOut();
            }
        }
        #endregion

        #region UI动画控制
        private void OnAnyActivity(object sender, EventArgs e)
        {
            RecordActivity();
        }

        private void StartFadeOutAnimation()
        {
            if (Dispatcher?.CheckAccess() == true)
            {
                StartFadeOutAnimationCore();
            }
            else
            {
                Dispatcher?.Invoke(StartFadeOutAnimationCore);
            }
        }

        private void StartFadeOutAnimationCore()
        {
            if (_isFadingOut)
                return;

            _isFadingOut = true;
            listbox.BeginAnimation(UIElement.OpacityProperty, null);

            DoubleAnimation fadeOut = new DoubleAnimation
            {
                From = 1.0,
                To = 0.3,
                Duration = TimeSpan.FromSeconds(FadeOutDuration),
                FillBehavior = FillBehavior.Stop,
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut },
            };

            fadeOut.Completed += (s, e) =>
            {
                _isFadingOut = false;
            };
            listbox.BeginAnimation(UIElement.OpacityProperty, fadeOut);
        }

        private void StopFadeOut()
        {
            if (Dispatcher?.CheckAccess() == true)
            {
                StopFadeOutCore();
            }
            else
            {
                Dispatcher?.Invoke(StopFadeOutCore);
            }
        }

        private void StopFadeOutCore()
        {
            _isFadingOut = false;
            listbox.BeginAnimation(UIElement.OpacityProperty, null);

            DoubleAnimation fadeIn = new DoubleAnimation
            {
                To = 1.0,
                Duration = TimeSpan.FromSeconds(0.3),
                FillBehavior = FillBehavior.Stop,
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut },
            };

            listbox.BeginAnimation(UIElement.OpacityProperty, fadeIn);
        }

        public void ActivateChatBox()
        {
            RecordActivity();
            StopFadeOut();
        }
        #endregion

        #region 清理资源 - 简化版本
        // 只需要清理后台任务，不需要注销事件
        private void CleanupResources()
        {
            StopActivityMonitor();
        }

        private void OnUserControlUnloaded(object sender, RoutedEventArgs e)
        {
            CleanupResources();
        }
        #endregion
    }
}
