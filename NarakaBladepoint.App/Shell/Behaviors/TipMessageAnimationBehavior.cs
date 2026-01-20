using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    /// 特点：
    /// - 每次触发时，如有动画运行中，会先停止并重置
    /// - 所有属性都有合理的默认值，使用时无需额外配置
    /// - 自动发现并监听 TipMessage 属性变化
    ///
    /// 最小化使用方式（推荐）：
    /// <![CDATA[
    /// <appBehaviors:TipMessageAnimationBehavior
    ///     StoryboardKey="TipMessageAnimationStoryboard" />
    /// ]]>
    ///
    /// 自定义使用方式（可选）：
    /// <![CDATA[
    /// <appBehaviors:TipMessageAnimationBehavior
    ///     StoryboardKey="MyStoryboard"
    ///     TipBorderName="myBorder"
    ///     TipTextblockName="myTextblock"
    ///     TipMessagePropertyName="CustomMessage" />
    /// ]]>
    /// </summary>
    public class TipMessageAnimationBehavior : Behavior<FrameworkElement>
    {
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

        private Storyboard _storyboard;
        private Window _window;
        private bool _controlsInitialized = false;
        private INotifyPropertyChanged _viewModel;

        protected override void OnAttached()
        {
            base.OnAttached();

            // 从 Window Resources 中获取 Storyboard
            if (AssociatedObject is Window window)
            {
                _window = window;
                _storyboard = window.Resources[StoryboardKey] as Storyboard;
            }

            // 订阅 DataContext 变化
            AssociatedObject.DataContextChanged += AssociatedObject_DataContextChanged;

            // 在附加时立即处理当前的 DataContext
            // 因为 DataContextChanged 事件可能不会被触发（如果 DataContext 已经被设置）
            if (AssociatedObject.DataContext is INotifyPropertyChanged vm)
            {
                vm.PropertyChanged += ViewModel_PropertyChanged;
            }
        }

        protected override void OnDetaching()
        {
            AssociatedObject.DataContextChanged -= AssociatedObject_DataContextChanged;

            // 取消订阅 ViewModel 的 PropertyChanged 事件
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
            // 取消旧 ViewModel 的订阅
            if (_viewModel != null)
            {
                _viewModel.PropertyChanged -= ViewModel_PropertyChanged;
            }

            // 订阅新 ViewModel 的 PropertyChanged 事件
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
            if (Application.Current?.Dispatcher != null && !Application.Current.Dispatcher.CheckAccess())
            {
                Application.Current.Dispatcher.Invoke(() => ViewModel_PropertyChanged(sender, e));
                return;
            }

            // 当 TipMessage 属性变化时，触发动画
            if (e.PropertyName == TipMessagePropertyName)
            {
                TriggerAnimation();
            }
        }

        /// <summary>
        /// 延迟初始化控件引用
        /// 在第一次需要时才查找控件，确保控件树已初始化
        /// </summary>
        private void EnsureControlsInitialized()
        {
            if (_controlsInitialized || _window == null)
            {
                return;
            }

            var tipBorder = _window.FindName(TipBorderName);
            var tipTextblock = _window.FindName(TipTextblockName);

            // 如果还是找不到，说明控件树还没准备好，稍后再试
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
            // 确保控件已初始化
            EnsureControlsInitialized();

            if (!_controlsInitialized || _window == null)
            {
                return;
            }

            // 获取控件引用
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

            // 通过重新应用样式来重置 Border 的 RenderTransform
            // 这样可以避免直接修改只读的 RenderTransform 属性
            var style = tipBorder.TryFindResource("TipMessageBorderStyle") as System.Windows.Style;

            if (style != null)
            {
                tipBorder.Style = null; // 清除样式
                tipBorder.Style = style; // 重新应用样式，重置 RenderTransform
            }
            else
            {
                // 如果找不到样式，手动重置
                // 创建一个新的 TranslateTransform 替换旧的
                tipBorder.RenderTransform = new System.Windows.Media.TranslateTransform { Y = 100 };
            }

            // 重置 TextBlock 的不透明度为完全透明（动画会从0淡入到1）
            if (tipTextblock != null)
            {
                tipTextblock.Opacity = 0.0;
            }

            // 在动画完成后隐藏 tipGrid，避免占用布局空间
            if (_storyboard != null)
            {
                _storyboard.Completed -= Storyboard_Completed;
                _storyboard.Completed += Storyboard_Completed;
            }

            // 启动完整的动画序列
            _storyboard?.Begin();
        }

        /// <summary>
        /// 动画完成后的回调 - 隐藏提示消息容器
        /// </summary>
        private void Storyboard_Completed(object sender, EventArgs e)
        {
            if (_window == null)
            {
                return;
            }

            var tipGrid = _window.FindName("tipGrid") as FrameworkElement;
            if (tipGrid != null)
            {
                tipGrid.Visibility = Visibility.Collapsed;
            }
        }
    }
}
