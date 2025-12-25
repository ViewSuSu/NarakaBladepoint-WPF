using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nakara_WPF.Modules.PersonalInformation.ViewModels;
using Nakara_WPF.Modules.PersonalInformation.Views;

namespace Nakara_WPF.Modules.PersonalInformation
{
    class PersonalInformationModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider) { }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<PersonalInformationUserControl>();
            containerRegistry.Register<PersonalInformationUserControlViewModel>();
        }
    }
}
