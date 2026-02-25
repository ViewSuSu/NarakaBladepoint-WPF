using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace NarakaBladepoint.Modules.CommonFunction.UI.SkillPoint.Converters
{
    /// <summary>
    /// 用于判断技能点编辑图标的可见性
    /// 只有当技能点可学习且仍有剩余技能点时才显示
    /// </summary>
    public class CanEditImageVisibilityConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length < 2)
                return Visibility.Collapsed;

            // values[0] 是 IsLearnable (bool)
            // values[1] 是 RemainingPoints (int)
            if (values[0] is bool isLearnable && values[1] is int remainingPoints)
            {
                return (isLearnable && remainingPoints > 0) ? Visibility.Visible : Visibility.Collapsed;
            }

            return Visibility.Collapsed;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
