using Nakara.Modules.CommonFunction.UI.Store.ViewModels;
using Nakara.Modules.CommonFunction.UI.Store.Views;

namespace Nakara.Modules.CommonFunction.Module
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
