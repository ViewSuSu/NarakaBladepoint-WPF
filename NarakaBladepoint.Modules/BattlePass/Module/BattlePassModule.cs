using NarakaBladepoint.Modules.BattlePass.UI.BattlePass.ViewModels;
using NarakaBladepoint.Modules.BattlePass.UI.BattlePass.Views;

namespace NarakaBladepoint.Modules.BattlePass.Module
{
    internal class BattlePassModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider) { }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<BattlePassPage>();
            containerRegistry.Register<BattlePassPageViewModel>();
        }
    }
}
