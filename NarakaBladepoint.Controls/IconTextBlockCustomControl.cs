using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace NarakaBladepoint.Controls
{
    public class IconTextBlockCustomControl : Control
    {
        static IconTextBlockCustomControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(IconTextBlockCustomControl),
                new FrameworkPropertyMetadata(typeof(IconTextBlockCustomControl))
            );
        }

        #region Icon（图片）

        public ImageSource Icon
        {
            get => (ImageSource)GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }

        public static readonly DependencyProperty IconProperty = DependencyProperty.Register(
            nameof(Icon),
            typeof(ImageSource),
            typeof(IconTextBlockCustomControl),
            new PropertyMetadata(null)
        );

        #endregion Icon（图片）

        #region Text（文本）

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            nameof(Text),
            typeof(string),
            typeof(IconTextBlockCustomControl),
            new PropertyMetadata(string.Empty)
        );

        #endregion Text（文本）

        #region Command（鼠标左键命令）

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(
            nameof(Command),
            typeof(ICommand),
            typeof(IconTextBlockCustomControl),
            new PropertyMetadata(null)
        );

        #endregion Command（鼠标左键命令）

        #region IconWidth（图片宽度）

        public double IconWidth
        {
            get => (double)GetValue(IconWidthProperty);
            set => SetValue(IconWidthProperty, value);
        }

        public static readonly DependencyProperty IconWidthProperty = DependencyProperty.Register(
            nameof(IconWidth),
            typeof(double),
            typeof(IconTextBlockCustomControl),
            new PropertyMetadata(30.0)
        );

        #endregion IconWidth（图片宽度）

        #region IconHeight（图片高度）

        public double IconHeight
        {
            get => (double)GetValue(IconHeightProperty);
            set => SetValue(IconHeightProperty, value);
        }

        public static readonly DependencyProperty IconHeightProperty = DependencyProperty.Register(
            nameof(IconHeight),
            typeof(double),
            typeof(IconTextBlockCustomControl),
            new PropertyMetadata(30.0)
        );

        #endregion IconHeight（图片高度）

        #region IconStretch（图片拉伸方式）

        public Stretch IconStretch
        {
            get => (Stretch)GetValue(IconStretchProperty);
            set => SetValue(IconStretchProperty, value);
        }

        public static readonly DependencyProperty IconStretchProperty = DependencyProperty.Register(
            nameof(IconStretch),
            typeof(Stretch),
            typeof(IconTextBlockCustomControl),
            new PropertyMetadata(Stretch.Uniform)
        );

        #endregion IconStretch（图片拉伸方式）

        #region TextFontSize（文本字体大小）

        public double TextFontSize
        {
            get => (double)GetValue(TextFontSizeProperty);
            set => SetValue(TextFontSizeProperty, value);
        }

        public static readonly DependencyProperty TextFontSizeProperty =
            DependencyProperty.Register(
                nameof(TextFontSize),
                typeof(double),
                typeof(IconTextBlockCustomControl),
                new PropertyMetadata(10.0)
            );

        #endregion TextFontSize（文本字体大小）

        #region TextForeground（文本前景色）

        public Brush TextForeground
        {
            get => (Brush)GetValue(TextForegroundProperty);
            set => SetValue(TextForegroundProperty, value);
        }

        public static readonly DependencyProperty TextForegroundProperty =
            DependencyProperty.Register(
                nameof(TextForeground),
                typeof(Brush),
                typeof(IconTextBlockCustomControl),
                new PropertyMetadata(Brushes.White)
            );

        #endregion TextForeground（文本前景色）

        #region Spacing（图片和文本间距）

        public double Spacing
        {
            get => (double)GetValue(SpacingProperty);
            set => SetValue(SpacingProperty, value);
        }

        public static readonly DependencyProperty SpacingProperty = DependencyProperty.Register(
            nameof(Spacing),
            typeof(double),
            typeof(IconTextBlockCustomControl),
            new PropertyMetadata(0.0, OnSpacingChanged)
        );

        private static void OnSpacingChanged(
            DependencyObject d,
            DependencyPropertyChangedEventArgs e
        )
        {
            var control = d as IconTextBlockCustomControl;
            if (control != null)
            {
                // 更新TextMargin，只设置上边距
                control.TextMargin = new Thickness(0, (double)e.NewValue, 0, 0);
            }
        }

        #endregion Spacing（图片和文本间距）

        #region TextMargin（文本边距）

        public Thickness TextMargin
        {
            get => (Thickness)GetValue(TextMarginProperty);
            set => SetValue(TextMarginProperty, value);
        }

        public static readonly DependencyProperty TextMarginProperty = DependencyProperty.Register(
            nameof(TextMargin),
            typeof(Thickness),
            typeof(IconTextBlockCustomControl),
            new PropertyMetadata(new Thickness(0))
        );

        #endregion TextMargin（文本边距）

        #region HorizontalContentAlignment（水平对齐方式）

        public HorizontalAlignment HorizontalContentAlignment
        {
            get => (HorizontalAlignment)GetValue(HorizontalContentAlignmentProperty);
            set => SetValue(HorizontalContentAlignmentProperty, value);
        }

        public static readonly DependencyProperty HorizontalContentAlignmentProperty =
            DependencyProperty.Register(
                nameof(HorizontalContentAlignment),
                typeof(HorizontalAlignment),
                typeof(IconTextBlockCustomControl),
                new PropertyMetadata(HorizontalAlignment.Center)
            );

        #endregion HorizontalContentAlignment（水平对齐方式）

        #region VerticalContentAlignment（垂直对齐方式）

        public VerticalAlignment VerticalContentAlignment
        {
            get => (VerticalAlignment)GetValue(VerticalContentAlignmentProperty);
            set => SetValue(VerticalContentAlignmentProperty, value);
        }

        public static readonly DependencyProperty VerticalContentAlignmentProperty =
            DependencyProperty.Register(
                nameof(VerticalContentAlignment),
                typeof(VerticalAlignment),
                typeof(IconTextBlockCustomControl),
                new PropertyMetadata(VerticalAlignment.Center)
            );

        #endregion VerticalContentAlignment（垂直对齐方式）

        #region Padding（内边距）

        public Thickness Padding
        {
            get => (Thickness)GetValue(PaddingProperty);
            set => SetValue(PaddingProperty, value);
        }

        public static readonly DependencyProperty PaddingProperty = DependencyProperty.Register(
            nameof(Padding),
            typeof(Thickness),
            typeof(IconTextBlockCustomControl),
            new PropertyMetadata(new Thickness(0))
        );

        #endregion Padding（内边距）
    }
}
