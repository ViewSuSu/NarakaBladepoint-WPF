using System.Globalization;
using System.Windows.Data;

namespace NarakaBladepoint.Controls.Converters
{
    /// <summary>
    /// 计算底部背景高度：IconHeight * BackgroundHeightFactor
    /// </summary>
    internal class IconTabItemConverter : IMultiValueConverter
    {
        public object Convert(
            object[] values,
            Type targetType,
            object parameter,
            CultureInfo culture
        )
        {
            if (values.Length >= 2 && values[0] is double iconHeight && values[1] is double factor)
            {
                return iconHeight * factor;
            }
            return 30.0 * 1.1; // 默认值
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
