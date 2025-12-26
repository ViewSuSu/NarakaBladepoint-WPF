using System.ComponentModel;
using System.Windows.Controls;

namespace Nakara.Modules.Social.UI.Social.Views
{
    /// <summary>
    /// SocialUserControl.xaml 的交互逻辑
    /// </summary>
    public partial class SocialUserControl : UserControl
    {
        public SocialUserControl()
        {
            InitializeComponent();
            if (DesignerProperties.GetIsInDesignMode(this))
                return;
        }
    }
}
