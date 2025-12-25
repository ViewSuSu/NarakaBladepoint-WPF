using Nakara_WPF.Modules.Store.ViewModels;
using Nakara_WPF.Modules.Store.Views;

namespace Nakara_WPF.Modules.Store
{
    class StoreModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider) { }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<StoreUserControl, StoreUserControlViewModel>();
        }
    }
}
