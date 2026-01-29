namespace NarakaBladepoint.Modules.Social.UI.Email.ViewModels
{
    internal class EmailPageViewModel : CanRemoveHomePageRegionViewModelBase
    {
        private readonly IEmailItemDataProvider emailItemDataProvider;

        public List<EmailItemData> EmailDatas { get; }

        private EmailItemData _selectedEmailData;

        public EmailItemData SelectedEmailData
        {
            get { return _selectedEmailData; }
            set
            {
                _selectedEmailData = value;
                RaisePropertyChanged();
            }
        }

        public EmailPageViewModel(IEmailItemDataProvider emailItemDataProvider)
        {
            this.emailItemDataProvider = emailItemDataProvider;
            this.EmailDatas = emailItemDataProvider.GetEmailItemDatasAsync().Result;
            SelectedEmailData = EmailDatas.FirstOrDefault();
        }
    }
}
