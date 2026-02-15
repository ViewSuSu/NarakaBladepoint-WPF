using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace NarakaBladepoint.Modules.EventCenter.UI.TimeLimitedEvent.Converters
{
    /// <summary>
    /// 将 double 值转换为 Thickness 的 Top 值
    /// 用于将 Height 绑定到 Content 的 Margin.Top
    /// </summary>
    internal class DoubleToThicknessTopConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double doubleValue)
            {
                return new Thickness(0, doubleValue, 0, 0);
            }
            return new Thickness(0);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Thickness thickness)
            {
                return thickness.Top;
            }
            return 0d;
        }
    }
}
