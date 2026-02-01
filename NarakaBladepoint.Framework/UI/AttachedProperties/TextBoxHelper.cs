using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace NarakaBladepoint.Framework.UI.AttachedProperties
{
    public static class TextBoxHelper
    {
        private class PlaceholderAdorner : Adorner
        {
            private readonly TextBox _textBox;

            public PlaceholderAdorner(TextBox textBox)
                : base(textBox)
            {
                _textBox = textBox;
            }

            protected override void OnRender(DrawingContext drawingContext)
            {
                base.OnRender(drawingContext);

                string placeholderValue = TextBoxHelper.GetPlaceholder(_textBox);
                if (string.IsNullOrEmpty(placeholderValue))
                    return;

                // 获取占位符样式属性
                Brush placeholderForeground =
                    TextBoxHelper.GetPlaceholderForeground(_textBox)
                    ?? SystemColors.InactiveCaptionBrush;
                Brush placeholderBrush =
                    TextBoxHelper.GetPlaceholderBrush(_textBox)
                    ?? placeholderForeground;
                double placeholderFontSize =
                    TextBoxHelper.GetPlaceholderFontSize(_textBox) ?? _textBox.FontSize;
                FontStyle placeholderFontStyle =
                    TextBoxHelper.GetPlaceholderFontStyle(_textBox) ?? _textBox.FontStyle;
                FontWeight placeholderFontWeight =
                    TextBoxHelper.GetPlaceholderFontWeight(_textBox) ?? _textBox.FontWeight;
                FontFamily placeholderFontFamily =
                    TextBoxHelper.GetPlaceholderFontFamily(_textBox) ?? _textBox.FontFamily;
                TextAlignment placeholderTextAlignment =
                    TextBoxHelper.GetPlaceholderTextAlignment(_textBox) ?? TextAlignment.Left;

                // 创建格式化文本对象
                FormattedText text = new FormattedText(
                    placeholderValue,
                    System.Globalization.CultureInfo.CurrentCulture,
                    _textBox.FlowDirection,
                    new Typeface(
                        placeholderFontFamily,
                        placeholderFontStyle,
                        placeholderFontWeight,
                        _textBox.FontStretch
                    ),
                    placeholderFontSize,
                    placeholderBrush,
                    VisualTreeHelper.GetDpi(_textBox).PixelsPerDip
                );

                // 计算文本的渲染位置 - 垂直居中，从左开始
                double textWidth = text.Width;
                double textHeight = text.Height;

                // 获取TextBox的可用区域
                double availableWidth = Math.Max(
                    _textBox.ActualWidth - _textBox.Padding.Left - _textBox.Padding.Right,
                    0
                );
                double availableHeight = Math.Max(
                    _textBox.ActualHeight - _textBox.Padding.Top - _textBox.Padding.Bottom,
                    0
                );

                // 计算垂直居中位置
                double verticalOffset = _textBox.Padding.Top + (availableHeight - textHeight) / 2;

                // 如果PART_ContentHost存在，则基于它计算位置
                if (
                    _textBox.Template.FindName("PART_ContentHost", _textBox)
                    is FrameworkElement contentHost
                )
                {
                    Point contentHostPosition = contentHost
                        .TransformToAncestor(_textBox)
                        .Transform(new Point(0, 0));

                    availableWidth = Math.Max(contentHost.ActualWidth - contentHostPosition.X, 0);
                    availableHeight = Math.Max(contentHost.ActualHeight - contentHostPosition.Y, 0);

                    verticalOffset = contentHostPosition.Y + (availableHeight - textHeight) / 2;
                }

                // 确保垂直偏移不会超出边界
                verticalOffset = Math.Max(verticalOffset, _textBox.Padding.Top);
                verticalOffset = Math.Min(
                    verticalOffset,
                    _textBox.ActualHeight - textHeight - _textBox.Padding.Bottom
                );

                // 根据文本对齐方式计算水平偏移
                double horizontalOffset = placeholderTextAlignment switch
                {
                    TextAlignment.Center => _textBox.Padding.Left + (availableWidth - textWidth) / 2,
                    TextAlignment.Right => Math.Max(_textBox.ActualWidth - _textBox.Padding.Right - textWidth, _textBox.Padding.Left),
                    _ => _textBox.Padding.Left
                };

                // 限制文本宽度，防止超出TextBox
                text.MaxTextWidth = Math.Max(availableWidth - horizontalOffset, 0);
                text.Trimming = TextTrimming.CharacterEllipsis;

                // 渲染文本
                drawingContext.DrawText(text, new Point(horizontalOffset, verticalOffset));
            }
        }

        #region Placeholder Property

        public static string GetPlaceholder(DependencyObject obj) =>
            (string)obj.GetValue(PlaceholderProperty);

        public static void SetPlaceholder(DependencyObject obj, string value) =>
            obj.SetValue(PlaceholderProperty, value);

        public static readonly DependencyProperty PlaceholderProperty =
            DependencyProperty.RegisterAttached(
                "Placeholder",
                typeof(string),
                typeof(TextBoxHelper),
                new FrameworkPropertyMetadata(
                    defaultValue: null,
                    FrameworkPropertyMetadataOptions.AffectsRender,
                    propertyChangedCallback: OnPlaceholderChanged
                )
            );

        #endregion Placeholder Property

        #region PlaceholderBrush Property

        public static Brush GetPlaceholderBrush(DependencyObject obj) =>
            (Brush)obj.GetValue(PlaceholderBrushProperty);

        public static void SetPlaceholderBrush(DependencyObject obj, Brush value) =>
            obj.SetValue(PlaceholderBrushProperty, value);

        public static readonly DependencyProperty PlaceholderBrushProperty =
            DependencyProperty.RegisterAttached(
                "PlaceholderBrush",
                typeof(Brush),
                typeof(TextBoxHelper),
                new FrameworkPropertyMetadata(
                    defaultValue: null,
                    FrameworkPropertyMetadataOptions.AffectsRender,
                    propertyChangedCallback: OnPlaceholderStyleChanged
                )
            );

        #endregion PlaceholderBrush Property

        #region PlaceholderForeground Property

        public static Brush GetPlaceholderForeground(DependencyObject obj) =>
            (Brush)obj.GetValue(PlaceholderForegroundProperty);

        public static void SetPlaceholderForeground(DependencyObject obj, Brush value) =>
            obj.SetValue(PlaceholderForegroundProperty, value);

        public static readonly DependencyProperty PlaceholderForegroundProperty =
            DependencyProperty.RegisterAttached(
                "PlaceholderForeground",
                typeof(Brush),
                typeof(TextBoxHelper),
                new FrameworkPropertyMetadata(
                    defaultValue: null,
                    FrameworkPropertyMetadataOptions.AffectsRender,
                    propertyChangedCallback: OnPlaceholderStyleChanged
                )
            );

        #endregion PlaceholderForeground Property

        #region PlaceholderFontSize Property

        public static double? GetPlaceholderFontSize(DependencyObject obj) =>
            (double?)obj.GetValue(PlaceholderFontSizeProperty);

        public static void SetPlaceholderFontSize(DependencyObject obj, double? value) =>
            obj.SetValue(PlaceholderFontSizeProperty, value);

        public static readonly DependencyProperty PlaceholderFontSizeProperty =
            DependencyProperty.RegisterAttached(
                "PlaceholderFontSize",
                typeof(double?),
                typeof(TextBoxHelper),
                new FrameworkPropertyMetadata(
                    defaultValue: null,
                    FrameworkPropertyMetadataOptions.AffectsRender,
                    propertyChangedCallback: OnPlaceholderStyleChanged
                )
            );

        #endregion PlaceholderFontSize Property

        #region PlaceholderFontStyle Property

        public static FontStyle? GetPlaceholderFontStyle(DependencyObject obj) =>
            (FontStyle?)obj.GetValue(PlaceholderFontStyleProperty);

        public static void SetPlaceholderFontStyle(DependencyObject obj, FontStyle? value) =>
            obj.SetValue(PlaceholderFontStyleProperty, value);

        public static readonly DependencyProperty PlaceholderFontStyleProperty =
            DependencyProperty.RegisterAttached(
                "PlaceholderFontStyle",
                typeof(FontStyle?),
                typeof(TextBoxHelper),
                new FrameworkPropertyMetadata(
                    defaultValue: null,
                    FrameworkPropertyMetadataOptions.AffectsRender,
                    propertyChangedCallback: OnPlaceholderStyleChanged
                )
            );

        #endregion PlaceholderFontStyle Property

        #region PlaceholderFontWeight Property

        public static FontWeight? GetPlaceholderFontWeight(DependencyObject obj) =>
            (FontWeight?)obj.GetValue(PlaceholderFontWeightProperty);

        public static void SetPlaceholderFontWeight(DependencyObject obj, FontWeight? value) =>
            obj.SetValue(PlaceholderFontWeightProperty, value);

        public static readonly DependencyProperty PlaceholderFontWeightProperty =
            DependencyProperty.RegisterAttached(
                "PlaceholderFontWeight",
                typeof(FontWeight?),
                typeof(TextBoxHelper),
                new FrameworkPropertyMetadata(
                    defaultValue: null,
                    FrameworkPropertyMetadataOptions.AffectsRender,
                    propertyChangedCallback: OnPlaceholderStyleChanged
                )
            );

        #endregion PlaceholderFontWeight Property

        #region PlaceholderFontFamily Property

        public static FontFamily GetPlaceholderFontFamily(DependencyObject obj) =>
            (FontFamily)obj.GetValue(PlaceholderFontFamilyProperty);

        public static void SetPlaceholderFontFamily(DependencyObject obj, FontFamily value) =>
            obj.SetValue(PlaceholderFontFamilyProperty, value);

        public static readonly DependencyProperty PlaceholderFontFamilyProperty =
            DependencyProperty.RegisterAttached(
                "PlaceholderFontFamily",
                typeof(FontFamily),
                typeof(TextBoxHelper),
                new FrameworkPropertyMetadata(
                    defaultValue: null,
                    FrameworkPropertyMetadataOptions.AffectsRender,
                    propertyChangedCallback: OnPlaceholderStyleChanged
                )
            );

        #endregion PlaceholderFontFamily Property

        #region PlaceholderTextAlignment Property

        public static TextAlignment? GetPlaceholderTextAlignment(DependencyObject obj) =>
            (TextAlignment?)obj.GetValue(PlaceholderTextAlignmentProperty);

        public static void SetPlaceholderTextAlignment(DependencyObject obj, TextAlignment? value) =>
            obj.SetValue(PlaceholderTextAlignmentProperty, value);

        public static readonly DependencyProperty PlaceholderTextAlignmentProperty =
            DependencyProperty.RegisterAttached(
                "PlaceholderTextAlignment",
                typeof(TextAlignment?),
                typeof(TextBoxHelper),
                new FrameworkPropertyMetadata(
                    defaultValue: null,
                    FrameworkPropertyMetadataOptions.AffectsRender,
                    propertyChangedCallback: OnPlaceholderStyleChanged
                )
            );

        #endregion PlaceholderTextAlignment Property

        private static void OnPlaceholderChanged(
            DependencyObject d,
            DependencyPropertyChangedEventArgs e
        )
        {
            if (d is TextBox textBoxControl)
            {
                AttachEvents(textBoxControl);
                UpdateAdornerVisibility(textBoxControl);
            }
        }

        private static void OnPlaceholderStyleChanged(
            DependencyObject d,
            DependencyPropertyChangedEventArgs e
        )
        {
            if (
                d is TextBox textBoxControl
                && GetOrCreateAdorner(textBoxControl, out PlaceholderAdorner adorner)
            )
            {
                adorner.InvalidateVisual();
            }
        }

        /// <summary>
        /// 标记是否已附加弱事件监听器
        /// </summary>
        private static readonly DependencyProperty WeakEventAttachedProperty =
            DependencyProperty.RegisterAttached(
                "WeakEventAttached",
                typeof(bool),
                typeof(TextBoxHelper),
                new PropertyMetadata(false)
            );

        private static bool GetWeakEventAttached(DependencyObject obj) =>
            (bool)obj.GetValue(WeakEventAttachedProperty);

        private static void SetWeakEventAttached(DependencyObject obj, bool value) =>
            obj.SetValue(WeakEventAttachedProperty, value);

        private static void AttachEvents(TextBox textBoxControl)
        {
            // 只附加一次弱事件监听器
            if (GetWeakEventAttached(textBoxControl))
            {
                return;
            }

            SetWeakEventAttached(textBoxControl, true);

            // 使用弱引用监听 Loaded 事件
            WeakEventManager<FrameworkElement, RoutedEventArgs>.AddHandler(
                textBoxControl,
                nameof(FrameworkElement.Loaded),
                TextBoxControl_Loaded
            );

            // 使用弱引用监听 TextChanged 事件
            WeakEventManager<TextBox, TextChangedEventArgs>.AddHandler(
                textBoxControl,
                nameof(TextBox.TextChanged),
                TextBoxControl_TextChanged
            );

            // 使用弱引用监听 SizeChanged 事件
            WeakEventManager<FrameworkElement, SizeChangedEventArgs>.AddHandler(
                textBoxControl,
                nameof(FrameworkElement.SizeChanged),
                TextBoxControl_SizeChanged
            );

            // 确保初始状态正确
            UpdateAdornerVisibility(textBoxControl);
        }

        private static void TextBoxControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBoxControl)
            {
                GetOrCreateAdorner(textBoxControl, out _);
                UpdateAdornerVisibility(textBoxControl);
            }
        }

        private static void TextBoxControl_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBoxControl)
            {
                UpdateAdornerVisibility(textBoxControl);
            }
        }

        private static void TextBoxControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (
                sender is TextBox textBoxControl
                && GetOrCreateAdorner(textBoxControl, out PlaceholderAdorner adorner)
            )
            {
                adorner.InvalidateVisual();
            }
        }

        private static void UpdateAdornerVisibility(TextBox textBoxControl)
        {
            if (GetOrCreateAdorner(textBoxControl, out PlaceholderAdorner adorner))
            {
                // 控制有文本时隐藏占位符，无文本时显示
                if (!string.IsNullOrEmpty(textBoxControl.Text))
                    adorner.Visibility = Visibility.Hidden;
                else
                    adorner.Visibility = Visibility.Visible;
            }
        }

        private static bool GetOrCreateAdorner(
            TextBox textBoxControl,
            out PlaceholderAdorner adorner
        )
        {
            // 获取装饰层
            AdornerLayer layer = AdornerLayer.GetAdornerLayer(textBoxControl);

            // 如果装饰层为空，控件模板可能未加载
            if (layer == null)
            {
                adorner = null;
                return false;
            }

            // 查找现有的占位符装饰器
            adorner = layer
                .GetAdorners(textBoxControl)
                ?.OfType<PlaceholderAdorner>()
                .FirstOrDefault();

            // 如果不存在，创建并添加新的装饰器
            if (adorner == null)
            {
                adorner = new PlaceholderAdorner(textBoxControl);
                layer.Add(adorner);
            }

            return true;
        }
    }
}
