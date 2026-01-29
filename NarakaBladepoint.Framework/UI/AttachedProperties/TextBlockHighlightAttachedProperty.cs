using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace NarakaBladepoint.Framework.UI.AttachedProperties
{
    /// <summary>
    /// 高亮文本片段集合
    /// </summary>
    public class HighlightSegmentCollection : List<HighlightSegment> { }

    /// <summary>
    /// 单个高亮文本片段
    /// </summary>
    public class HighlightSegment
    {
        /// <summary>
        /// 要高亮的文本
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// 高亮文本的前景色
        /// </summary>
        public Brush Foreground { get; set; } =
            new SolidColorBrush(Color.FromArgb(255, 0xEA, 0xB1, 0x81));
    }

    /// <summary>
    /// TextBlock 文本高亮附加属性
    ///
    /// 用途：为 TextBlock 中的指定文本添加高亮效果（改变前景色）
    ///
    /// 实现方式：使用 TextEffect 实现高亮，不修改文本内容
    ///
    /// 内存安全：使用弱引用事件管理，避免内存泄漏
    ///
    /// 支持的数据类型：
    /// - 单个文本：使用 HighlightText 属性
    /// - 多个文本：使用 HighlightSegments 属性，支持 List&lt;string&gt; 或 HighlightSegmentCollection
    ///
    /// 使用示例：
    /// <![CDATA[
    /// <!-- 单个高亮 -->
    /// <TextBlock Text="这是一段包含关键词的文本"
    ///            attach:TextBlockHighlightAttachedProperty.HighlightText="关键词"
    ///            attach:TextBlockHighlightAttachedProperty.HighlightForeground="Red" />
    ///
    /// <!-- 多个高亮 -->
    /// <TextBlock Text="这是一段包含多个关键词的文本"
    ///            attach:TextBlockHighlightAttachedProperty.HighlightSegments="{Binding HighlightWords}" />
    /// ]]>
    /// </summary>
    public static class TextBlockHighlightAttachedProperty
    {
        #region Dependency Properties

        /// <summary>
        /// 需要高亮的文本内容（单个文本）
        /// </summary>
        public static readonly DependencyProperty HighlightTextProperty =
            DependencyProperty.RegisterAttached(
                "HighlightText",
                typeof(string),
                typeof(TextBlockHighlightAttachedProperty),
                new PropertyMetadata(null, OnHighlightPropertyChanged)
            );

        /// <summary>
        /// 高亮文本的前景色（对应 HighlightText）
        /// </summary>
        public static readonly DependencyProperty HighlightForegroundProperty =
            DependencyProperty.RegisterAttached(
                "HighlightForeground",
                typeof(Brush),
                typeof(TextBlockHighlightAttachedProperty),
                new PropertyMetadata(
                    new SolidColorBrush(Color.FromArgb(255, 0xEA, 0xB1, 0x81)),
                    OnHighlightPropertyChanged
                )
            );

        /// <summary>
        /// 高亮文本列表（可以是 List&lt;string&gt; 或 HighlightSegmentCollection）
        /// </summary>
        public static readonly DependencyProperty HighlightSegmentsProperty =
            DependencyProperty.RegisterAttached(
                "HighlightSegments",
                typeof(object),
                typeof(TextBlockHighlightAttachedProperty),
                new PropertyMetadata(null, OnHighlightPropertyChanged)
            );

        /// <summary>
        /// 标记是否已附加弱事件监听器
        /// </summary>
        private static readonly DependencyProperty WeakEventAttachedProperty =
            DependencyProperty.RegisterAttached(
                "WeakEventAttached",
                typeof(bool),
                typeof(TextBlockHighlightAttachedProperty),
                new PropertyMetadata(false)
            );

        #endregion Dependency Properties

        #region Getters and Setters

        public static string GetHighlightText(DependencyObject obj) =>
            (string)obj.GetValue(HighlightTextProperty);

        public static void SetHighlightText(DependencyObject obj, string value) =>
            obj.SetValue(HighlightTextProperty, value);

        public static Brush GetHighlightForeground(DependencyObject obj) =>
            (Brush)obj.GetValue(HighlightForegroundProperty);

        public static void SetHighlightForeground(DependencyObject obj, Brush value) =>
            obj.SetValue(HighlightForegroundProperty, value);

        public static object GetHighlightSegments(DependencyObject obj) =>
            obj.GetValue(HighlightSegmentsProperty);

        public static void SetHighlightSegments(DependencyObject obj, object value) =>
            obj.SetValue(HighlightSegmentsProperty, value);

        private static bool GetWeakEventAttached(DependencyObject obj) =>
            (bool)obj.GetValue(WeakEventAttachedProperty);

        private static void SetWeakEventAttached(DependencyObject obj, bool value) =>
            obj.SetValue(WeakEventAttachedProperty, value);

        #endregion Getters and Setters

        #region Property Changed Handlers

        /// <summary>
        /// 统一处理所有高亮相关属性的变化
        /// </summary>
        private static void OnHighlightPropertyChanged(
            DependencyObject d,
            DependencyPropertyChangedEventArgs e
        )
        {
            if (d is not TextBlock textBlock)
                return;

            // 附加弱事件监听器（只需一次）
            if (!GetWeakEventAttached(textBlock))
            {
                SetWeakEventAttached(textBlock, true);
                AttachWeakEventListeners(textBlock);
            }

            // 应用高亮
            ApplyHighlightingDelayed(textBlock);
        }

        /// <summary>
        /// 附加弱事件监听器
        /// </summary>
        private static void AttachWeakEventListeners(TextBlock textBlock)
        {
            // 使用弱事件监听 Loaded 事件
            WeakEventManager<FrameworkElement, RoutedEventArgs>.AddHandler(
                textBlock,
                nameof(FrameworkElement.Loaded),
                OnTextBlockLoaded
            );

            // 使用弱事件监听 LayoutUpdated 事件（Text 属性变化时会触发）
            WeakEventManager<UIElement, EventArgs>.AddHandler(
                textBlock,
                nameof(UIElement.LayoutUpdated),
                OnTextBlockLayoutUpdated
            );
        }

        /// <summary>
        /// TextBlock 加载完成事件处理
        /// </summary>
        private static void OnTextBlockLoaded(object sender, RoutedEventArgs e)
        {
            if (sender is TextBlock textBlock)
            {
                ApplyHighlighting(textBlock);
            }
        }

        /// <summary>
        /// TextBlock 布局更新事件处理（用于检测 Text 变化）
        /// </summary>
        private static void OnTextBlockLayoutUpdated(object sender, EventArgs e)
        {
            if (sender is TextBlock textBlock)
            {
                ApplyHighlighting(textBlock);
            }
        }

        #endregion Property Changed Handlers

        #region Highlighting Logic

        /// <summary>
        /// 延迟应用高亮（处理 TextBlock 未加载的情况）
        /// </summary>
        private static void ApplyHighlightingDelayed(TextBlock textBlock)
        {
            if (textBlock.IsLoaded)
            {
                ApplyHighlighting(textBlock);
            }
            // 如果未加载，Loaded 事件会触发 ApplyHighlighting
        }

        /// <summary>
        /// 应用高亮效果
        /// </summary>
        private static void ApplyHighlighting(TextBlock textBlock)
        {
            if (textBlock == null || string.IsNullOrEmpty(textBlock.Text))
            {
                return;
            }

            // 获取高亮配置
            var segments = GetHighlightSegments(textBlock);
            var highlightText = GetHighlightText(textBlock);
            var highlightForeground = GetHighlightForeground(textBlock);

            // 构建高亮片段集合
            HighlightSegmentCollection highlightCollection = null;

            if (segments != null)
            {
                highlightCollection = ConvertToSegments(segments);
            }
            else if (!string.IsNullOrEmpty(highlightText))
            {
                highlightCollection = new HighlightSegmentCollection
                {
                    new HighlightSegment { Text = highlightText, Foreground = highlightForeground },
                };
            }

            // 应用 TextEffect
            ApplyTextEffects(textBlock, highlightCollection);
        }

        /// <summary>
        /// 应用 TextEffect 高亮效果
        /// </summary>
        private static void ApplyTextEffects(
            TextBlock textBlock,
            HighlightSegmentCollection segments
        )
        {
            if (segments == null || segments.Count == 0)
            {
                textBlock.TextEffects.Clear();
                return;
            }

            try
            {
                // 清空现有的文本效果
                textBlock.TextEffects.Clear();

                string text = textBlock.Text;

                // 为每个高亮片段添加文本效果
                foreach (var segment in segments)
                {
                    if (string.IsNullOrEmpty(segment.Text))
                        continue;

                    // 查找所有匹配的位置
                    int startIndex = 0;
                    while (startIndex < text.Length)
                    {
                        int index = text.IndexOf(
                            segment.Text,
                            startIndex,
                            StringComparison.Ordinal
                        );
                        if (index == -1)
                            break;

                        // 创建文本效果
                        var textEffect = new TextEffect
                        {
                            Foreground = segment.Foreground,
                            PositionStart = index,
                            PositionCount = segment.Text.Length,
                        };

                        textBlock.TextEffects.Add(textEffect);

                        startIndex = index + segment.Text.Length;
                    }
                }
            }
            catch
            {
                // 忽略高亮应用错误
            }
        }

        /// <summary>
        /// 将 List&lt;string&gt; 或 HighlightSegmentCollection 转换为 HighlightSegmentCollection
        /// </summary>
        private static HighlightSegmentCollection ConvertToSegments(object data)
        {
            if (data == null)
                return null;

            // 如果已经是 HighlightSegmentCollection，直接返回
            if (data is HighlightSegmentCollection segments)
                return segments;

            // 如果是 List<string>，转换为 HighlightSegmentCollection
            if (data is List<string> stringList)
            {
                var result = new HighlightSegmentCollection();
                foreach (var text in stringList)
                {
                    if (!string.IsNullOrEmpty(text))
                    {
                        result.Add(new HighlightSegment { Text = text });
                    }
                }
                return result.Count > 0 ? result : null;
            }

            return null;
        }

        #endregion Highlighting Logic
    }
}
