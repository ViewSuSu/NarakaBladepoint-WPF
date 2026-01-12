using NarakaBladepoint.Modules.PersonalInformation.Domain.Events;

namespace NarakaBladepoint.Modules.PersonalInformation.UI.PersonalInformationDetailMainContent.ViewModels
{
    internal class PersonalInformationDetailMainContentPageViewModel
        : CanRemoveHomePageRegionViewModelBase
    {
        public PersonalInformationDetailMainContentPageViewModel(
            IContainerProvider containerProvider
        )
            : base(containerProvider) { }
    }
}
