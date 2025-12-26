using Nakara.Modules.Store.UI.ViewModels;
using Nakara.Modules.Store.UI.Views;

namespace Nakara.Modules.Store.Module
{
    internal class StoreModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider) { }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<StoreUserControl, StoreUserControlViewModel>();
        }
    }
}
