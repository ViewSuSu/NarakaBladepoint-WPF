using System;
using System.Windows.Markup;

namespace NarakaBladepoint.Modules.CommonFunction.UI.SkillPoint.Converters
{
    /// <summary>
    /// 技能点极坐标转换器参数MarkupExtension
    /// 动态组合转换器参数字符串，避免硬编码重复值
    /// 
    /// 使用方式:
    /// ConverterParameter="{local:SkillPointParameter Radius=150, Angle=120, ElementSize=50, Coordinate=0}"
    /// </summary>
    public class SkillPointParameterExtension : MarkupExtension
    {
        /// <summary>
        /// 圆的半径
        /// </summary>
        public double Radius { get; set; }

        /// <summary>
        /// 角度（度数）
        /// </summary>
        public double Angle { get; set; }

        /// <summary>
        /// 技能点元素大小
        /// </summary>
        public double ElementSize { get; set; }

        /// <summary>
        /// 坐标类型 (0 for X, 1 for Y)
        /// </summary>
        public int Coordinate { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            // 格式: "radius|angle|elementSize|coordinate"
            return $"{Radius}|{Angle}|{ElementSize}|{Coordinate}";
        }
    }
}
