using NarakaBladepoint.Modules.CommonFunction.UI.Inventory.ViewModels;
using NarakaBladepoint.Modules.CommonFunction.UI.Inventory.Views;

namespace NarakaBladepoint.Modules.CommonFunction.Module
{
    /// <summary>
    /// Inventory模块，注册背包相关页面和ViewModel
    /// 包括：背包主页、第二页、第三页、主内容页等
    /// </summary>
    internal class InventoryModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider) { }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // 注册背包主页
            containerRegistry.RegisterForNavigation<InventoryPage, InventoryPageViewModel>();
            
            // 注册背包第二页（装备等）
            containerRegistry.RegisterForNavigation<InventoryPageTwo, InventoryPageTwoViewModel>();
            
            // 注册背包第三页（碎片等）
            containerRegistry.RegisterForNavigation<InventoryPageThree, InventoryPageThreeViewModel>();
            
            // 注册背包主内容页
            containerRegistry.RegisterForNavigation<InventoryMainContentPage, InventoryMainContentPageViewModel>();
        }
    }
}
