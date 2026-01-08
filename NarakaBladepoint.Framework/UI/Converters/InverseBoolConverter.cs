using System;
using System.Globalization;
using System.Windows.Data;

namespace NarakaBladepoint.Framework.UI.Converters
{
    /// <summary>
    /// 布尔值取反转换器
    /// </summary>
    [ValueConversion(typeof(bool), typeof(bool))]
    public class InverseBoolConverter : IValueConverter
    {
        /// <summary>
        /// 将布尔值取反
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
            {
                return !boolValue;
            }

            // 如果值不是布尔类型，尝试转换
            if (value != null)
            {
                try
                {
                    return !System.Convert.ToBoolean(value);
                }
                catch
                {
                    // 转换失败，返回原始值
                    return value;
                }
            }

            return false;
        }

        /// <summary>
        /// 将布尔值取反（反向转换）
        /// </summary>
        public object ConvertBack(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture
        )
        {
            // 反向转换也是取反
            return Convert(value, targetType, parameter, culture);
        }
    }
}
