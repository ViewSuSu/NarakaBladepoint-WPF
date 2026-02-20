using System;
using System.Globalization;
using System.Windows.Data;

namespace NarakaBladepoint.Modules.CommonFunction.UI.SkillPoint.Converters
{
    /// <summary>
    /// 计算相对于容器中心的偏移量
    /// </summary>
    public class CenterOffsetConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double containerSize && parameter is string paramStr)
            {
                if (double.TryParse(paramStr, out double elementSize))
                {
                    // 返回相对于中心的位置：(容器大小 - 元素大小) / 2
                    return (containerSize - elementSize) / 2;
                }
            }
            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
