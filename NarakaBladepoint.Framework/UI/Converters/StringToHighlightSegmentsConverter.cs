using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using NarakaBladepoint.Framework.UI.AttachedProperties;

namespace NarakaBladepoint.Framework.UI.Converters
{
    /// <summary>
    /// 将字符串列表转换为 HighlightSegmentCollection
    /// 支持将 List&lt;string&gt; 直接转换为多个高亮文本片段
    /// 所有片段使用默认颜色 #EAB181
    /// </summary>
    public class StringToHighlightSegmentsConverter : IValueConverter
    {
        /// <summary>
        /// 默认高亮颜色 #EAB181
        /// </summary>
        public Brush DefaultOrangeBrush { get; set; } =
            (Brush)System.Windows.Application.Current.Resources["DefaultOrangeBrush"];

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not List<string> highlightTexts || highlightTexts.Count == 0)
            {
                return null;
            }

            var segments = new HighlightSegmentCollection();

            foreach (var text in highlightTexts)
            {
                if (string.IsNullOrWhiteSpace(text))
                    continue;

                segments.Add(
                    new HighlightSegment { Text = text.Trim(), Foreground = DefaultOrangeBrush }
                );
            }

            return segments.Count > 0 ? segments : null;
        }

        public object ConvertBack(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture
        )
        {
            if (value is not HighlightSegmentCollection segments)
                return null;

            // 反向转换：将 HighlightSegmentCollection 转换回 List<string>
            var result = new List<string>();

            foreach (var segment in segments)
            {
                if (!string.IsNullOrEmpty(segment.Text))
                {
                    result.Add(segment.Text);
                }
            }

            return result;
        }
    }
}
