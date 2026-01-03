using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Nakara.Controls.Converters
{
    internal class IndexMatchConverter : IMultiValueConverter
    {
        public object Convert(
            object[] values,
            Type targetType,
            object parameter,
            CultureInfo culture
        )
        {
            if (values.Length != 2)
                return false;
            if (values[0] is int itemIndex && values[1] is int highlightIndex)
            {
                return itemIndex == highlightIndex;
            }
            return false;
        }

        public object[] ConvertBack(
            object value,
            Type[] targetTypes,
            object parameter,
            CultureInfo culture
        ) => throw new NotSupportedException();
    }
}
