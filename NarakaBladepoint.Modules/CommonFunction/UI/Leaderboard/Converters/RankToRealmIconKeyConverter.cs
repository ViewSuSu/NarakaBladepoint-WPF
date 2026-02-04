using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace NarakaBladepoint.Modules.CommonFunction.UI.Leaderboard.Converters
{
    internal class RankToRealmIconKeyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int rank)
            {
                string key = (rank >= 1 && rank <= 3) ? "LeaderboardRealmTopImage" : "LeaderboardRealmOtherImage";
                return Application.Current.TryFindResource(key);
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}
