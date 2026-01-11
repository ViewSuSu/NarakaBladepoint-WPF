using NarakaBladepoint.Shared.Datas;
using NarakaBladepoint.Shared.Services.Abstractions;

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

        public EmailPageViewModel(
            IContainerProvider containerProvider,
            IEmailItemDataProvider emailItemDataProvider
        )
            : base(containerProvider)
        {
            this.emailItemDataProvider = emailItemDataProvider;
            this.EmailDatas = emailItemDataProvider.GetEmailItemDatasAsync().Result;
            SelectedEmailData = EmailDatas.FirstOrDefault();
        }
    }
}
