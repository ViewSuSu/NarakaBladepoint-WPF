using System;
using System.Globalization;
using System.Windows.Data;
using NarakaBladepoint.Shared.Enums;

namespace NarakaBladepoint.Modules.PersonalInformation.Domain.Converters
{
    public class TeamSizeToIndexConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is TeamSize teamSize)
            {
                // 将TeamSize枚举值转换为索引（减1）
                return (int)teamSize - 1;
            }
            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int index)
            {
                // 将索引转换为TeamSize枚举值（加1）
                return (TeamSize)(index + 1);
            }
            return TeamSize.Solo;
        }
    }
}