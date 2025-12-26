using Nakara.Modules.PersonalInformation.UI.PersonalInformationDetailMainContent.Views;

namespace Nakara.Modules.PersonalInformation.UI.PersonalInformation.ViewModels
{
    internal class PersonalInformationUserControlViewModel {
        private readonly IEventAggregator eventAggregator;

        public PersonalInformationUserControlViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
            NavigateToPersonalInfomationCommand = new DelegateCommand(() => { 
            this.eventAggregator.GetEvent<LoadHomePageRegionEvent>()
                .Publish(nameof(PersonalInformationDetailMainContentUserControl));
            });
        }

        public DelegateCommand NavigateToPersonalInfomationCommand { get; set; }
    }
}
