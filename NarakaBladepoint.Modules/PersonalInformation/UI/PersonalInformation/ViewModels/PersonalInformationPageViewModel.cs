using NarakaBladepoint.Modules.PersonalInformation.UI.PersonalInformationDetailMainContent.Views;

namespace NarakaBladepoint.Modules.PersonalInformation.UI.PersonalInformation.ViewModels
{
    internal class PersonalInformationPageViewModel : ViewModelBase
    {
        public PersonalInformationPageViewModel(
            ICurrentUserInfoProvider currentUserBasicInformation
        )
        {
            this.UserInfoModel = currentUserBasicInformation.GetCurrentUserInfoAsync().Result;
        }

        private DelegateCommand _navigateToPersonalInfomationCommand;

        public DelegateCommand NavigateToPersonalInfomationCommand =>
            _navigateToPersonalInfomationCommand ??= new DelegateCommand(() =>
            {
                eventAggregator
                    .GetEvent<LoadHomePageRegionEvent>()
                    .Publish(new NavigationArgs(nameof(PersonalInformationDetailMainContentPage)));
            });

        public UserInformationData UserInfoModel { get; }
    }
}
