using System.Globalization;
using System.Windows.Data;

namespace NarakaBladepoint.Controls.Converters
{
    /// <summary>
    /// 将项索引转换为是否高亮显示的转换器
    /// </summary>
    internal class ItemIndexToIsHighlightedConverter : IMultiValueConverter
    {
        public object Convert(
            object[] values,
            Type targetType,
            object parameter,
            CultureInfo culture
        )
        {
            if (
                values.Length >= 2
                && values[0] is ToggleButtonComboBoxItem comboBoxItem
                && values[1] is ToggleButtonComboBox comboBox
            )
            {
                try
                {
                    // 获取当前项的索引
                    var itemIndex = comboBox.ItemContainerGenerator.IndexFromContainer(
                        comboBoxItem
                    );

                    // 获取ComboBox的HighlightIndex属性
                    var highlightIndex = comboBox.HighlightIndex;

                    // 比较索引
                    return itemIndex >= 0 && itemIndex == highlightIndex;
                }
                catch
                {
                    // 忽略错误，返回false
                    return false;
                }
            }
            return false;
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
