using NarakaBladepoint.Modules.CommonFunction.UI.Inventory.ViewModels;
using NarakaBladepoint.Modules.CommonFunction.UI.Inventory.Views;

namespace NarakaBladepoint.Modules.CommonFunction.Module
{
    internal class InventoryModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider) { }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<InventoryPage, InventoryPageViewModel>();
        }
    }
}
