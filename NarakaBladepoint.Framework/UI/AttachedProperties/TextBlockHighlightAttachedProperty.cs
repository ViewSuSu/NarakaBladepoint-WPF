using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        /// <summary>
        /// 需要高亮的文本内容（单个文本）
        /// </summary>
        public static readonly DependencyProperty HighlightTextProperty =
            DependencyProperty.RegisterAttached(
                "HighlightText",
                typeof(string),
                typeof(TextBlockHighlightAttachedProperty),
                new PropertyMetadata(null, OnHighlightTextChanged)
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
                    OnHighlightForegroundChanged
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
            if (d is TextBlock textBlock && e.NewValue is string highlightText && !string.IsNullOrEmpty(highlightText))
            {
                // 附加 Text 属性变化监听（只需一次）
                if (!IsTextChangedListenerAttached(textBlock))
                {
                    SetTextChangedListenerAttached(textBlock, true);
                    
                    DependencyPropertyDescriptor.FromProperty(TextBlock.TextProperty, typeof(TextBlock))
                        .AddValueChanged(textBlock, (s, args) =>
                        {
                            // 重新应用高亮
                            var currentHighlightText = GetHighlightText(textBlock);
                            if (!string.IsNullOrEmpty(currentHighlightText))
                            {
                                var segments = new HighlightSegmentCollection
                                {
                                    new HighlightSegment 
                                    { 
                                        Text = currentHighlightText,
                                        Foreground = GetHighlightForeground(textBlock)
                                    }
                                };
                                ApplyHighlighting(textBlock, segments);
                            }
                        });
                }

                // 如果 TextBlock 还没加载，监听加载事件
                if (!textBlock.IsLoaded)
                {
                    RoutedEventHandler loadedHandler = null;
                    loadedHandler = (s, args) =>
                    {
                        textBlock.Loaded -= loadedHandler;
                        var segments = new HighlightSegmentCollection
                        {
                            new HighlightSegment 
                            { 
                                Text = highlightText,
                                Foreground = GetHighlightForeground(textBlock)
                            }
                        };
                        ApplyHighlighting(textBlock, segments);
                    };
                    textBlock.Loaded += loadedHandler;
                }
                else
                {
                    // TextBlock 已加载，直接应用
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
        }

        private static void OnHighlightForegroundChanged(
            DependencyObject d,
            DependencyPropertyChangedEventArgs e
        )
        {
            if (d is TextBlock textBlock && e.NewValue is Brush newBrush)
            {
                string highlightText = GetHighlightText(textBlock);
                if (!string.IsNullOrEmpty(highlightText))
                {
                    // 如果 TextBlock 还没加载，监听加载事件
                    if (!textBlock.IsLoaded)
                    {
                        RoutedEventHandler loadedHandler = null;
                        loadedHandler = (s, args) =>
                        {
                            textBlock.Loaded -= loadedHandler;
                            var segments = new HighlightSegmentCollection
                            {
                                new HighlightSegment 
                                { 
                                    Text = highlightText,
                                    Foreground = newBrush
                                }
                            };
                            ApplyHighlighting(textBlock, segments);
                        };
                        textBlock.Loaded += loadedHandler;
                    }
                    else
                    {
                        var segments = new HighlightSegmentCollection
                        {
                            new HighlightSegment 
                            { 
                                Text = highlightText,
                                Foreground = newBrush
                            }
                        };
                        ApplyHighlighting(textBlock, segments);
                    }
                }
            }
        }

        private static void OnHighlightSegmentsChanged(
            DependencyObject d,
            DependencyPropertyChangedEventArgs e
        )
        {
            if (d is TextBlock textBlock && e.NewValue != null)
            {
                // 附加 Text 属性变化监听（只需一次）
                if (!IsTextChangedListenerAttached(textBlock))
                {
                    SetTextChangedListenerAttached(textBlock, true);
                    
                    DependencyPropertyDescriptor.FromProperty(TextBlock.TextProperty, typeof(TextBlock))
                        .AddValueChanged(textBlock, (s, args) =>
                        {
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
                    RoutedEventHandler loadedHandler = null;
                    loadedHandler = (s, args) =>
                    {
                        textBlock.Loaded -= loadedHandler;
                        ApplyHighlighting(textBlock, ConvertToSegments(e.NewValue));
                    };
                    textBlock.Loaded += loadedHandler;
                }
                else
                {
                    // TextBlock 已加载，直接应用
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
                typeof(TextBlockHighlightAttachedProperty),
                new PropertyMetadata(false)
            );

        private static bool IsTextChangedListenerAttached(DependencyObject obj) =>
            (bool)obj.GetValue(TextChangedListenerAttachedProperty);

        private static void SetTextChangedListenerAttached(DependencyObject obj, bool value) =>
            obj.SetValue(TextChangedListenerAttachedProperty, value);

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

        /// <summary>
        /// 应用高亮效果（使用 TextEffect）
        /// </summary>
        private static void ApplyHighlighting(TextBlock textBlock, HighlightSegmentCollection segments)
        {
            if (textBlock == null)
            {
                return;
            }

            if (segments == null || segments.Count == 0)
            {
                textBlock.TextEffects.Clear();
                return;
            }

            if (string.IsNullOrEmpty(textBlock.Text))
            {
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

                        startIndex = index + segment.Text.Length;
                    }
                }
            }
            catch
            {
                // 忽略高亮应用错误
            }
        }
    }
}
