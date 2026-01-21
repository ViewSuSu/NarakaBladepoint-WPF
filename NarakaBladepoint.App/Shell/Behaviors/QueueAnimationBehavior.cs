using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media.Animation;
using Microsoft.Xaml.Behaviors;

namespace NarakaBladepoint.App.Shell.Behaviors
{
    /// <summary>
    /// 排队面板动画 Behavior
    ///
    /// 用途：监听 ViewModel 中的 IsQueuing 属性变化，
    /// 当属性变化时自动触发动画和计时逻辑：
    /// - IsQueuing = true: 从下往上滑出 + 淡入 + 启动计时
    /// - IsQueuing = false: 向下滑出 + 淡出 + 停止计时
    /// 
    /// 内存安全：使用 WeakEventManager 弱引用事件管理
    /// </summary>
    public class QueueAnimationBehavior : Behavior<FrameworkElement>
    {
        #region Dependency Properties

        public static readonly DependencyProperty ShowStoryboardKeyProperty =
            DependencyProperty.Register(
                nameof(ShowStoryboardKey),
                typeof(string),
                typeof(QueueAnimationBehavior),
                new PropertyMetadata("QueueShowStoryboard")
            );

        public string ShowStoryboardKey
        {
            get => (string)GetValue(ShowStoryboardKeyProperty);
            set => SetValue(ShowStoryboardKeyProperty, value);
        }

        public static readonly DependencyProperty HideStoryboardKeyProperty =
            DependencyProperty.Register(
                nameof(HideStoryboardKey),
                typeof(string),
                typeof(QueueAnimationBehavior),
                new PropertyMetadata("QueueHideStoryboard")
            );

        public string HideStoryboardKey
        {
            get => (string)GetValue(HideStoryboardKeyProperty);
            set => SetValue(HideStoryboardKeyProperty, value);
        }

        public static readonly DependencyProperty IsQueuingPropertyNameProperty =
            DependencyProperty.Register(
                nameof(IsQueuingPropertyName),
                typeof(string),
                typeof(QueueAnimationBehavior),
                new PropertyMetadata("IsQueuing")
            );

        public string IsQueuingPropertyName
        {
            get => (string)GetValue(IsQueuingPropertyNameProperty);
            set => SetValue(IsQueuingPropertyNameProperty, value);
        }

        public static readonly DependencyProperty QueueBorderNameProperty =
            DependencyProperty.Register(
                nameof(QueueBorderName),
                typeof(string),
                typeof(QueueAnimationBehavior),
                new PropertyMetadata("queueBorder")
            );

        public string QueueBorderName
        {
            get => (string)GetValue(QueueBorderNameProperty);
            set => SetValue(QueueBorderNameProperty, value);
        }

        #endregion

        #region Fields

        private Storyboard _showStoryboard;
        private Storyboard _hideStoryboard;
        private Window _window;
        private INotifyPropertyChanged _viewModel;
        private System.Timers.Timer _queueTimer;

        #endregion

        #region Behavior Lifecycle

        protected override void OnAttached()
        {
            base.OnAttached();

            if (AssociatedObject is Window window)
            {
                _window = window;
                _showStoryboard = window.Resources[ShowStoryboardKey] as Storyboard;
                _hideStoryboard = window.Resources[HideStoryboardKey] as Storyboard;
            }

            // DataContextChanged 不是标准的 RoutedEvent，需要手动管理
            // 但 Behavior 的 OnDetaching 会确保清理
            AssociatedObject.DataContextChanged += OnDataContextChanged;

            // 立即处理当前 DataContext
            SubscribeViewModel(AssociatedObject.DataContext as INotifyPropertyChanged);
        }

        protected override void OnDetaching()
        {
            AssociatedObject.DataContextChanged -= OnDataContextChanged;
            StopQueueTimer();
            base.OnDetaching();
        }

        #endregion

        #region Event Handlers

        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            SubscribeViewModel(e.NewValue as INotifyPropertyChanged);
        }

