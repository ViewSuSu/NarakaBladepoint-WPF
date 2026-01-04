using NarakaBladepoint.Modules.PersonalInformation.Domain.Events;
using NarakaBladepoint.Modules.PersonalInformation.UI.PersonalInformationDetails.Views;

namespace NarakaBladepoint.Modules.PersonalInformation.UI.PersonalInformationDetails.ViewModels
{
    internal class PersonalInformationDetailsPageViewModel : ViewModelBase
    {
        public PersonalInformationDetailsPageViewModel(IContainerExtension containerExtension)
            : base(containerExtension)
        {
            HeroTagCommand = new DelegateCommand(() =>
            {
                eventAggregator
                    .GetEvent<LoadPersonalInformationDetailMainContentEvents>()
                    .Publish(nameof(HeroTagPage));
            });
        }

        public DelegateCommand HeroTagCommand { get; set; }
    }
}
