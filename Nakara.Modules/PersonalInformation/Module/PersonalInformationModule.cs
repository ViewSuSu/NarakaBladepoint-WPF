using Nakara.Modules.PersonalInformation.UI.ViewModels;
using Nakara.Modules.PersonalInformation.UI.Views;

namespace Nakara.Modules.PersonalInformation.Module
{
    internal class PersonalInformationModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider) { }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<PersonalInformationUserControl>();
            containerRegistry.Register<PersonalInformationUserControlViewModel>();
        }
    }
}
