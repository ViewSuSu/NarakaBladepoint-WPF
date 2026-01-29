using NarakaBladepoint.Modules.BattlePass.UI.BattlePass.ViewModels;
using NarakaBladepoint.Modules.BattlePass.UI.BattlePass.Views;

namespace NarakaBladepoint.Modules.BattlePass.Module
{
    internal class BattlePassModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider) { }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<BattlePassPage, BattlePassPageViewModel>();
            containerRegistry.RegisterForNavigation<
                BattlePassContentPage,
                BattlePassContentPageViewModel
            >();
            containerRegistry.RegisterForNavigation<
                BattlePassContentTabOnePage,
                BattlePassContentTabOnePageViewModel
            >();
            containerRegistry.RegisterForNavigation<
                BattlePassContentTabTwoPage,
                BattlePassContentTabTwoPageViewModel
            >();
            containerRegistry.RegisterForNavigation<
                BattlePassContentTabThreePage,
                BattlePassContentTabThreePageViewModel
            >();
        }
    }
}
