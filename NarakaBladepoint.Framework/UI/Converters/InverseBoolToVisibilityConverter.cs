using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace NarakaBladepoint.Framework.UI.Converters
{
    /// <summary>
    /// 将 bool 值取反后转换为 Visibility（true -> Collapsed, false -> Visible）
    /// </summary>
    [ValueConversion(typeof(bool), typeof(Visibility))]
    public class InverseBoolToVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// 是否使用 Hidden 代替 Collapsed
        /// </summary>
        public bool UseHidden { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
            {
                // 如果值为 true，则隐藏元素
                if (boolValue)
                {
                    return UseHidden ? Visibility.Hidden : Visibility.Collapsed;
                }
                // 如果值为 false，则显示元素
                else
                {
                    return Visibility.Visible;
                }
            }

            // 如果不是 bool 类型，返回 Visible 或根据参数决定
            return Visibility.Visible;
        }

        public object ConvertBack(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture
        )
        {
            if (value is Visibility visibility)
            {
                // Visible -> false, Collapsed/Hidden -> true
                return visibility != Visibility.Visible;
            }

            return false;
        }
    }
}
