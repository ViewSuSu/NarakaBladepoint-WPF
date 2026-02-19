using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace NarakaBladepoint.Controls
{
    public class ImageTextTabItem : TabItem
    {
        static ImageTextTabItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(ImageTextTabItem),
                new FrameworkPropertyMetadata(typeof(ImageTextTabItem))
            );
        }

        #region 依赖属性

        public Thickness ContentMargin
        {
            get { return (Thickness)GetValue(ContentMarginProperty); }
            set { SetValue(ContentMarginProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ContentMargin.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ContentMarginProperty =
            DependencyProperty.Register(
                nameof(ContentMargin),
                typeof(Thickness),
                typeof(ImageTextTabItem),
                new PropertyMetadata(new Thickness(0, 0, 0, 0))
            );

        public HorizontalAlignment ContentHorizontalAlignment
        {
            get { return (HorizontalAlignment)GetValue(ContentHorizontalAlignmentProperty); }
            set { SetValue(ContentHorizontalAlignmentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ContentHorizontalAlignment.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ContentHorizontalAlignmentProperty =
            DependencyProperty.Register(
                nameof(ContentHorizontalAlignment),
                typeof(HorizontalAlignment),
                typeof(ImageTextTabItem),
                new PropertyMetadata(HorizontalAlignment.Center)
            );

        public bool IsSelectedHilgihtBoder
        {
            get { return (bool)GetValue(IsSelectedHilgihtBoderProperty); }
            set { SetValue(IsSelectedHilgihtBoderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsSelectedHilgihtBoder.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsSelectedHilgihtBoderProperty =
            DependencyProperty.Register(
                nameof(IsSelectedHilgihtBoder),
                typeof(bool),
                typeof(ImageTextTabItem),
                new PropertyMetadata(false)
            );

        public bool IsMouseOverHilightBoder
        {
            get { return (bool)GetValue(IsMouseOverHilightProperty); }
            set { SetValue(IsMouseOverHilightProperty, value); }
        }

        public static readonly DependencyProperty IsMouseOverHilightProperty =
            DependencyProperty.Register(
                nameof(IsMouseOverHilightBoder),
                typeof(bool),
                typeof(ImageTextTabItem),
                new PropertyMetadata(false)
            );

        // 图片源属性
        public static readonly DependencyProperty ImageSourceProperty = DependencyProperty.Register(
            "ImageSource",
            typeof(ImageSource),
            typeof(ImageTextTabItem),
            new PropertyMetadata(null)
        );

        public ImageSource ImageSource
        {
            get { return (ImageSource)GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value); }
        }

        // 图片宽度属性
        public static readonly DependencyProperty ImageWidthProperty = DependencyProperty.Register(
            "ImageWidth",
            typeof(double),
            typeof(ImageTextTabItem),
            new PropertyMetadata(20.0)
        );

        public double ImageWidth
        {
            get { return (double)GetValue(ImageWidthProperty); }
            set { SetValue(ImageWidthProperty, value); }
        }

        // 图片高度属性
        public static readonly DependencyProperty ImageHeightProperty = DependencyProperty.Register(
            "ImageHeight",
            typeof(double),
            typeof(ImageTextTabItem),
            new PropertyMetadata(20.0)
        );

        public double ImageHeight
        {
            get { return (double)GetValue(ImageHeightProperty); }
            set { SetValue(ImageHeightProperty, value); }
        }

        // 文本内容属性
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            "Text",
            typeof(string),
            typeof(ImageTextTabItem),
            new PropertyMetadata(string.Empty)
        );

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        // 鼠标悬停时的文本颜色
        public static readonly DependencyProperty MouseOverTextColorProperty =
            DependencyProperty.Register(
                "MouseOverTextColor",
                typeof(Brush),
                typeof(ImageTextTabItem),
                new PropertyMetadata(new SolidColorBrush(Color.FromRgb(0xE6, 0xE6, 0xE6)))
            );

        public Brush MouseOverTextColor
        {
            get { return (Brush)GetValue(MouseOverTextColorProperty); }
            set { SetValue(MouseOverTextColorProperty, value); }
        }

        public Brush SelectedBackground
        {
            get { return (Brush)GetValue(SelectedBackgroundProperty); }
            set { SetValue(SelectedBackgroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedBackground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedBackgroundProperty =
            DependencyProperty.Register(
                nameof(SelectedBackground),
                typeof(Brush),
                typeof(ImageTextTabItem),
                new PropertyMetadata(new SolidColorBrush(Colors.Transparent))
            );

        public bool ShowSelectedBorder
        {
            get { return (bool)GetValue(ShowSelectedBorderProperty); }
            set { SetValue(ShowSelectedBorderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ShowSelectedBorder.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShowSelectedBorderProperty =
            DependencyProperty.Register(
                nameof(ShowSelectedBorder),
                typeof(bool),
                typeof(ImageTextTabItem),
                new PropertyMetadata(false)
            );

        public bool ShowMouseOverBorder
        {
            get { return (bool)GetValue(ShowMouseOverBorderProperty); }
            set { SetValue(ShowMouseOverBorderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ShowMouseOverBorder.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShowMouseOverBorderProperty =
            DependencyProperty.Register(
                nameof(ShowMouseOverBorder),
                typeof(bool),
                typeof(ImageTextTabItem),
                new PropertyMetadata(false)
            );

        #endregion 依赖属性
    }
}
