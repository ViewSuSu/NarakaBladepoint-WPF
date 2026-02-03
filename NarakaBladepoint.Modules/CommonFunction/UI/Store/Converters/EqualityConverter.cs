using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace NarakaBladepoint.Modules.CommonFunction.UI.Store.Converters
{
    /// <summary>
    /// 比较两个值是否相等的转换器，用于返回不同的Brush或其他值
    /// 相等时返回White，不相等时返回Transparent
    /// </summary>
    internal class EqualityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null)
            {
                return GetFalseValue(targetType);
            }

            bool isEqual = value.ToString() == parameter.ToString();

            // 如果目标类型是Brush，返回颜色Brush
            if (targetType == typeof(Brush) || targetType == typeof(SolidColorBrush))
            {
                return isEqual ? Brushes.White : Brushes.Transparent;
            }

            // 其他类型返回bool
            return isEqual;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private object GetFalseValue(Type targetType)
        {
            if (targetType == typeof(Brush) || targetType == typeof(SolidColorBrush))
            {
                return Brushes.Transparent;
            }
            return false;
        }
    }
}
