using System;
using System.Globalization;
using System.Windows.Data;

namespace NarakaBladepoint.Modules.CommonFunction.UI.SkillPoint.Converters
{
    /// <summary>
    /// 关于水平轴的镜像转换器（上下镜像）
    /// 这是 MultiValueConverter，需要同时绑定基础点位置和Canvas高度
    /// 公式: y' = canvasHeight - y - elementHeight
    /// ConverterParameter 格式: "elementHeight"（只需传入元素高度）
    /// 使用方式:
    /// &lt;MultiBinding Converter="{StaticResource VerticalMirrorConverter}" ConverterParameter="50"&gt;
    ///     &lt;Binding ElementName="BaseGrid" Path="(Canvas.Top)"/&gt;
    ///     &lt;Binding ElementName="skillPointCanvas" Path="ActualHeight"/&gt;
    /// &lt;/MultiBinding&gt;
    /// </summary>
    public class VerticalMirrorConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length < 2 || !(values[0] is double elementTop) || !(values[1] is double canvasHeight))
                return 0;

            if (!(parameter is string paramStr) || !double.TryParse(paramStr, out double elementHeight))
                return 0;

            // 镜像计算: y' = canvasHeight - y - elementHeight
            return canvasHeight - elementTop - elementHeight;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
