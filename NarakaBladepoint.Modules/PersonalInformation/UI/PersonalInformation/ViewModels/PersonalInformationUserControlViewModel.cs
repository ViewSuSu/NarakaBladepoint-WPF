using NarakaBladepoint.Modules.PersonalInformation.UI.PersonalInformationDetailMainContent.Views;
using NarakaBladepoint.Shared.Services.Abstractions;
using NarakaBladepoint.Shared.Services.Models;

namespace NarakaBladepoint.Modules.PersonalInformation.UI.PersonalInformation.ViewModels
{
    internal class PersonalInformationUserControlViewModel : ViewModelBase
    {
        public PersonalInformationUserControlViewModel(
            IContainerExtension containerExtension,
            ICurrentUserInformationProvider currentUserBasicInformation
        )
            : base(containerExtension)
        {
            NavigateToPersonalInfomationCommand = new DelegateCommand(() =>
            {
                this.eventAggregator.GetEvent<LoadHomePageRegionEvent>()
                    .Publish(nameof(PersonalInformationDetailMainContentUserControl));
            });
            this.UserInfoModel = currentUserBasicInformation.GetCurrentUserInfoAsync().Result;
        }

        public DelegateCommand NavigateToPersonalInfomationCommand { get; set; }
        public UserInformationModel UserInfoModel { get; }
    }
}
