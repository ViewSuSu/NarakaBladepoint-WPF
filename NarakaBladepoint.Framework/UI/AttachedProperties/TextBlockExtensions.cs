using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace NarakaBladepoint.Framework.UI.AttachedProperties
{
    /// <summary>
    /// 高亮文本片段
    /// </summary>
    public class HighlightSegmentCollection : List<HighlightSegment> { }

    /// <summary>
    /// 单个高京文本片段
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
    /// TextBlock 高亮扩展 - 使用 TextEffect 实现
    /// 支持 List<string> 或 HighlightSegmentCollection
    /// </summary>
    public static class TextBlockExtensions
    {
        /// <summary>
        /// 需要修改颜色的文本内容（单个文本，向后兼容）
        /// </summary>
        public static readonly DependencyProperty HighlightTextProperty =
            DependencyProperty.RegisterAttached(
                "HighlightText",
                typeof(string),
                typeof(TextBlockExtensions),
                new PropertyMetadata(null, OnHighlightTextChanged)
            );

        /// <summary>
        /// 高亮文本的前景色（对应 HighlightText）
        /// </summary>
        public static readonly DependencyProperty HighlightForegroundProperty =
            DependencyProperty.RegisterAttached(
                "HighlightForeground",
                typeof(Brush),
                typeof(TextBlockExtensions),
                new PropertyMetadata(
                    new SolidColorBrush(Color.FromArgb(255, 0xEA, 0xB1, 0x81)),
                    OnHighlightForegroundChanged
                )
            );

        /// <summary>
        /// 高亮文本列表（可以是 List<string> 或 HighlightSegmentCollection）
        /// </summary>
        public static readonly DependencyProperty HighlightSegmentsProperty =
            DependencyProperty.RegisterAttached(
                "HighlightSegments",
                typeof(object),
                typeof(TextBlockExtensions),
                new PropertyMetadata(null, OnHighlightSegmentsChanged)
            );

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

        private static void OnHighlightTextChanged(
            DependencyObject d,
            DependencyPropertyChangedEventArgs e
        )
        {
            System.Diagnostics.Debug.WriteLine($"OnHighlightTextChanged: oldValue={e.OldValue}, newValue={e.NewValue}");
            
            if (d is TextBlock textBlock && e.NewValue is string highlightText && !string.IsNullOrEmpty(highlightText))
            {
                var segments = new HighlightSegmentCollection
                {
                    new HighlightSegment 
                    { 
                        Text = highlightText,
                        Foreground = GetHighlightForeground(textBlock)
                    }
                };
                ApplyHighlighting(textBlock, segments);
            }
        }

        private static void OnHighlightForegroundChanged(
            DependencyObject d,
            DependencyPropertyChangedEventArgs e
        )
        {
            System.Diagnostics.Debug.WriteLine($"OnHighlightForegroundChanged: oldValue={e.OldValue}, newValue={e.NewValue}");
            
            if (d is TextBlock textBlock && e.NewValue is Brush)
            {
                string highlightText = GetHighlightText(textBlock);
                if (!string.IsNullOrEmpty(highlightText))
                {
                    var segments = new HighlightSegmentCollection
                    {
                        new HighlightSegment 
                        { 
                            Text = highlightText,
                            Foreground = (Brush)e.NewValue
                        }
                    };
                    ApplyHighlighting(textBlock, segments);
                }
            }
        }

        private static void OnHighlightSegmentsChanged(
            DependencyObject d,
            DependencyPropertyChangedEventArgs e
        )
        {
            System.Diagnostics.Debug.WriteLine($"OnHighlightSegmentsChanged: newValue type={e.NewValue?.GetType().Name}, value={e.NewValue}");
            
            if (d is TextBlock textBlock && e.NewValue != null)
            {
                // 附加 Text 属性变化监听（只需一次）
                if (!IsTextChangedListenerAttached(textBlock))
                {
                    SetTextChangedListenerAttached(textBlock, true);
                    
                    System.Diagnostics.Debug.WriteLine("OnHighlightSegmentsChanged: Attaching Text property changed listener");
                    
                    DependencyPropertyDescriptor.FromProperty(TextBlock.TextProperty, typeof(TextBlock))
                        .AddValueChanged(textBlock, (s, args) =>
                        {
                            System.Diagnostics.Debug.WriteLine("OnHighlightSegmentsChanged: Text changed, reapplying highlights");
                            var currentSegments = GetHighlightSegments(textBlock);
                            if (currentSegments != null)
                            {
                                ApplyHighlighting(textBlock, ConvertToSegments(currentSegments));
                            }
                        });
                }

                // 如果 TextBlock 还没加载，监听加载事件
                if (!textBlock.IsLoaded)
                {
                    System.Diagnostics.Debug.WriteLine("OnHighlightSegmentsChanged: TextBlock not loaded yet, waiting for Loaded event");
                    
                    RoutedEventHandler loadedHandler = null;
                    loadedHandler = (s, args) =>
                    {
                        System.Diagnostics.Debug.WriteLine("OnHighlightSegmentsChanged: TextBlock Loaded event triggered");
                        textBlock.Loaded -= loadedHandler;
                        ApplyHighlighting(textBlock, ConvertToSegments(e.NewValue));
                    };
                    textBlock.Loaded += loadedHandler;
                }
                else
                {
                    // TextBlock 已加载，直接应用
                    System.Diagnostics.Debug.WriteLine("OnHighlightSegmentsChanged: TextBlock already loaded, applying immediately");
                    ApplyHighlighting(textBlock, ConvertToSegments(e.NewValue));
                }
            }
        }

        /// <summary>
        /// 标记 TextBlock 的 Text 变化是否已被监听
        /// </summary>
        private static readonly DependencyProperty TextChangedListenerAttachedProperty =
            DependencyProperty.RegisterAttached(
                "TextChangedListenerAttached",
                typeof(bool),
                typeof(TextBlockExtensions),
                new PropertyMetadata(false)
            );

        private static bool IsTextChangedListenerAttached(DependencyObject obj) =>
            (bool)obj.GetValue(TextChangedListenerAttachedProperty);

        private static void SetTextChangedListenerAttached(DependencyObject obj, bool value) =>
            obj.SetValue(TextChangedListenerAttachedProperty, value);

        /// <summary>
        /// 将 List<string> 或 HighlightSegmentCollection 转换为 HighlightSegmentCollection
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

            System.Diagnostics.Debug.WriteLine($"ConvertToSegments: Unsupported type {data.GetType().Name}");
            return null;
        }

        /// <summary>
        /// 应用高亮效果（使用 TextEffect）
        /// </summary>
        private static void ApplyHighlighting(TextBlock textBlock, HighlightSegmentCollection segments)
        {
            if (textBlock == null)
            {
                System.Diagnostics.Debug.WriteLine("ApplyHighlighting: textBlock is null");
                return;
            }

            if (segments == null || segments.Count == 0)
            {
                System.Diagnostics.Debug.WriteLine("ApplyHighlighting: segments is null or empty");
                textBlock.TextEffects.Clear();
                return;
            }

            if (string.IsNullOrEmpty(textBlock.Text))
            {
                System.Diagnostics.Debug.WriteLine("ApplyHighlighting: textBlock.Text is empty");
                return;
            }

            try
            {
                System.Diagnostics.Debug.WriteLine($"ApplyHighlighting: text='{textBlock.Text}', segments count={segments.Count}");

                // 清空现有的文本效果
                textBlock.TextEffects.Clear();

                string text = textBlock.Text;
                int effectCount = 0;

                // 为每个高亮片段添加文本效果
                foreach (var segment in segments)
                {
                    if (string.IsNullOrEmpty(segment.Text))
                        continue;

                    System.Diagnostics.Debug.WriteLine($"  Processing segment: '{segment.Text}'");

                    // 查找所有匹配的位置
                    int startIndex = 0;
                    int matchCount = 0;
                    while (startIndex < text.Length)
                    {
                        int index = text.IndexOf(segment.Text, startIndex, StringComparison.Ordinal);
                        if (index == -1)
                            break;

                        // 创建文本效果
                        var textEffect = new TextEffect
                        {
                            Foreground = segment.Foreground,
                            PositionStart = index,
                            PositionCount = segment.Text.Length
                        };

                        textBlock.TextEffects.Add(textEffect);
                        effectCount++;
                        matchCount++;

                        System.Diagnostics.Debug.WriteLine($"    Added TextEffect at position {index}, length {segment.Text.Length}");

                        startIndex = index + segment.Text.Length;
                    }

                    System.Diagnostics.Debug.WriteLine($"  Segment '{segment.Text}' found {matchCount} times");
                }

                System.Diagnostics.Debug.WriteLine($"ApplyHighlighting completed: {effectCount} TextEffects added");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"ApplyHighlighting error: {ex.Message}\nStackTrace: {ex.StackTrace}");
            }
        }
    }
}
