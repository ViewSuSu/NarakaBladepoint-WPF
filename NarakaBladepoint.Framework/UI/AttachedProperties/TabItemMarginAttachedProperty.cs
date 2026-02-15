using System.Windows;
using System.Windows.Controls;

namespace NarakaBladepoint.Framework.UI.AttachedProperties
{
    /// <summary>
    /// TabControl TabItem 间距附加属性
    ///
    /// 用途：为 TabControl 中的 TabItem 添加统一的间距（Margin）
    ///
    /// 使用场景：根据 TabControl 的布局方向（水平或垂直）自动调整 TabItem 的间距
    /// - 水平布局（Top/Bottom）：调整左右间距
    /// - 垂直布局（Left/Right）：调整上下间距
    ///
    /// 重要：此附加属性内部使用 Padding 而不是 Margin 来实现间距。
    /// 这样做是为了避免 Adorner 层（用于呈现高亮边框）被父容器裁剪的问题。
    /// 
    /// 使用示例：
    /// <![CDATA[
    /// <!-- 垂直布局，TabItem 间距 5px -->
    /// <TabControl
    ///     attach:TabItemMarginAttachedProperty.ItemMargin="0,5,0,5"
    ///     TabStripPlacement="Left">
    ///     <TabItem Header="Item 1" />
    ///     <TabItem Header="Item 2" />
    /// </TabControl>
    ///
    /// <!-- 水平布局，TabItem 间距 10px -->
    /// <TabControl
    ///     attach:TabItemMarginAttachedProperty.ItemMargin="5,0,5,0"
    ///     TabStripPlacement="Top">
    ///     <TabItem Header="Item 1" />
    ///     <TabItem Header="Item 2" />
    /// </TabControl>
    /// ]]>
    /// </summary>
    public static class TabItemMarginAttachedProperty
    {
        /// <summary>
        /// TabItem 的间距
        /// </summary>
        public static readonly DependencyProperty ItemMarginProperty =
            DependencyProperty.RegisterAttached(
                "ItemMargin",
                typeof(Thickness),
                typeof(TabItemMarginAttachedProperty),
                new PropertyMetadata(new Thickness(0), OnItemMarginChanged)
            );

        public static Thickness GetItemMargin(DependencyObject obj) =>
            (Thickness)obj.GetValue(ItemMarginProperty);

        public static void SetItemMargin(DependencyObject obj, Thickness value) =>
            obj.SetValue(ItemMarginProperty, value);

        private static void OnItemMarginChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TabControl tabControl && e.NewValue is Thickness margin)
            {
                tabControl.Loaded += (s, args) => ApplyItemMargins(tabControl, margin);
            }
        }

        private static void ApplyItemMargins(TabControl tabControl, Thickness margin)
        {
            // 获取所有 TabItem
            var items = tabControl.Items;
            if (items.Count == 0)
                return;

            // 根据 TabStripPlacement 调整间距
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i] is TabItem tabItem)
                {
                    var adjustedMargin = AdjustMarginByPlacement(margin, tabControl.TabStripPlacement, i, items.Count);
                    // 使用 Padding 而不是 Margin，以避免 Adorner（高亮边框）被裁剪
                    tabItem.Padding = adjustedMargin;
                }
            }
        }

        /// <summary>
        /// 根据 TabStripPlacement 调整 Margin
        /// </summary>
        private static Thickness AdjustMarginByPlacement(
            Thickness originalMargin,
            Dock placement,
            int itemIndex,
            int totalItems)
        {
            switch (placement)
            {
                case Dock.Left:
                case Dock.Right:
                    // 垂直布局：保持原始 Margin（上下间距）
                    // 第一个不需要上边距，最后一个不需要下边距
                    if (itemIndex == 0)
                    {
                        return new Thickness(originalMargin.Left, 0, originalMargin.Right, originalMargin.Bottom);
                    }
                    else if (itemIndex == totalItems - 1)
                    {
                        return new Thickness(originalMargin.Left, originalMargin.Top, originalMargin.Right, 0);
                    }
                    return originalMargin;

                case Dock.Top:
                case Dock.Bottom:
                default:
                    // 水平布局：保持原始 Margin（左右间距）
                    // 第一个不需要左边距，最后一个不需要右边距
                    if (itemIndex == 0)
                    {
                        return new Thickness(0, originalMargin.Top, originalMargin.Right, originalMargin.Bottom);
                    }
                    else if (itemIndex == totalItems - 1)
                    {
                        return new Thickness(originalMargin.Left, originalMargin.Top, 0, originalMargin.Bottom);
                    }
                    return originalMargin;
            }
        }
    }
}
