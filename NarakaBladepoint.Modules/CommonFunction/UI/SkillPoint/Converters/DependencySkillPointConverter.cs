using System;
using System.Globalization;
using System.Windows.Data;

namespace NarakaBladepoint.Modules.CommonFunction.UI.SkillPoint.Converters
{
    /// <summary>
    /// 通用的两值求和比较转换器
    /// 用于判断两个数值之和是否大于等于指定值
    /// </summary>
    public class DependencySkillPointConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            // 需要至少3个值：value1, value2, limit
            if (values == null || values.Length < 3)
            {
                return true; // 绑定失败时默认启用
            }

            // 安全地解析第一个值
            int firstValue = 0;
            if (values[0] != null)
            {
                if (!int.TryParse(values[0].ToString(), out firstValue))
                {
                    firstValue = 0;
                }
            }

            // 安全地解析第二个值
            int secondValue = 0;
            if (values[1] != null)
            {
                if (!int.TryParse(values[1].ToString(), out secondValue))
                {
                    secondValue = 0;
                }
            }

            // 安全地解析限制值
            int limitValue = 0;
            if (values[2] != null)
            {
                if (!int.TryParse(values[2].ToString(), out limitValue))
                {
                    limitValue = 0;
                }
            }

            // 当两个值的和达到或超过限制值时，返回true（启用），否则返回false（禁用）
            bool result = (firstValue + secondValue) >= limitValue;
            return result;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
