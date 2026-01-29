using System.Windows;
using System.Windows.Input;

namespace NarakaBladepoint.Framework.UI.AttachedProperties
{
    /// <summary>
    /// 鼠标悬停高亮附加属性 (适用于所有 FrameworkElement)
    ///
    /// 未悬停时 Opacity 为 0.8，悬停时 Opacity 为 1.0 (实现“高亮”)
    /// </summary>
    public static class HoverHighlightAttachedProperty
    {
        public static readonly DependencyProperty EnableHoverHighlightProperty =
            DependencyProperty.RegisterAttached(
                "EnableHoverHighlight",
                typeof(bool),
                typeof(HoverHighlightAttachedProperty),
                new PropertyMetadata(false, OnEnableHoverHighlightChanged)
            );

        public static bool GetEnableHoverHighlight(DependencyObject obj) =>
            (bool)obj.GetValue(EnableHoverHighlightProperty);

        public static void SetEnableHoverHighlight(DependencyObject obj, bool value) =>
            obj.SetValue(EnableHoverHighlightProperty, value);

        private static void OnEnableHoverHighlightChanged(
            DependencyObject d,
            DependencyPropertyChangedEventArgs e
        )
        {
            if (d is FrameworkElement element)
            {
                // 先移除现有处理器
                WeakEventManager<UIElement, MouseEventArgs>.RemoveHandler(
                    element,
                    nameof(UIElement.MouseEnter),
                    OnMouseEnter
                );
                WeakEventManager<UIElement, MouseEventArgs>.RemoveHandler(
                    element,
                    nameof(UIElement.MouseLeave),
                    OnMouseLeave
                );

                if ((bool)e.NewValue)
                {
                    // 初始化 Opacity 为 0.8
                    element.Opacity = 0.8;
                    WeakEventManager<UIElement, MouseEventArgs>.AddHandler(
                        element,
                        nameof(UIElement.MouseEnter),
                        OnMouseEnter
                    );
                    WeakEventManager<UIElement, MouseEventArgs>.AddHandler(
                        element,
                        nameof(UIElement.MouseLeave),
                        OnMouseLeave
                    );
                }
                else
                {
                    element.Opacity = 1.0;
                }
            }
        }

        private static void OnMouseEnter(object sender, MouseEventArgs e)
        {
            if (sender is FrameworkElement element)
            {
                element.Opacity = 1.0;
            }
        }

        private static void OnMouseLeave(object sender, MouseEventArgs e)
        {
            if (sender is FrameworkElement element)
            {
                element.Opacity = 0.8;
            }
        }
    }
}
