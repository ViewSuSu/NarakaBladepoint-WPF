using NarakaBladepoint.Modules.PersonalInformation.UI.PersonalInformationDetailMainContent.Views;
using NarakaBladepoint.Shared.Datas;
using NarakaBladepoint.Shared.Services.Abstractions;

namespace NarakaBladepoint.Modules.PersonalInformation.UI.PersonalInformation.ViewModels
{
    internal class PersonalInformationUserControlViewModel : ViewModelBase
    {
        public PersonalInformationUserControlViewModel(
            IContainerProvider containerProvider,
            ICurrentUserInfoProvider currentUserBasicInformation
        )
            : base(containerProvider)
        {
            NavigateToPersonalInfomationCommand = new DelegateCommand(() =>
            {
                this.eventAggregator.GetEvent<LoadHomePageRegionEvent>()
                    .Publish(nameof(PersonalInformationDetailMainContentUserControl));
            });
            this.UserInfoModel = currentUserBasicInformation.GetCurrentUserInfoAsync().Result;
        }

        public DelegateCommand NavigateToPersonalInfomationCommand { get; set; }
        public UserInformationData UserInfoModel { get; }
    }
}
