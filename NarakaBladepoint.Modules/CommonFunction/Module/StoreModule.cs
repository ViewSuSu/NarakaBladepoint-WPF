using NarakaBladepoint.Modules.CommonFunction.UI.Store.ViewModels;
using NarakaBladepoint.Modules.CommonFunction.UI.Store.Views;

namespace NarakaBladepoint.Modules.CommonFunction.Module
{
    internal class StoreModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider) { }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<StorePage, StorePageViewModel>();
            containerRegistry.RegisterForNavigation<StoreHeroTagPage, StoreHeroTagPageViewModel>();
            containerRegistry.RegisterForNavigation<StoreDailyPage, StoreDailyPageViewModel>();
            containerRegistry.RegisterForNavigation<
                StoreOverviewPage,
                StoreOverviewPageViewModel
            >();
            containerRegistry.RegisterForNavigation<StoreRecommendPage>();
        }
    }
}
