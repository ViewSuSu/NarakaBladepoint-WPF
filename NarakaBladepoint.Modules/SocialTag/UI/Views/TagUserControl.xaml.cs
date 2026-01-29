using System.Windows;

namespace NarakaBladepoint.Modules.SocialTag.UI.Views
{
    /// <summary>
    /// TagUserControl.xaml 的交互逻辑
    /// </summary>
    public partial class TagUserControl : UserControlBase
    {
        public TagUserControl()
        {
            InitializeComponent();
        }

        public double ContentFontSize
        {
            get { return (double)GetValue(ContentFontSizeProperty); }
            set { SetValue(ContentFontSizeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ContentFontSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ContentFontSizeProperty =
            DependencyProperty.Register(
                nameof(ContentFontSize),
                typeof(double),
                typeof(TagUserControl),
                new PropertyMetadata(Convert.ToDouble(12))
            );
    }
}
