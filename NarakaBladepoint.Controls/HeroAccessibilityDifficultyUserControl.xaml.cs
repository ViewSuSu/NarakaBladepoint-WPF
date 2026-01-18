using System.Windows;
using System.Windows.Controls;

namespace NarakaBladepoint.Controls
{
    public partial class HeroAccessibilityDifficultyUserControl : UserControl
    {
        public HeroAccessibilityDifficultyUserControl()
        {
            InitializeComponent();
        }

        public double Number
        {
            get => (double)GetValue(NumberProperty);
            set => SetValue(NumberProperty, value);
        }

        public static readonly DependencyProperty NumberProperty = DependencyProperty.Register(
            nameof(Number),
            typeof(double),
            typeof(HeroAccessibilityDifficultyUserControl),
            new FrameworkPropertyMetadata(1d, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault)
        );
    }
}
