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
    /// 当属性变化时自动触发动画：
    /// - IsQueuing = true: 从下往上滑出 + 淡入
    /// - IsQueuing = false: 向下滑出 + 淡出
    /// </summary>
    public class QueueAnimationBehavior : Behavior<FrameworkElement>
    {
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

        private Storyboard _showStoryboard;
        private Storyboard _hideStoryboard;
        private Window _window;
        private INotifyPropertyChanged _viewModel;

        protected override void OnAttached()
        {
            base.OnAttached();

            if (AssociatedObject is Window window)
            {
                _window = window;
                _showStoryboard = window.Resources[ShowStoryboardKey] as Storyboard;
                _hideStoryboard = window.Resources[HideStoryboardKey] as Storyboard;
            }

            AssociatedObject.DataContextChanged += AssociatedObject_DataContextChanged;

            // 立即处理当前 DataContext
            if (AssociatedObject.DataContext is INotifyPropertyChanged vm)
            {
                _viewModel = vm;
                _viewModel.PropertyChanged += ViewModel_PropertyChanged;
            }
        }

        protected override void OnDetaching()
        {
            AssociatedObject.DataContextChanged -= AssociatedObject_DataContextChanged;

            if (_viewModel != null)
            {
                _viewModel.PropertyChanged -= ViewModel_PropertyChanged;
                _viewModel = null;
            }

            base.OnDetaching();
        }

        private void AssociatedObject_DataContextChanged(
            object sender,
            DependencyPropertyChangedEventArgs e
        )
        {
            if (_viewModel != null)
            {
                _viewModel.PropertyChanged -= ViewModel_PropertyChanged;
            }

            if (e.NewValue is INotifyPropertyChanged vm)
            {
                _viewModel = vm;
                _viewModel.PropertyChanged += ViewModel_PropertyChanged;
            }
            else
            {
                _viewModel = null;
            }
        }

        private void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // 确保在 UI 线程上执行，因为访问 DependencyProperty 也需要在 UI 线程上
            if (
                Application.Current?.Dispatcher != null
                && !Application.Current.Dispatcher.CheckAccess()
            )
            {
                Application.Current.Dispatcher.Invoke(() => ViewModel_PropertyChanged(sender, e));
                return;
            }

            if (e.PropertyName == IsQueuingPropertyName)
            {
                TriggerAnimation();
            }
        }

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

                    // 移除之前的完成回调
                    if (_showStoryboard != null)
                    {
                        _showStoryboard.Completed -= ShowStoryboard_Completed;
                        _showStoryboard.Completed += ShowStoryboard_Completed;
                    }

                    _showStoryboard?.Begin();
                }
                else
                {
                    // 隐藏动画完成后的回调 - 此时才停止计时
                    if (_hideStoryboard != null)
                    {
                        _hideStoryboard.Completed -= HideStoryboard_Completed;
                        _hideStoryboard.Completed += HideStoryboard_Completed;
                    }

                    _hideStoryboard?.Begin();
                }
            }
        }

        /// <summary>
        /// 显示动画完成后的回调 - 清理之前的隐藏回调
        /// </summary>
        private void ShowStoryboard_Completed(object sender, EventArgs e)
        {
            if (_hideStoryboard != null)
            {
                _hideStoryboard.Completed -= HideStoryboard_Completed;
            }
        }

        /// <summary>
        /// 隐藏动画完成后的回调 - 隐藏排队容器并停止计时
        /// </summary>
        private void HideStoryboard_Completed(object sender, EventArgs e)
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
        }
    }
}
