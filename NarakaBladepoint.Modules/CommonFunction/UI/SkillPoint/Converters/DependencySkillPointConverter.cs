using System;
using System.Globalization;
using System.Windows.Data;

namespace NarakaBladepoint.Modules.CommonFunction.UI.SkillPoint.Converters
{
    /// <summary>
    /// 技能点依赖转换器
    /// 用于判断两个技能点的和是否达到指定值，来确定Grid是否可点击
    /// </summary>
    public class DependencySkillPointConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null || values.Length < 2 || parameter == null)
                return true;

            if (!int.TryParse(values[0]?.ToString() ?? "0", out int firstValue))
                firstValue = 0;

            if (!int.TryParse(values[1]?.ToString() ?? "0", out int secondValue))
                secondValue = 0;

            if (!int.TryParse(parameter.ToString(), out int targetSum))
                targetSum = 4;

            // 当两个值的和等于目标值时，返回true（启用），否则返回false（禁用）
            return firstValue + secondValue == targetSum;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
