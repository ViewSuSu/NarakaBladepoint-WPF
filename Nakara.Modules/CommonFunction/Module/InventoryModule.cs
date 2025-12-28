using Nakara.Modules.CommonFunction.UI.Inventory.ViewModels;
using Nakara.Modules.CommonFunction.UI.Inventory.Views;

namespace Nakara.Modules.CommonFunction.Module
{
    internal class InventoryModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider) { }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<
                InventoryUserControl,
                InventoryUserControlViewModel
            >();
        }
    }
}
