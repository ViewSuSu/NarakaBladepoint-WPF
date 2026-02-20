using System;
using System.Globalization;
using System.Windows.Data;
using NarakaBladepoint.Modules.CommonFunction.UI.SkillPoint.ViewModels;

namespace NarakaBladepoint.Modules.CommonFunction.UI.SkillPoint.Converters
{
    /// <summary>
    /// Converts SkillPointItemViewModel to opacity (0.5 if not learned, 1.0 if learned)
    /// </summary>
    public class LearnedToOpacityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is SkillPointItemViewModel skillPoint)
            {
                return skillPoint.CurrentLearned > 0 ? 1.0 : 0.5;
            }
            return 0.5;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

