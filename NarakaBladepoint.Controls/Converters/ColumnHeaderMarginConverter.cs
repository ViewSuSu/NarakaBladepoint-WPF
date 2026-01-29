using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace NarakaBladepoint.Controls.Converters
{
    // Converts column header DisplayIndex and total column count to a Thickness margin:
    // - first header: left=0, right=5
    // - middle headers: left=0, right=5
    // - last header: left=0, right=0
    internal class ColumnHeaderMarginConverter : IMultiValueConverter
    {
        public object Convert(
            object[] values,
            Type targetType,
            object parameter,
            CultureInfo culture
        )
        {
            if (values == null || values.Length < 2)
                return new Thickness(0);

            if (!int.TryParse(values[0]?.ToString() ?? "0", out int displayIndex))
                displayIndex = 0;

            if (!int.TryParse(values[1]?.ToString() ?? "0", out int count))
                count = 0;

            const double gap = 5.0;

            if (count <= 1)
                return new Thickness(0);

            double left = 0,
                right = gap;

            if (displayIndex == count - 1)
                right = 0;

            return new Thickness(left, 0, right, 0);
        }

        public object[] ConvertBack(
            object value,
            Type[] targetTypes,
            object parameter,
            CultureInfo culture
        )
        {
            throw new NotImplementedException();
        }
    }
}
