using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace NarakaBladepoint.Controls.Converters
{
    // MultiValueConverter that returns Visibility.Visible when the segment is fully completed
    // values expected: [progressText1, progressText2, currentItem, itemsCollection]
    internal class ProgressCompletedToVisibilityConverter : IMultiValueConverter
    {
        // If true, invert the result
        public bool Invert { get; set; }

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            double progress1 = 0.0;
            double progress2 = 1.0; // avoid division by zero
            object currentItem = null;
            IEnumerable itemsCollection = null;

            if (values.Length > 0 && values[0] != null)
                double.TryParse(values[0].ToString(), System.Globalization.NumberStyles.Any, culture, out progress1);
            if (values.Length > 1 && values[1] != null)
                double.TryParse(values[1].ToString(), System.Globalization.NumberStyles.Any, culture, out progress2);
            if (values.Length > 2)
                currentItem = values[2];
            if (values.Length > 3 && values[3] is IEnumerable col)
                itemsCollection = col;

            if (progress2 == 0) progress2 = 1.0;

            int itemsCount = 1;
            int alternationIndex = 0;
            if (itemsCollection != null)
            {
                if (itemsCollection is IList list)
                {
                    itemsCount = list.Count;
                    if (currentItem != null)
                    {
                        alternationIndex = list.IndexOf(currentItem);
                        if (alternationIndex < 0) alternationIndex = 0;
                    }
                }
                else
                {
                    int idx = 0;
                    int found = -1;
                    int cnt = 0;
                    foreach (var it in itemsCollection)
                    {
                        if (object.Equals(it, currentItem) && found == -1)
                            found = idx;
                        idx++;
                        cnt++;
                    }
                    itemsCount = cnt > 0 ? cnt : 1;
                    alternationIndex = found >= 0 ? found : 0;
                }
            }

            double overallPercent = (progress1 / progress2) * 100.0;
            double filledAmount = overallPercent * itemsCount;
            double segmentStart = alternationIndex * 100.0;
            double segmentValue = Math.Max(0.0, Math.Min(100.0, filledAmount - segmentStart));

            bool completed = segmentValue >= 100.0 - 1e-6; // tolerate floating point
            if (Invert) completed = !completed;

            return completed ? Visibility.Visible : Visibility.Collapsed;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
