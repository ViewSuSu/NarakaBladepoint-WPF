namespace NarakaBladepoint.Modules.Social.UI.Friend.UI.Views
{
    /// <summary>
    /// FriendUserControl.xaml 的交互逻辑
    /// </summary>
    internal partial class FriendUserControl : UserControlBase
    {
        public FriendUserControl()
        {
            InitializeComponent();
        }

        private void Image_MouseLeftButtonDown(
            object sender,
            System.Windows.Input.MouseButtonEventArgs e
        )
        {
            SearchBox.Text = null;
            SearchBox.Focus();
        }
    }
}
