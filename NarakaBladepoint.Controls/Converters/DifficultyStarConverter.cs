using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace NarakaBladepoint.Controls.Converters
{
    internal class DifficultyStarConverter : IMultiValueConverter
    {
        public object Convert(
            object[] values,
            Type targetType,
            object parameter,
            CultureInfo culture
        )
        {
            int index = (int)values[0];
            double number = (double)values[1];

            if (number >= index)
                return Load("2.png");

            if (number >= index - 0.5)
                return Load("3.png");

            return Load("1.png");
        }

        private static BitmapImage Load(string name)
        {
            return new BitmapImage(
                new Uri(
                    $"/NarakaBladepoint.Resources;component/Image/Hero/CustomControls/{name}",
                    UriKind.Relative
                )
            );
        }

        public object[] ConvertBack(
            object value,
            Type[] targetTypes,
            object parameter,
            CultureInfo culture
        )
        {
            throw new NotSupportedException();
        }
    }
}
