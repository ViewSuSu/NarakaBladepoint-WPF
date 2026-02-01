using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace NarakaBladepoint.Modules.CommonFunction.UI.Inventory.Converters
{
    /// <summary>
    /// 布尔值转换为Brush的转换器
    /// true -> White SolidColorBrush
    /// false -> Transparent SolidColorBrush
    /// </summary>
    internal class BoolToTransparentBrushConverter : IValueConverter
    {
        private static readonly SolidColorBrush WhiteBrush = new SolidColorBrush(Colors.White);

        private static readonly SolidColorBrush TransparentBrush = new SolidColorBrush(
            Colors.Transparent
        );

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
            {
                return boolValue ? WhiteBrush : TransparentBrush;
            }

            // 如果传入的不是bool类型，返回透明
            return TransparentBrush;
        }

        public object ConvertBack(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture
        )
        {
            // 如果value是Brush类型
            if (value is SolidColorBrush brush)
            {
                // 比较颜色值
                return brush.Color == Colors.White;
            }

            // 默认返回false
            return false;
        }
    }
}
