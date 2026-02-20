using System;
using System.Globalization;
using System.Windows.Data;

namespace NarakaBladepoint.Modules.CommonFunction.UI.SkillPoint.Converters
{
    /// <summary>
    /// 关于竖直轴的镜像转换器（左右镜像）
    /// 这是 MultiValueConverter，需要同时绑定基础点位置和Canvas宽度
    /// 公式: x' = canvasWidth - x - elementWidth
    /// ConverterParameter 格式: "elementWidth"（只需传入元素宽度）
    /// 使用方式:
    /// &lt;MultiBinding Converter="{StaticResource HorizontalMirrorConverter}" ConverterParameter="50"&gt;
    ///     &lt;Binding ElementName="BaseGrid" Path="(Canvas.Left)"/&gt;
    ///     &lt;Binding ElementName="skillPointCanvas" Path="ActualWidth"/&gt;
    /// &lt;/MultiBinding&gt;
    /// </summary>
    public class HorizontalMirrorConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length < 2 || !(values[0] is double elementLeft) || !(values[1] is double canvasWidth))
                return 0;

            if (!(parameter is string paramStr) || !double.TryParse(paramStr, out double elementWidth))
                return 0;

            // 镜像计算: x' = canvasWidth - x - elementWidth
            return canvasWidth - elementLeft - elementWidth;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
