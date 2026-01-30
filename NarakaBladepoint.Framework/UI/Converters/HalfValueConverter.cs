using System;
using System.Globalization;
using System.Windows.Data;

namespace NarakaBladepoint.Framework.UI.Converters
{
    /// <summary>
    /// Returns half of a numeric value (used to bind to ActualWidth / 2 in XAML).
    /// </summary>
    public class HalfValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double d)
                return d / 2.0;

            try
            {
                var dd = System.Convert.ToDouble(value);
                return dd / 2.0;
            }
            catch
            {
                return 0.0;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
