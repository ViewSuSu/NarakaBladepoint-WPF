using System;
using System.Globalization;
using System.Windows.Data;

namespace NarakaBladepoint.Modules.SocialTag.UI.Converters
{
    public class WidthSubtractConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length >= 2 && values[0] is double tabControlWidth && values[1] is double tabItemWidth)
            {
                double result = tabControlWidth - tabItemWidth;
                return result > 0 ? result : 0;
            }
            return 0;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
