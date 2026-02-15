using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace NarakaBladepoint.Framework.UI.AttachedProperties
{
    /// <summary>
    /// TabControl 的附加属性，用于自动管理所有 TabItem Content 的 Margin
    /// 当启用此属性时，TabControl 中所有 TabItem 的 Content 的顶部 Margin 将被设置为
    /// 第一个不被隐藏的 TabItem 的高度的负数，实现向上偏移效果
    /// </summary>
    public static class TabControlContentMarginAttachedProperty
    {
        /// <summary>
        /// 获取或设置是否自动管理 TabControl 中 TabItem Content 的 Margin
        /// </summary>
        public static bool GetIsAutoAdjustContentMargin(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsAutoAdjustContentMarginProperty);
        }

        public static void SetIsAutoAdjustContentMargin(DependencyObject obj, bool value)
        {
            obj.SetValue(IsAutoAdjustContentMarginProperty, value);
        }

        public static readonly DependencyProperty IsAutoAdjustContentMarginProperty =
            DependencyProperty.RegisterAttached(
                "IsAutoAdjustContentMargin",
                typeof(bool),
                typeof(TabControlContentMarginAttachedProperty),
                new PropertyMetadata(false, OnIsAutoAdjustContentMarginChanged));

        private static void OnIsAutoAdjustContentMarginChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not TabControl tabControl)
                return;

            if ((bool)e.NewValue)
            {
                // 当属性被设置为 true 时，应用 Margin 绑定
                tabControl.Dispatcher.BeginInvoke(() =>
                {
                    ApplyContentMarginBinding(tabControl);
                });
            }
        }

        private static void ApplyContentMarginBinding(TabControl tabControl)
        {
            if (tabControl.Items.Count == 0)
                return;

            // 获取第一个不被隐藏的 TabItem
            TabItem? referenceTabItem = null;
            for (int i = 0; i < tabControl.Items.Count; i++)
            {
                if (tabControl.Items[i] is TabItem tabItem)
                {
                    // 检查 TabItem 是否不被隐藏
                    if (tabItem.Visibility != Visibility.Hidden)
                    {
                        referenceTabItem = tabItem;
                        break;
                    }
                }
            }

            if (referenceTabItem == null)
                return;

            // 为所有 TabItem 应用 Margin 绑定
            for (int i = 0; i < tabControl.Items.Count; i++)
            {
                if (tabControl.Items[i] is TabItem tabItem)
                {
                    // 创建 ContentPresenter 来包裹原始 Content
                    var content = tabItem.Content;

                    var contentPresenter = new ContentPresenter
                    {
                        Content = content
                    };

                    // 绑定 Margin 到第一个不被隐藏的 TabItem 的 Height（负数）
                    var binding = new Binding
                    {
                        Source = referenceTabItem,
                        Path = new PropertyPath("ActualHeight"),
                        Converter = new NegativeHeightToMarginConverter()
                    };

                    contentPresenter.SetBinding(FrameworkElement.MarginProperty, binding);

                    // 将 ContentPresenter 设置为新的 Content
                    tabItem.Content = contentPresenter;
                }
            }
        }

        /// <summary>
        /// 内部转换器：将高度转换为负 Margin
        /// </summary>
        private class NegativeHeightToMarginConverter : IValueConverter
        {
            public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                if (value is double doubleValue)
                {
                    return new Thickness(0, -doubleValue, 0, 0);
                }
                return new Thickness(0);
            }

            public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                if (value is Thickness thickness)
                {
                    return -thickness.Top;
                }
                return 0d;
            }
        }
    }
}
