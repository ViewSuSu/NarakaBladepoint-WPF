using System.Globalization;
using System.Windows.Data;
using NarakaBladepoint.Framework.Core.Extensions;

namespace NarakaBladepoint.Framework.UI.Converters
{
    public class EnumToDescriptionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return string.Empty;
            if (value is Enum enumValue)
            {
                return enumValue.GetDescription() ?? enumValue.ToString();
            }
            return value.ToString();
        }

        public object ConvertBack(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture
        )
        {
            return Binding.DoNothing;
        }
    }
}
