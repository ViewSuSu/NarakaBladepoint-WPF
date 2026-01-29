using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace NarakaBladepoint.Framework.UI.AttachedProperties
{
    public static class ControlHoverBehavior
    {
        // 是否启用悬停放大效果
        public static readonly DependencyProperty IsEnabledProperty =
            DependencyProperty.RegisterAttached(
                "IsEnabled",
                typeof(bool),
                typeof(ControlHoverBehavior),
                new PropertyMetadata(false, OnIsEnabledChanged)
            );

        // 放大倍数
        public static readonly DependencyProperty ZoomFactorProperty =
            DependencyProperty.RegisterAttached(
                "ZoomFactor",
                typeof(double),
                typeof(ControlHoverBehavior),
                new PropertyMetadata(1.2)
            );

        public static bool GetIsEnabled(DependencyObject obj) =>
            (bool)obj.GetValue(IsEnabledProperty);

        public static void SetIsEnabled(DependencyObject obj, bool value) =>
            obj.SetValue(IsEnabledProperty, value);

        public static double GetZoomFactor(DependencyObject obj) =>
            (double)obj.GetValue(ZoomFactorProperty);

        public static void SetZoomFactor(DependencyObject obj, double value) =>
            obj.SetValue(ZoomFactorProperty, value);

        private static void OnIsEnabledChanged(
            DependencyObject d,
            DependencyPropertyChangedEventArgs e
        )
        {
            if (d is FrameworkElement element)
            {
                ConfigureControl(element, (bool)e.NewValue);
            }
        }

        private static void ConfigureControl(FrameworkElement element, bool isEnabled)
        {
            if (isEnabled)
            {
                // 确保有缩放变换
                EnsureScaleTransform(element);

                // 使用弱事件监听鼠标进入和离开
                WeakEventManager<FrameworkElement, MouseEventArgs>.AddHandler(
                    element,
                    "MouseEnter",
                    OnMouseEnter
                );
                WeakEventManager<FrameworkElement, MouseEventArgs>.AddHandler(
                    element,
                    "MouseLeave",
                    OnMouseLeave
                );
            }
            else
            {
                // 移除弱事件监听
                WeakEventManager<FrameworkElement, MouseEventArgs>.RemoveHandler(
                    element,
                    "MouseEnter",
                    OnMouseEnter
                );
                WeakEventManager<FrameworkElement, MouseEventArgs>.RemoveHandler(
                    element,
                    "MouseLeave",
                    OnMouseLeave
                );

                // 恢复到原始大小
                ResetScaleTransform(element);
            }
        }

        private static void EnsureScaleTransform(FrameworkElement element)
        {
            if (element.RenderTransform is not ScaleTransform)
            {
                // 设置变换原点为中心
                element.RenderTransformOrigin = new Point(0.5, 0.5);

                // 创建缩放变换
                element.RenderTransform = new ScaleTransform(1, 1);
            }
        }

        private static void OnMouseEnter(object sender, MouseEventArgs e)
        {
            if (sender is FrameworkElement element && GetIsEnabled(element))
            {
                ApplyScaleAnimation(element, GetZoomFactor(element));
            }
        }

        private static void OnMouseLeave(object sender, MouseEventArgs e)
        {
            if (sender is FrameworkElement element && GetIsEnabled(element))
            {
                ApplyScaleAnimation(element, 1.0);
            }
        }

        private static void ApplyScaleAnimation(FrameworkElement element, double targetScale)
        {
            if (element.RenderTransform is ScaleTransform scaleTransform)
            {
                // 停止当前动画
                scaleTransform.BeginAnimation(ScaleTransform.ScaleXProperty, null);
                scaleTransform.BeginAnimation(ScaleTransform.ScaleYProperty, null);

                var animation = new DoubleAnimation
                {
                    To = targetScale,
                    Duration = TimeSpan.FromMilliseconds(1),
                    EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut },
                };

                scaleTransform.BeginAnimation(ScaleTransform.ScaleXProperty, animation);
                scaleTransform.BeginAnimation(ScaleTransform.ScaleYProperty, animation);
            }
        }

        private static void ResetScaleTransform(FrameworkElement element)
        {
            if (element.RenderTransform is ScaleTransform scaleTransform)
            {
                // 停止动画
                scaleTransform.BeginAnimation(ScaleTransform.ScaleXProperty, null);
                scaleTransform.BeginAnimation(ScaleTransform.ScaleYProperty, null);

                // 重置到原始大小
                scaleTransform.ScaleX = 1.0;
                scaleTransform.ScaleY = 1.0;
            }
        }
    }
}
