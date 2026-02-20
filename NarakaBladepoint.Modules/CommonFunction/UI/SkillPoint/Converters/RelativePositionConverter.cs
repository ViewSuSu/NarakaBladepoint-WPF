using System;
using System.Globalization;
using System.Windows.Data;

namespace NarakaBladepoint.Modules.CommonFunction.UI.SkillPoint.Converters
{
    /// <summary>
    /// 根据中心坐标、半径、角度计算相对位置
    /// ConverterParameter 格式: "radius,angleInDegrees,elementSize"
    /// </summary>
    public class RelativePositionConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length < 2 || !(values[0] is double centerX) || !(values[1] is double centerY))
                return 0;

            if (parameter is not string paramStr)
                return 0;

            // 解析参数: "radius,angleInDegrees,elementSize"
            var parts = paramStr.Split(',');
            if (parts.Length < 3)
                return 0;

            if (!double.TryParse(parts[0], out double radius) ||
                !double.TryParse(parts[1], out double angleInDegrees) ||
                !double.TryParse(parts[2], out double elementSize))
                return 0;

            // 转换角度为弧度
            double angleInRadians = angleInDegrees * Math.PI / 180;

            // 计算相对于中心的偏移
            // X: centerX + radius * cos(angle) - elementSize/2
            // Y: centerY + radius * sin(angle) - elementSize/2

            if (targetType == typeof(double))
            {
                // 根据调用来源判断返回X还是Y
                // 这里需要通过额外参数区分，通常通过绑定路径名称
                return centerX + radius * Math.Cos(angleInRadians) - elementSize / 2;
            }

            return 0;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
