using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace NarakaBladepoint.Controls
{
    /// <summary>
    /// 带占位符的文本输入控件
    /// </summary>
    public class PlaceholderTextBox : TextBox
    {
        #region Fields

        private readonly TextBlock _placeholderTextBlock = new TextBlock();
        private readonly Border _placeholderContainer = new Border();
        private readonly VisualBrush _placeholderVisualBrush = new VisualBrush();

        #endregion

        #region Dependency Properties

        public static readonly DependencyProperty PlaceholderProperty = DependencyProperty.Register(
            nameof(Placeholder),
            typeof(string),
            typeof(PlaceholderTextBox),
            new FrameworkPropertyMetadata("请在此输入")
        );

        public static readonly DependencyProperty PlaceholderForegroundProperty =
            DependencyProperty.Register(
                nameof(PlaceholderForeground),
                typeof(Brush),
                typeof(PlaceholderTextBox),
                new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromRgb(136, 136, 136)))
            );

        public static readonly DependencyProperty PlaceholderMarginProperty =
            DependencyProperty.Register(
                nameof(PlaceholderMargin),
                typeof(Thickness),
                typeof(PlaceholderTextBox),
                new FrameworkPropertyMetadata(new Thickness(0))
            );

        #endregion

        #region CLR Properties

        public string Placeholder
        {
            get => (string)GetValue(PlaceholderProperty);
            set => SetValue(PlaceholderProperty, value);
        }

        public Brush PlaceholderForeground
        {
            get => (Brush)GetValue(PlaceholderForegroundProperty);
            set => SetValue(PlaceholderForegroundProperty, value);
        }

        public Thickness PlaceholderMargin
        {
            get => (Thickness)GetValue(PlaceholderMarginProperty);
            set => SetValue(PlaceholderMarginProperty, value);
        }

        #endregion

        #region Constructor

        public PlaceholderTextBox()
        {
            _placeholderTextBlock.SetBinding(
                TextBlock.TextProperty,
                new Binding(nameof(Placeholder)) { Source = this }
            );

            _placeholderTextBlock.SetBinding(
                TextBlock.ForegroundProperty,
                new Binding(nameof(PlaceholderForeground)) { Source = this }
            );

            _placeholderContainer.Child = _placeholderTextBlock;

            _placeholderContainer.SetBinding(
                Border.MarginProperty,
                new Binding(nameof(PlaceholderMargin)) { Source = this }
            );

            _placeholderVisualBrush.AlignmentX = AlignmentX.Left;
            _placeholderVisualBrush.AlignmentY = AlignmentY.Center;
            _placeholderVisualBrush.Stretch = Stretch.None;
            _placeholderVisualBrush.Visual = _placeholderContainer;

            Loaded += PlaceholderTextBox_Loaded;
            TextChanged += PlaceholderTextBox_TextChanged;
        }

        #endregion

        #region Event Handling

        private void PlaceholderTextBox_Loaded(object sender, RoutedEventArgs e)
        {
            Background = string.IsNullOrEmpty(Text) ? _placeholderVisualBrush : null;
        }

        private void PlaceholderTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Background = string.IsNullOrEmpty(Text) ? _placeholderVisualBrush : null;
        }

        #endregion
    }
}
