using Nakara.Modules.PersonalInformation.Domain.Consts;
using Nakara.Modules.PersonalInformation.Domain.Events;
using Nakara.Modules.PersonalInformation.UI.PersonalInformationDetails.Views;

namespace Nakara.Modules.PersonalInformation.UI.PersonalInformationDetailMainContent.ViewModels
{
    internal class PersonalInformationDetailMainContentUserControlViewModel
    {
        private readonly IEventAggregator eventAggregator;
        private readonly IRegionManager regionManager;

        public PersonalInformationDetailMainContentUserControlViewModel(IEventAggregator eventAggregator, IRegionManager regionManager)
        {
            this.eventAggregator = eventAggregator;
            this.regionManager = regionManager;
            PersonalInfomationCommand = new DelegateCommand(() =>
            {
                this.eventAggregator.GetEvent<LoadPersonalInformationDetailMainContentEvents>()
                    .Publish(nameof(PersonalInformationDetailsPage));
            });
            this.eventAggregator.GetEvent<LoadPersonalInformationDetailMainContentEvents>().Subscribe((viewName) =>
            {
                this.regionManager.Regions[PersonalInformationDetailMainContentRegionConsts.PersonalInformationMainContent].RemoveAll();
                this.regionManager.RequestNavigate(PersonalInformationDetailMainContentRegionConsts.PersonalInformationMainContent, viewName);
            }, ThreadOption.UIThread);
        }

        public DelegateCommand PersonalInfomationCommand { get; }
    }
}
