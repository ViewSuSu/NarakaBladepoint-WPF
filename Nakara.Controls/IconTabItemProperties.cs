using System.Windows;
using System.Windows.Media;

namespace Nakara.Controls
{
    public static class IconTabItemProperties
    {
        // 图标
        public static readonly DependencyProperty IconProperty =
            DependencyProperty.RegisterAttached(
                "Icon",
                typeof(ImageSource),
                typeof(IconTabItemProperties),
                new FrameworkPropertyMetadata(
                    null,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault
                )
            );

        public static void SetIcon(DependencyObject obj, ImageSource value) =>
            obj.SetValue(IconProperty, value);

        public static ImageSource GetIcon(DependencyObject obj) =>
            (ImageSource)obj.GetValue(IconProperty);

        // 图标宽度
        public static readonly DependencyProperty IconWidthProperty =
            DependencyProperty.RegisterAttached(
                "IconWidth",
                typeof(double),
                typeof(IconTabItemProperties),
                new FrameworkPropertyMetadata(30.0)
            );

        public static void SetIconWidth(DependencyObject obj, double value) =>
            obj.SetValue(IconWidthProperty, value);

        public static double GetIconWidth(DependencyObject obj) =>
            (double)obj.GetValue(IconWidthProperty);

        // 图标高度
        public static readonly DependencyProperty IconHeightProperty =
            DependencyProperty.RegisterAttached(
                "IconHeight",
                typeof(double),
                typeof(IconTabItemProperties),
                new FrameworkPropertyMetadata(30.0)
            );

        public static void SetIconHeight(DependencyObject obj, double value) =>
            obj.SetValue(IconHeightProperty, value);

        public static double GetIconHeight(DependencyObject obj) =>
            (double)obj.GetValue(IconHeightProperty);

        // 文本与图标间距
        public static readonly DependencyProperty TextSpacingProperty =
            DependencyProperty.RegisterAttached(
                "TextSpacing",
                typeof(double),
                typeof(IconTabItemProperties),
                new FrameworkPropertyMetadata(4.0)
            );

        public static void SetTextSpacing(DependencyObject obj, double value) =>
            obj.SetValue(TextSpacingProperty, value);

        public static double GetTextSpacing(DependencyObject obj) =>
            (double)obj.GetValue(TextSpacingProperty);

        // 背景高度倍数（与 IconHeight 相乘）
        public static readonly DependencyProperty BackgroundHeightFactorProperty =
            DependencyProperty.RegisterAttached(
                "BackgroundHeightFactor",
                typeof(double),
                typeof(IconTabItemProperties),
                new FrameworkPropertyMetadata(1.2)
            );

        public static void SetBackgroundHeightFactor(DependencyObject obj, double value) =>
            obj.SetValue(BackgroundHeightFactorProperty, value);

        public static double GetBackgroundHeightFactor(DependencyObject obj) =>
            (double)obj.GetValue(BackgroundHeightFactorProperty);

        // TextBlock 字体大小
        public static readonly DependencyProperty HeaderFontSizeProperty =
            DependencyProperty.RegisterAttached(
                "HeaderFontSize",
                typeof(double),
                typeof(IconTabItemProperties)
            );

        public static void SetHeaderFontSize(DependencyObject obj, double value) =>
            obj.SetValue(HeaderFontSizeProperty, value);

        public static double GetHeaderFontSize(DependencyObject obj) =>
            (double)obj.GetValue(HeaderFontSizeProperty);

        // TextBlock 前景色
        public static readonly DependencyProperty HeaderForegroundProperty =
            DependencyProperty.RegisterAttached(
                "HeaderForeground",
                typeof(Brush),
                typeof(IconTabItemProperties),
                new FrameworkPropertyMetadata(Brushes.White)
            );

        public static void SetHeaderForeground(DependencyObject obj, Brush value) =>
            obj.SetValue(HeaderForegroundProperty, value);

        public static Brush GetHeaderForeground(DependencyObject obj) =>
            (Brush)obj.GetValue(HeaderForegroundProperty);
    }
}