        private void SubscribeViewModel(INotifyPropertyChanged newViewModel)
        {
            _viewModel = newViewModel;

            if (_viewModel != null)
            {
                // 使用弱引用监听 PropertyChanged
                PropertyChangedEventManager.AddHandler(
                    _viewModel,
                    OnViewModelPropertyChanged,
                    string.Empty
                );
            }
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // 确保在 UI 线程上执行
            if (Application.Current?.Dispatcher != null && !Application.Current.Dispatcher.CheckAccess())
            {
                Application.Current.Dispatcher.Invoke(() => OnViewModelPropertyChanged(sender, e));
                return;
            }

            if (e.PropertyName == IsQueuingPropertyName)
            {
                TriggerAnimation();
            }
        }

        private void OnShowStoryboardCompleted(object sender, EventArgs e)
        {
            // 清理之前的隐藏回调（如果有）
        }

        private void OnHideStoryboardCompleted(object sender, EventArgs e)
        {
            if (_window == null)
            {
                return;
            }

            var queueGrid = _window.FindName("queue") as FrameworkElement;
            if (queueGrid != null)
            {
                queueGrid.Visibility = Visibility.Collapsed;
            }

            StopQueueTimer();
        }

        #endregion

        #region Animation Logic

        private void TriggerAnimation()
        {
            if (_window == null)
            {
                return;
            }

            var queueBorder = _window.FindName(QueueBorderName) as FrameworkElement;
            var queueGrid = _window.FindName("queue") as FrameworkElement;

            if (queueBorder == null)
            {
                return;
            }

            // 停止当前动画
            _showStoryboard?.Stop();
            _hideStoryboard?.Stop();

            // 重置 Border 的变换
            var style = queueBorder.TryFindResource("QueueBorderStyle") as System.Windows.Style;
            if (style != null)
            {
                queueBorder.Style = null;
                queueBorder.Style = style;
            }
            else
            {
                queueBorder.RenderTransform = new System.Windows.Media.TranslateTransform
                {
                    Y = 100,
                };
                queueBorder.Opacity = 0;
            }

            // 根据 IsQueuing 属性判断播放哪个动画
            if (_viewModel != null)
            {
                var isQueuing = (bool)
                    _viewModel
                        .GetType()
                        .GetProperty(IsQueuingPropertyName)
                        ?.GetValue(_viewModel, null);

                if (isQueuing)
                {
                    // 显示queue容器
                    if (queueGrid != null)
                    {
                        queueGrid.Visibility = Visibility.Visible;
                    }

                    // 启动排队计时
                    StartQueueTimer();

                    // 使用弱引用监听 Storyboard.Completed
                    if (_showStoryboard != null)
                    {
                        WeakEventManager<Timeline, EventArgs>.AddHandler(
                            _showStoryboard,
                            nameof(Timeline.Completed),
                            OnShowStoryboardCompleted
                        );
                    }

                    _showStoryboard?.Begin();
                }
                else
                {
                    // 使用弱引用监听 Storyboard.Completed
                    if (_hideStoryboard != null)
                    {
                        WeakEventManager<Timeline, EventArgs>.AddHandler(
                            _hideStoryboard,
                            nameof(Timeline.Completed),
                            OnHideStoryboardCompleted
                        );
                    }

                    _hideStoryboard?.Begin();
                }
            }
        }

        #endregion

        #region Timer Logic

        private void StartQueueTimer()
        {
            if (_viewModel == null)
            {
                return;
            }

            var queueTimeProperty = _viewModel.GetType().GetProperty("QueueTime");
            if (queueTimeProperty == null)
            {
                return;
            }

            // 重置计时
            queueTimeProperty.SetValue(_viewModel, 0);

            if (_queueTimer != null)
            {
                _queueTimer.Stop();
                _queueTimer.Dispose();
            }

            // 创建新的计时器
            _queueTimer = new System.Timers.Timer(1000);
            _queueTimer.Elapsed += (s, e) =>
            {
                if (_viewModel != null)
                {
                    var currentValue = (int)queueTimeProperty.GetValue(_viewModel, null);
                    queueTimeProperty.SetValue(_viewModel, currentValue + 1);
                }
            };
            _queueTimer.AutoReset = true;
            _queueTimer.Start();
        }

        private void StopQueueTimer()
        {
            if (_queueTimer != null)
            {
                _queueTimer.Stop();
                _queueTimer.Dispose();
                _queueTimer = null;
            }

            if (_viewModel != null)
            {
                var queueTimeProperty = _viewModel.GetType().GetProperty("QueueTime");
                queueTimeProperty?.SetValue(_viewModel, 0);
            }
        }

        #endregion
    }
}
