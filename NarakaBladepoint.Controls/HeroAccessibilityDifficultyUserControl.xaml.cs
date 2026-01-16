using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace NarakaBladepoint.Controls
{
    public partial class HeroAccessibilityDifficultyUserControl : UserControl
    {
        public HeroAccessibilityDifficultyUserControl()
        {
            InitializeComponent();
            StarIndexes = new[] { 1, 2, 3, 4, 5 };
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

        internal IEnumerable<int> StarIndexes { get; }
    }
}
