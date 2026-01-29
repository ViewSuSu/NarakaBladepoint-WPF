using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace NarakaBladepoint.Framework.UI.AttachedProperties
{
    public static class ClipToBoundsBehavior
    {
        public static readonly DependencyProperty IsEnabledProperty =
            DependencyProperty.RegisterAttached(
                "IsEnabled",
                typeof(bool),
                typeof(ClipToBoundsBehavior),
                new PropertyMetadata(false, OnIsEnabledChanged)
            );

        public static bool GetIsEnabled(DependencyObject obj) =>
            (bool)obj.GetValue(IsEnabledProperty);

        public static void SetIsEnabled(DependencyObject obj, bool value) =>
            obj.SetValue(IsEnabledProperty, value);

        private static void OnIsEnabledChanged(
            DependencyObject d,
            DependencyPropertyChangedEventArgs e
        )
        {
            if (d is FrameworkElement element)
            {
                if ((bool)e.NewValue)
                {
                    // 创建裁剪矩形
                    var clipGeometry = new RectangleGeometry();
                    element.Clip = clipGeometry;

                    // 使用弱事件监听大小变化
                    WeakEventManager<FrameworkElement, SizeChangedEventArgs>.AddHandler(
                        element,
                        "SizeChanged",
                        OnElementSizeChanged
                    );

                    UpdateClip(element);
                }
                else
                {
                    // 移除弱事件监听
                    WeakEventManager<FrameworkElement, SizeChangedEventArgs>.RemoveHandler(
                        element,
                        "SizeChanged",
                        OnElementSizeChanged
                    );

                    element.Clip = null;
                }
            }
        }

        private static void OnElementSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (sender is FrameworkElement element && GetIsEnabled(element))
            {
                UpdateClip(element);
            }
        }

        private static void UpdateClip(FrameworkElement element)
        {
            if (element.Clip is RectangleGeometry rectGeometry)
            {
                rectGeometry.Rect = new Rect(0, 0, element.ActualWidth, element.ActualHeight);

                // 如果需要圆角，可以从父容器继承
                if (element is Border border && border.CornerRadius != new CornerRadius(0))
                {
                    rectGeometry.RadiusX = border.CornerRadius.TopLeft;
                    rectGeometry.RadiusY = border.CornerRadius.TopLeft;
                }
                else if (
                    element.TemplatedParent is Border templatedBorder
                    && templatedBorder.CornerRadius != new CornerRadius(0)
                )
                {
                    // 如果是在模板中的 Border
                    rectGeometry.RadiusX = templatedBorder.CornerRadius.TopLeft;
                    rectGeometry.RadiusY = templatedBorder.CornerRadius.TopLeft;
                }
            }
        }
    }
}
