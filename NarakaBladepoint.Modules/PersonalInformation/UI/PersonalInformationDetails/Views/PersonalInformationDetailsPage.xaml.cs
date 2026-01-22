namespace NarakaBladepoint.Modules.PersonalInformation.UI.PersonalInformationDetails.Views
{
    /// <summary>
    /// PersonalInformationDetailsPage.xaml 的交互逻辑
    /// </summary>
    public partial class PersonalInformationDetailsPage : UserControlBase
    {
        public PersonalInformationDetailsPage()
        {
            InitializeComponent();
        }

        private void ChangeGender_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            e.Handled = true;  // 标记事件为已处理，防止事件继续传播
            if (this.FindName("EditMenuPopup") is System.Windows.Controls.Primitives.Popup popup)
            {
                popup.IsOpen = !popup.IsOpen;
            }
        }
    }
}
