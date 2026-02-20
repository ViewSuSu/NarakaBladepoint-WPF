using System;
using System.Globalization;
using System.Windows.Data;

namespace NarakaBladepoint.Modules.CommonFunction.UI.SkillPoint.Converters
{
    /// <summary>
    /// 左上方向极坐标转换器（10-11点钟方向的3层圆形布局）
    /// 使用单值转换器，所有参数在ConverterParameter中指定
    /// 
    /// ConverterParameter 格式: "radius|angle|elementSize|coordinate"
    /// 例如:
    /// - 点0 (睹) Canvas.Left:  "150|120|50|0"  (radius=150, angle=120°, size=50, coordinate=X)
    /// - 点0 (睹) Canvas.Top:   "150|120|50|1"  (radius=150, angle=120°, size=50, coordinate=Y)
    /// - 点1 (马) Canvas.Left:  "150|150|50|0"  (radius=150, angle=150°, size=50, coordinate=X)
    /// - 点1 (马) Canvas.Top:   "150|150|50|1"  (radius=150, angle=150°, size=50, coordinate=Y)
    /// </summary>
    public class UpperLeftPolarConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is double centerCoordinate) || parameter is not string paramStr)
                return 0;

            var parts = paramStr.Split('|');
            if (parts.Length < 4)
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
