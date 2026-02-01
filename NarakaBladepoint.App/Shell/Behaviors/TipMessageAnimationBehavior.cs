using System.ComponentModel;
using System.Windows;
using System.Windows.Media.Animation;
using Microsoft.Xaml.Behaviors;

namespace NarakaBladepoint.App.Shell.Behaviors
{
    /// <summary>
    /// 提示消息动画 Behavior
    ///
    /// 用途：监听 ViewModel 中的 TipMessage 属性变化，
    /// 当属性变化时自动触发完整的动画序列：
    /// 1. 消息从底部向上移动到顶部 (0s - 0.5s)
    /// 2. 消息显示 (0.5s - 1.0s)
    /// 3. 消息前景色淡出 (1.0s - 1.5s)
    ///
    /// 内存安全：使用 WeakEventManager 弱引用事件管理
    ///
    /// 最小化使用方式（推荐）：
    /// <![CDATA[
    /// <appBehaviors:TipMessageAnimationBehavior
    ///     StoryboardKey="TipMessageAnimationStoryboard" />
    /// ]]>
    /// </summary>
    public class TipMessageAnimationBehavior : Behavior<FrameworkElement>
    {
        #region Dependency Properties

        /// <summary>
        /// 完整动画序列的 Storyboard Key
        /// </summary>
        public static readonly DependencyProperty StoryboardKeyProperty =
            DependencyProperty.Register(
                nameof(StoryboardKey),
                typeof(string),
                typeof(TipMessageAnimationBehavior),
                new PropertyMetadata(null)
            );

        public string StoryboardKey
        {
            get => (string)GetValue(StoryboardKeyProperty);
            set => SetValue(StoryboardKeyProperty, value);
        }

        /// <summary>
        /// 提示消息内容绑定源属性名
        /// </summary>
        public static readonly DependencyProperty TipMessagePropertyNameProperty =
            DependencyProperty.Register(
                nameof(TipMessagePropertyName),
                typeof(string),
                typeof(TipMessageAnimationBehavior),
                new PropertyMetadata("TipMessage")
            );

        public string TipMessagePropertyName
        {
            get => (string)GetValue(TipMessagePropertyNameProperty);
            set => SetValue(TipMessagePropertyNameProperty, value);
        }

        /// <summary>
        /// Border 控件名称（用于重置 TranslateTransform）
        /// </summary>
        public static readonly DependencyProperty TipBorderNameProperty =
            DependencyProperty.Register(
                nameof(TipBorderName),
                typeof(string),
                typeof(TipMessageAnimationBehavior),
                new PropertyMetadata("tipBorder")
            );

        public string TipBorderName
        {
            get => (string)GetValue(TipBorderNameProperty);
            set => SetValue(TipBorderNameProperty, value);
        }

        /// <summary>
        /// TextBlock 控件名称（用于重置 Opacity）
        /// </summary>
        public static readonly DependencyProperty TipTextblockNameProperty =
            DependencyProperty.Register(
                nameof(TipTextblockName),
                typeof(string),
                typeof(TipMessageAnimationBehavior),
                new PropertyMetadata("tipTextblock")
            );

        public string TipTextblockName
        {
            get => (string)GetValue(TipTextblockNameProperty);
            set => SetValue(TipTextblockNameProperty, value);
        }

        #endregion Dependency Properties

        #region Fields

        private Storyboard _storyboard;
        private Window _window;
        private bool _controlsInitialized = false;
        private INotifyPropertyChanged _viewModel;

        #endregion Fields

        #region Behavior Lifecycle

        protected override void OnAttached()
        {
            base.OnAttached();

            // 从 Window Resources 中获取 Storyboard
            if (AssociatedObject is Window window)
            {
                _window = window;
                _storyboard = window.Resources[StoryboardKey] as Storyboard;
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
            base.OnDetaching();
        }

        #endregion Behavior Lifecycle

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
            if (
                Application.Current?.Dispatcher != null
                && !Application.Current.Dispatcher.CheckAccess()
            )
            {
                Application.Current.Dispatcher.Invoke(() => OnViewModelPropertyChanged(sender, e));
                return;
            }

            if (e.PropertyName == TipMessagePropertyName)
            {
                TriggerAnimation();
            }
        }

        private void OnStoryboardCompleted(object sender, EventArgs e)
        {
            if (_window == null)
            {
                return;
            }

            var tipGrid = _window.FindName("tipGrid") as FrameworkElement;
            var tipBorder = _window.FindName(TipBorderName) as FrameworkElement;
            var tipTextblock = _window.FindName(TipTextblockName) as FrameworkElement;

            // 隐藏 tipGrid 并禁用点击测试
            if (tipGrid != null)
            {
                tipGrid.Visibility = Visibility.Collapsed;
                tipGrid.IsHitTestVisible = false;
            }

            // 完全重置所有控件状态，确保动画不会留下任何残留
            if (tipBorder != null)
            {
                // 停止所有动画
                _storyboard?.Stop(_window);

                // 重置 RenderTransform 和 Opacity
                tipBorder.RenderTransform = new System.Windows.Media.TranslateTransform { Y = 100 };
                tipBorder.Opacity = 0;
            }

            // 重置 TextBlock 的可见性和 Opacity
            if (tipTextblock != null)
            {
                tipTextblock.Opacity = 1;
                tipTextblock.Visibility = Visibility.Collapsed;
            }
        }

        #endregion Event Handlers

        #region Animation Logic

        /// <summary>
        /// 延迟初始化控件引用
        /// </summary>
        private void EnsureControlsInitialized()
        {
            if (_controlsInitialized || _window == null)
            {
                return;
            }

            var tipBorder = _window.FindName(TipBorderName);
            var tipTextblock = _window.FindName(TipTextblockName);

            if (tipBorder == null || tipTextblock == null)
            {
                return;
            }

            _controlsInitialized = true;
        }

        /// <summary>
        /// 触发完整的动画序列
        /// </summary>
        private void TriggerAnimation()
        {
            EnsureControlsInitialized();

            if (!_controlsInitialized || _window == null)
            {
                return;
            }

            var tipBorder = _window.FindName(TipBorderName) as FrameworkElement;
            var tipTextblock = _window.FindName(TipTextblockName) as FrameworkElement;
            var tipGrid = _window.FindName("tipGrid") as FrameworkElement;

            if (tipBorder == null || tipTextblock == null)
            {
                return;
            }

            // 显示 tipGrid 以便动画播放
            if (tipGrid != null)
            {
                tipGrid.Visibility = Visibility.Visible;
            }

            // 停止当前运行的动画
            _storyboard?.Stop();

            // 重置 Border 的 RenderTransform
            var style = tipBorder.TryFindResource("TipMessageBorderStyle") as System.Windows.Style;
            if (style != null)
            {
                tipBorder.Style = null;
                tipBorder.Style = style;
            }
            else
            {
                tipBorder.RenderTransform = new System.Windows.Media.TranslateTransform { Y = 100 };
            }

            // 使用弱引用监听 Storyboard.Completed
            if (_storyboard != null)
            {
                WeakEventManager<Timeline, EventArgs>.AddHandler(
                    _storyboard,
                    nameof(Timeline.Completed),
                    OnStoryboardCompleted
                );
            }

            // 启动动画
            _storyboard?.Begin();
        }

        #endregion Animation Logic
    }
}
