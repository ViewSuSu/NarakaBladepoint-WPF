using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Nakara.Controls.Converters
{
    /// <summary>
    /// 计算底部背景高度：IconHeight * BackgroundHeightFactor
    /// </summary>
    internal class IconTabItemConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is TabItem tabItem)
            {
                double iconHeight = IconTabItemProperties.GetIconHeight(tabItem);
                double factor = IconTabItemProperties.GetBackgroundHeightFactor(tabItem);
                return iconHeight * factor;
            }
            return 0;
        }

        public object ConvertBack(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture
        ) => throw new NotImplementedException();
    }
}
