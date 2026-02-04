using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace NarakaBladepoint.Modules.CommonFunction.UI.Leaderboard.Converters
{
    internal class RankToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int rank && rank >= 1 && rank <= 3)
            {
                return Visibility.Visible;
            }

            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}
