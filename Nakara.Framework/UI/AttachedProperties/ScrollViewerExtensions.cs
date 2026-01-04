using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Nakara.Framework.UI.AttachedProperties
{
    public static class ScrollViewerExtensions
    {
        public static readonly DependencyProperty HorizontalScrollOnMouseWheelProperty =
            DependencyProperty.RegisterAttached(
                "HorizontalScrollOnMouseWheel",
                typeof(bool),
                typeof(ScrollViewerExtensions),
                new PropertyMetadata(false, OnHorizontalScrollOnMouseWheelChanged)
            );

        public static bool GetHorizontalScrollOnMouseWheel(DependencyObject obj) =>
            (bool)obj.GetValue(HorizontalScrollOnMouseWheelProperty);

        public static void SetHorizontalScrollOnMouseWheel(DependencyObject obj, bool value) =>
            obj.SetValue(HorizontalScrollOnMouseWheelProperty, value);

        private static void OnHorizontalScrollOnMouseWheelChanged(
            DependencyObject d,
            DependencyPropertyChangedEventArgs e
        )
        {
            if (d is UIElement element)
            {
                element.PreviewMouseWheel -= OnPreviewMouseWheel;

                if ((bool)e.NewValue)
                {
                    element.PreviewMouseWheel += OnPreviewMouseWheel;
                }
            }
        }

        private static void OnPreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            // 查找父级的 ScrollViewer
            var scrollViewer = sender as ScrollViewer;
            if (scrollViewer == null)
                return;

            // 检查是否可以水平滚动
            if (scrollViewer.ScrollableWidth > 0)
            {
                double scrollAmount = 50; // 每次滚动的像素数
                if (e.Delta > 0)
                    scrollViewer.LineLeft(); // 向左滚动
                else
                    scrollViewer.LineRight(); // 向右滚动

                e.Handled = true;
            }
        }

        private static T FindParent<T>(DependencyObject child)
            where T : DependencyObject
        {
            var parent = VisualTreeHelper.GetParent(child);

            while (parent != null)
            {
                if (parent is T parentOfType)
                    return parentOfType;

                parent = VisualTreeHelper.GetParent(parent);
            }

            return null;
        }
    }
}
