using Nakara.Modules.PersonalInformation.UI.PersonalInformationDetailMainContent.Views;

namespace Nakara.Modules.PersonalInformation.UI.PersonalInformation.ViewModels
{
    internal class PersonalInformationUserControlViewModel : ViewModelBase
    {
        public PersonalInformationUserControlViewModel(IContainerExtension containerExtension)
            : base(containerExtension)
        {
            NavigateToPersonalInfomationCommand = new DelegateCommand(() =>
            {
                this.eventAggregator.GetEvent<LoadHomePageRegionEvent>()
                    .Publish(nameof(PersonalInformationDetailMainContentUserControl));
            });
        }

        public DelegateCommand NavigateToPersonalInfomationCommand { get; set; }
    }
}
