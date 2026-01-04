using Nakara.Modules.PersonalInformation.Domain.Events;

namespace Nakara.Modules.PersonalInformation.UI.PersonalInformationDetails.ViewModels
{
    internal class HeroTagPageViewModel : ViewModelBase
    {
        public HeroTagPageViewModel(IContainerExtension containerExtension)
            : base(containerExtension)
        {
            EscCommand = new DelegateCommand(() =>
            {
                eventAggregator
                    .GetEvent<RemovePersonalInformationDetailMainContentEvents>()
                    .Publish();
            });
            SaveCommand = new DelegateCommand(() => { });
        }

        public DelegateCommand EscCommand { get; set; }
        public DelegateCommand SaveCommand { get; set; }
    }
}
