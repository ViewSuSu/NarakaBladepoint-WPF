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
            this.UserInfoModel = currentUserBasicInformation.GetCurrentUserInfoAsync().Result;
        }

        private DelegateCommand _navigateToPersonalInfomationCommand;

        public DelegateCommand NavigateToPersonalInfomationCommand =>
            _navigateToPersonalInfomationCommand ??= new DelegateCommand(() =>
            {
                eventAggregator.GetEvent<LoadHomePageRegionEvent>()
                    .Publish(nameof(PersonalInformationDetailMainContentUserControl));
            });
        public UserInformationData UserInfoModel { get; }
    }
}
