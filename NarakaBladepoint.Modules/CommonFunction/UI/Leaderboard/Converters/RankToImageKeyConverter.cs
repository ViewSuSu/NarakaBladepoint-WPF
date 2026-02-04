using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace NarakaBladepoint.Modules.CommonFunction.UI.Leaderboard.Converters
{
    internal class RankToImageKeyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int rank)
            {
                string key = rank switch
                {
                    1 => "LeaderboardRank1Image",
                    2 => "LeaderboardRank2Image",
                    3 => "LeaderboardRank3Image",
                    _ => null,
                };

                if (key != null)
                {
                    return Application.Current.TryFindResource(key);
                }
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}
