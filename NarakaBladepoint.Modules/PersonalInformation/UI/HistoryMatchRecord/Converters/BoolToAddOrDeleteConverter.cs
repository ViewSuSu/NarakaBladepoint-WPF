using System.Globalization;
using System.Windows.Data;

namespace NarakaBladepoint.Modules.PersonalInformation.UI.HistoryMatchRecord.Converters
{
    internal class BoolToAddOrDeleteConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool)
            {
                bool boolValue = (bool)value;
                return boolValue ? "+" : "-";
            }
            return null;
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
