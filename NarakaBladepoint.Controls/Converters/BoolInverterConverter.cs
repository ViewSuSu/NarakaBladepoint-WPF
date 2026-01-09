using System.Globalization;
using System.Windows.Data;

namespace NarakaBladepoint.Controls.Converters
{
    internal class BoolInverterConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool b)
            {
                return !b; // 关键：取反
            }

            return Binding.DoNothing;
        }

        public object ConvertBack(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture
        )
        {
            if (value is bool b)
            {
                return !b; // 双向绑定时同样取反
            }

            return Binding.DoNothing;
        }
    }
}
