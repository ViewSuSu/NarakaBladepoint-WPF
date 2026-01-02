using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nakara.Modules.PersonalInformation.Domain.Events;
using Nakara.Modules.PersonalInformation.UI.PersonalInformationDetails.Views;

namespace Nakara.Modules.PersonalInformation.UI.PersonalInformationDetails.ViewModels
{
    class HeroTagPageViewModel : ViewModelBase
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
