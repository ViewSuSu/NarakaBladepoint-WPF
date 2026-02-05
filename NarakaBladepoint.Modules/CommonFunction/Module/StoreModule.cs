using NarakaBladepoint.Modules.CommonFunction.UI.Store.ViewModels;
using NarakaBladepoint.Modules.CommonFunction.UI.Store.Views;

namespace NarakaBladepoint.Modules.CommonFunction.Module
{
    /// <summary>
    /// Store模块，注册商店相关页面和ViewModel
    /// 包括：商店主页、英雄印、每日商店、推荐商店等
    /// </summary>
    internal class StoreModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider) { }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // 注册商店主页
            containerRegistry.RegisterForNavigation<StorePage, StorePageViewModel>();
            
            // 注册英雄印页面
            containerRegistry.RegisterForNavigation<StoreHeroTagPage, StoreHeroTagPageViewModel>();
            
            // 注册每日商店页面
            containerRegistry.RegisterForNavigation<StoreDailyPage, StoreDailyPageViewModel>();
            
            // 注册概览页面
            containerRegistry.RegisterForNavigation<
                StoreOverviewPage,
                StoreOverviewPageViewModel
            >();
            
            // 注册推荐商店页面
            containerRegistry.RegisterForNavigation<StoreRecommendPage>();
        }
    }
}
