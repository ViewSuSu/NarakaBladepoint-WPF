using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace NarakaBladepoint.Framework.UI.AttachedProperties
{
    /// <summary>
    /// ScrollViewer 水平滚动附加属性
    ///
    /// 用途：为 ScrollViewer 添加鼠标滚轮水平滚动支持
    ///
    /// 使用场景：当 ScrollViewer 只有水平滚动条时，允许用户使用鼠标滚轮进行水平滚动
    ///
    /// 内存安全：使用 WeakEventManager 弱引用事件管理
    ///
    /// 使用示例：
    /// <![CDATA[
    /// <ScrollViewer attach:ScrollViewerHorizontalScrollAttachedProperty.EnableMouseWheelScroll="True"
    ///               HorizontalScrollBarVisibility="Auto"
    ///               VerticalScrollBarVisibility="Disabled">
    ///     <!-- 内容 -->
    /// </ScrollViewer>
    /// ]]>
    /// </summary>
    public static class ScrollViewerHorizontalScrollAttachedProperty
    {
        /// <summary>
        /// 是否启用鼠标滚轮水平滚动
        /// </summary>
        public static readonly DependencyProperty EnableMouseWheelScrollProperty =
            DependencyProperty.RegisterAttached(
                "EnableMouseWheelScroll",
                typeof(bool),
                typeof(ScrollViewerHorizontalScrollAttachedProperty),
                new PropertyMetadata(false, OnEnableMouseWheelScrollChanged)
            );

        public static bool GetEnableMouseWheelScroll(DependencyObject obj) =>
            (bool)obj.GetValue(EnableMouseWheelScrollProperty);

        public static void SetEnableMouseWheelScroll(DependencyObject obj, bool value) =>
            obj.SetValue(EnableMouseWheelScrollProperty, value);

        private static void OnEnableMouseWheelScrollChanged(
            DependencyObject d,
            DependencyPropertyChangedEventArgs e
        )
        {
            if (d is UIElement element)
            {
                // 使用弱引用事件管理
                WeakEventManager<UIElement, MouseWheelEventArgs>.RemoveHandler(
                    element,
                    nameof(UIElement.PreviewMouseWheel),
                    OnPreviewMouseWheel
                );

                if ((bool)e.NewValue)
                {
                    WeakEventManager<UIElement, MouseWheelEventArgs>.AddHandler(
                        element,
                        nameof(UIElement.PreviewMouseWheel),
                        OnPreviewMouseWheel
                    );
                }
            }
        }

        private static void OnPreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            var scrollViewer = sender as ScrollViewer;
            if (scrollViewer == null)
                return;

            // 检查是否可以水平滚动
            if (scrollViewer.ScrollableWidth > 0)
            {
                if (e.Delta > 0)
                    scrollViewer.LineLeft(); // 向左滚动
                else
                    scrollViewer.LineRight(); // 向右滚动

                e.Handled = true;
            }
        }
    }
}
