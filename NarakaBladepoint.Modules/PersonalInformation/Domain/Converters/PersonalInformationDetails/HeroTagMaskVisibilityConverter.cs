using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace NarakaBladepoint.Modules.PersonalInformation.Domain.Converters.PersonalInformationDetails
{
    internal class HeroTagMaskVisibilityConverter : IMultiValueConverter
    {
        public object Convert(
            object[] values,
            Type targetType,
            object parameter,
            CultureInfo culture
        )
        {
            if (values.Length != 3)
                return Visibility.Collapsed;

            if (
                values[0] is not int selectedCount
                || values[1] is not bool isSelected
                || values[2] is not int maxCount
            )
                return Visibility.Collapsed;

            return (selectedCount >= maxCount && !isSelected)
                ? Visibility.Visible
                : Visibility.Collapsed;
        }

        public object[] ConvertBack(
            object value,
            Type[] targetTypes,
            object parameter,
            CultureInfo culture
        ) => throw new NotImplementedException();
    }
}
