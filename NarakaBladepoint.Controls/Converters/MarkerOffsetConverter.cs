using System;
using System.Globalization;
using System.Windows.Data;

namespace NarakaBladepoint.Controls.Converters
{
    /// <summary>
    /// Calculates left offset for a marker so its right edge aligns at the item's center on the main progress bar.
    /// Inputs: [0]=mainWidth (double), [1]=itemIndex (int), [2]=itemsCount (int), [3]=barLength (double)
    /// Output: left offset (double)
    /// </summary>
    public class MarkerOffsetConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null || values.Length < 4) return 0.0;

            if (!(values[0] is double mainWidth)) return 0.0;
            if (!(values[1] is int index))
            {
                // AlternationIndex can come as string or other, try parse
                try { index = System.Convert.ToInt32(values[1]); } catch { index = 0; }
            }
            if (!(values[2] is int count))
            {
                try { count = System.Convert.ToInt32(values[2]); } catch { count = 1; }
            }
            if (!(values[3] is double barLength))
            {
                try { barLength = System.Convert.ToDouble(values[3]); } catch { barLength = 0.0; }
            }

            if (count <= 0) count = 1;

            // position center fraction for this item
            double centerFraction = (index + 0.5) / count;
            double centerX = centerFraction * mainWidth;
            double left = centerX - barLength;
            return left;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
