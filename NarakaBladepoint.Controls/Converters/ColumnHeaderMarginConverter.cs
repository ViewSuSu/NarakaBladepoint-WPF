using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace NarakaBladepoint.Controls.Converters
{
    /// <summary>
    /// 将列头的 DisplayIndex 和总列数转换为 Thickness margin
    /// - 第一列（首列）: left=0, right=5 （首列左侧间距为 0）
    /// - 中间列: left=0, right=5 （中间列右侧间距为 5）
    /// - 最后一列（末列）: left=0, right=0 （末列右侧间距为 0）
    /// </summary>
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

            // 如果只有一列或没有列，返回无间距
            if (count <= 1)
                return new Thickness(0);

            const double gap = 5.0;
            double left = 0;  // 所有列左侧间距都为 0
            double right = gap;  // 默认右侧间距为 5

            // 最后一列右侧间距为 0
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
