using System;
using System.Globalization;
using System.Windows.Data;

namespace NarakaBladepoint.Modules.CommonFunction.UI.SkillPoint.Converters
{
    /// <summary>
    /// 基于中心坐标、半径、角度计算元素位置（用于绝对坐标系统）
    /// ConverterParameter 格式: "radius,angleInDegrees"
    /// </summary>
    public class PolarToCartesianConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is double centerCoordinate) || parameter is not string paramStr)
                return 0;

            var parts = paramStr.Split(',');
            if (parts.Length < 3)
                return 0;

            if (!double.TryParse(parts[0], out double radius) ||
                !double.TryParse(parts[1], out double angleInDegrees) ||
                !double.TryParse(parts[2], out double elementSize) ||
                !int.TryParse(parts[3], out int coordinate)) // 0 for X, 1 for Y
                return 0;

            double angleInRadians = angleInDegrees * Math.PI / 180;

            if (coordinate == 0) // X coordinate
            {
                return centerCoordinate + radius * Math.Cos(angleInRadians) - elementSize / 2;
            }
            else // Y coordinate
            {
                return centerCoordinate + radius * Math.Sin(angleInRadians) - elementSize / 2;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
