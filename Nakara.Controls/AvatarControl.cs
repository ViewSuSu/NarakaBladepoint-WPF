using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Nakara.Controls
{
    public class AvatarControl : Control
    {
        static AvatarControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(AvatarControl),
                new FrameworkPropertyMetadata(typeof(AvatarControl))
            );
        }

        public ImageSource BackgroundImage
        {
            get => (ImageSource)GetValue(BackgroundImageProperty);
            set => SetValue(BackgroundImageProperty, value);
        }

        public static readonly DependencyProperty BackgroundImageProperty =
            DependencyProperty.Register(
                nameof(BackgroundImage),
                typeof(ImageSource),
                typeof(AvatarControl)
            );

        public int Value
        {
            get => (int)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
            nameof(Value),
            typeof(int),
            typeof(AvatarControl),
            new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault)
        );

        public Stretch ImageStretch
        {
            get => (Stretch)GetValue(ImageStretchProperty);
            set => SetValue(ImageStretchProperty, value);
        }

        public static readonly DependencyProperty ImageStretchProperty =
            DependencyProperty.Register(
                nameof(ImageStretch),
                typeof(Stretch),
                typeof(AvatarControl),
                new PropertyMetadata(Stretch.UniformToFill)
            );

        public double CornerLength
        {
            get => (double)GetValue(CornerLengthProperty);
            set => SetValue(CornerLengthProperty, value);
        }

        public static readonly DependencyProperty CornerLengthProperty =
            DependencyProperty.Register(
                nameof(CornerLength),
                typeof(double),
                typeof(AvatarControl),
                new PropertyMetadata(18.0)
            );

        public double CornerThickness
        {
            get => (double)GetValue(CornerThicknessProperty);
            set => SetValue(CornerThicknessProperty, value);
        }

        public static readonly DependencyProperty CornerThicknessProperty =
            DependencyProperty.Register(
                nameof(CornerThickness),
                typeof(double),
                typeof(AvatarControl),
                new PropertyMetadata(3.0)
            );
    }
}
