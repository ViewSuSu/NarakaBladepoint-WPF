using NarakaBladepoint.Modules.PersonalInformation.Domain.Events;
using NarakaBladepoint.Modules.PersonalInformation.UI.PersonalInformationDetailMainContent.Views;

namespace NarakaBladepoint.Modules.PersonalInformation.UI.PersonalInformation.ViewModels
{
    internal class PersonalInformationPageViewModel : ViewModelBase
    {
        public PersonalInformationPageViewModel(
            ICurrentUserInfoProvider currentUserBasicInformation
        )
        {
            this.currentUserBasicInformation = currentUserBasicInformation;
            this.UserInfoModel = currentUserBasicInformation.GetCurrentUserInfoAsync().Result;
            Name = UserInfoModel.Name;
            eventAggregator.GetEvent<SaveNameSuccessEvent>().Subscribe(OnNameSaved);
        }

        private DelegateCommand _navigateToPersonalInfomationCommand;
        private readonly ICurrentUserInfoProvider currentUserBasicInformation;

        public DelegateCommand NavigateToPersonalInfomationCommand =>
            _navigateToPersonalInfomationCommand ??= new DelegateCommand(() =>
            {
                eventAggregator
                    .GetEvent<LoadHomePageRegionEvent>()
                    .Publish(new NavigationArgs(nameof(PersonalInformationDetailMainContentPage)));
            });

        public UserInformationData UserInfoModel { get; }

        private string _name;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                RaisePropertyChanged();
            }
        }

        private async void OnNameSaved()
        {
            var updatedUserInfo = await currentUserBasicInformation.GetCurrentUserInfoAsync();
            Name = updatedUserInfo.Name;
        }
    }
}
