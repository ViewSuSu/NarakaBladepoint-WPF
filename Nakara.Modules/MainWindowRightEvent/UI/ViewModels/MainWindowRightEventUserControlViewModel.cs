using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nakara.Modules.MainWindowRightEvent.UI.ViewModels
{
    internal class MainWindowRightEventUserControlViewModel : ViewModelBase
    {
        public MainWindowRightEventUserControlViewModel(IContainerExtension containerExtension)
            : base(containerExtension)
        {
            EventOneCommand = new DelegateCommand(() => { });
            EventFourCommand = new DelegateCommand(() => { });
            EventEightCommand = new DelegateCommand(() => { });
        }

        public DelegateCommand EventOneCommand { get; set; }
        public DelegateCommand EventFourCommand { get; set; }
        public DelegateCommand EventEightCommand { get; set; }
    }
}
