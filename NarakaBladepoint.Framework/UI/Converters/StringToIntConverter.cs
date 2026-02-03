using System;
using System.Globalization;
using System.Windows.Data;

namespace NarakaBladepoint.Framework.UI.Converters
{
    /// <summary>
    /// 将字符串转换为int或int?
    /// </summary>
    public class StringToIntConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }

            if (int.TryParse(value.ToString(), out int result))
            {
                return result;
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }

            if (targetType == typeof(string) || targetType == typeof(object))
            {
                return value.ToString();
            }

            return null;
        }
    }
}
