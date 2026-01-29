using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace NarakaBladepoint.Controls
{
    public partial class HeroRadarUserControl : UserControl
    {
        public HeroRadarUserControl()
        {
            InitializeComponent();
            UpdateRadar();
        }

        public int Survival
        {
            get => (int)GetValue(SurvivalProperty);
            set => SetValue(SurvivalProperty, value);
        }

        public static readonly DependencyProperty SurvivalProperty = DependencyProperty.Register(
            nameof(Survival),
            typeof(int),
            typeof(HeroRadarUserControl),
            new FrameworkPropertyMetadata(
                1,
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                OnValueChanged
            )
        );

        public int Control
        {
            get => (int)GetValue(ControlProperty);
            set => SetValue(ControlProperty, value);
        }

        public static readonly DependencyProperty ControlProperty = DependencyProperty.Register(
            nameof(Control),
            typeof(int),
            typeof(HeroRadarUserControl),
            new FrameworkPropertyMetadata(
                1,
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                OnValueChanged
            )
        );

        public int Mobility
        {
            get => (int)GetValue(MobilityProperty);
            set => SetValue(MobilityProperty, value);
        }

        public static readonly DependencyProperty MobilityProperty = DependencyProperty.Register(
            nameof(Mobility),
            typeof(int),
            typeof(HeroRadarUserControl),
            new FrameworkPropertyMetadata(
                1,
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                OnValueChanged
            )
        );

        public int Damage
        {
            get => (int)GetValue(DamageProperty);
            set => SetValue(DamageProperty, value);
        }

        public static readonly DependencyProperty DamageProperty = DependencyProperty.Register(
            nameof(Damage),
            typeof(int),
            typeof(HeroRadarUserControl),
            new FrameworkPropertyMetadata(
                1,
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                OnValueChanged
            )
        );

        public int Support
        {
            get => (int)GetValue(SupportProperty);
            set => SetValue(SupportProperty, value);
        }

        public static readonly DependencyProperty SupportProperty = DependencyProperty.Register(
            nameof(Support),
            typeof(int),
            typeof(HeroRadarUserControl),
            new FrameworkPropertyMetadata(
                1,
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                OnValueChanged
            )
        );

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((HeroRadarUserControl)d).UpdateRadar();
        }

        private readonly Point[] MaxPoints =
        {
            new(130, 30),
            new(215, 85),
            new(185, 190),
            new(75, 190),
            new(45, 85),
        };

        private const double CenterX = 130;
        private const double CenterY = 130;

        private void UpdateRadar()
        {
            int[] values = { Survival, Control, Mobility, Damage, Support };
            var points = new PointCollection();

            for (int i = 0; i < 5; i++)
            {
                double rate = Math.Clamp(values[i], 1, 4) / 4.0;
                var p = Lerp(MaxPoints[i], rate);
                points.Add(p);
                SetPoint(i, p);
                SetTextColor(i, values[i]);
            }

            DataPolygon.Points = points;
        }

        private Point Lerp(Point max, double rate)
        {
            return new Point(
                CenterX + (max.X - CenterX) * rate,
                CenterY + (max.Y - CenterY) * rate
            );
        }

        private void SetPoint(int i, Point p)
        {
            var e = new[] { P1, P2, P3, P4, P5 }[i];
            Canvas.SetLeft(e, p.X - 4);
            Canvas.SetTop(e, p.Y - 4);
        }

        private void SetTextColor(int i, int value)
        {
            var t = new[] { T1, T2, T3, T4, T5 }[i];
            t.Foreground = value >= 3 ? Brushes.White : Brushes.Gray;
        }
    }
}
