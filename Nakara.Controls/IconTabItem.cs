using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Nakara.Controls
{
    public class IconTabItem : TabItem
    {
        static IconTabItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(IconTabItem),
                new FrameworkPropertyMetadata(typeof(IconTabItem))
            );
        }

        #region 依赖属性

        #region Icon

        public static readonly DependencyProperty IconProperty = DependencyProperty.Register(
            "Icon",
            typeof(ImageSource),
            typeof(IconTabItem),
            new FrameworkPropertyMetadata(
                null,
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault
            )
        );

        public ImageSource Icon
        {
            get => (ImageSource)GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }

        #endregion Icon

        #region IconWidth

        public static readonly DependencyProperty IconWidthProperty = DependencyProperty.Register(
            "IconWidth",
            typeof(double),
            typeof(IconTabItem),
            new FrameworkPropertyMetadata(30.0)
        );

        public double IconWidth
        {
            get => (double)GetValue(IconWidthProperty);
            set => SetValue(IconWidthProperty, value);
        }

        #endregion IconWidth

        #region IconHeight

        public static readonly DependencyProperty IconHeightProperty = DependencyProperty.Register(
            "IconHeight",
            typeof(double),
            typeof(IconTabItem),
            new FrameworkPropertyMetadata(30.0)
        );

        public double IconHeight
        {
            get => (double)GetValue(IconHeightProperty);
            set => SetValue(IconHeightProperty, value);
        }

        #endregion IconHeight

        #region TextSpacing

        public static readonly DependencyProperty TextSpacingProperty = DependencyProperty.Register(
            "TextSpacing",
            typeof(double),
            typeof(IconTabItem),
            new FrameworkPropertyMetadata(4.0)
        );

        public double TextSpacing
        {
            get => (double)GetValue(TextSpacingProperty);
            set => SetValue(TextSpacingProperty, value);
        }

        #endregion TextSpacing

        #region BackgroundHeightFactor

        public static readonly DependencyProperty BackgroundHeightFactorProperty =
            DependencyProperty.Register(
                "BackgroundHeightFactor",
                typeof(double),
                typeof(IconTabItem),
                new FrameworkPropertyMetadata(1.2)
            );

        public double BackgroundHeightFactor
        {
            get => (double)GetValue(BackgroundHeightFactorProperty);
            set => SetValue(BackgroundHeightFactorProperty, value);
        }

        #endregion BackgroundHeightFactor

        #region HeaderFontSize

        public static readonly DependencyProperty HeaderFontSizeProperty =
            DependencyProperty.Register("HeaderFontSize", typeof(double), typeof(IconTabItem));

        public double HeaderFontSize
        {
            get => (double)GetValue(HeaderFontSizeProperty);
            set => SetValue(HeaderFontSizeProperty, value);
        }

        #endregion HeaderFontSize

        #region HeaderForeground

        public static readonly DependencyProperty HeaderForegroundProperty =
            DependencyProperty.Register(
                "HeaderForeground",
                typeof(Brush),
                typeof(IconTabItem),
                new FrameworkPropertyMetadata(Brushes.White)
            );

        public Brush HeaderForeground
        {
            get => (Brush)GetValue(HeaderForegroundProperty);
            set => SetValue(HeaderForegroundProperty, value);
        }

        #endregion HeaderForeground

        #endregion 依赖属性
    }
}
