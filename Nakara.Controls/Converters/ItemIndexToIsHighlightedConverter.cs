using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace Nakara.Controls.Converters
{
    internal class ItemIndexToIsHighlightedConverter : IMultiValueConverter
    {
        public object Convert(
            object[] values,
            Type targetType,
            object parameter,
            CultureInfo culture
        )
        {
            // values[0]: ComboBoxItem
            // values[1]: ComboBox
            if (
                values.Length >= 2
                && values[0] is ComboBoxItem comboBoxItem
                && values[1] is System.Windows.Controls.ComboBox comboBox
            )
            {
                try
                {
                    // 获取当前项的索引
                    var itemIndex = comboBox.ItemContainerGenerator.IndexFromContainer(
                        comboBoxItem
                    );

                    // 获取ComboBox的HighlightIndex属性
                    var highlightIndex = ComboBoxToggleExtensions.GetHighlightIndex(comboBox);

                    // 比较索引
                    if (itemIndex >= 0 && itemIndex == highlightIndex)
                    {
                        return true;
                    }
                }
                catch
                {
                    // 忽略错误，返回false
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
            throw new NotImplementedException();
        }
    }
}
