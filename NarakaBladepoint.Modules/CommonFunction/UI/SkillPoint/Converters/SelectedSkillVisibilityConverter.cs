using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace NarakaBladepoint.Modules.CommonFunction.UI.SkillPoint.Converters
{
    /// <summary>
    /// 技能选中互斥显示转换器
    /// 根据选中的技能类型决定控件的显示/隐藏
    /// </summary>
    public class SelectedSkillVisibilityConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            // values: [currentSelectedSkillType (string)]
            if (values == null || values.Length < 1 || parameter == null)
                return Visibility.Collapsed;

            string currentSelectedSkillType = values[0] as string ?? "";
            string skillType = parameter.ToString().ToLower();

            return skillType switch
            {
                "f1" => currentSelectedSkillType == "f1" ? Visibility.Visible : Visibility.Collapsed,
                "f2" => currentSelectedSkillType == "f2" ? Visibility.Visible : Visibility.Collapsed,
                "v1" => currentSelectedSkillType == "v1" ? Visibility.Visible : Visibility.Collapsed,
                "v2" => currentSelectedSkillType == "v2" ? Visibility.Visible : Visibility.Collapsed,
                // canvas 在没有选中任何技能（F/V）或选中天赋时显示
                "canvas" => (string.IsNullOrEmpty(currentSelectedSkillType) || currentSelectedSkillType == "tianfu") ? Visibility.Visible : Visibility.Collapsed,
                _ => Visibility.Collapsed
            };
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
