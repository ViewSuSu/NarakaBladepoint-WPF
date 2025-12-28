using Nakara.Modules.BattlePass.UI.BattlePass.ViewModels;
using Nakara.Modules.BattlePass.UI.BattlePass.Views;

namespace Nakara.Modules.BattlePass.Module
{
    internal class BattlePassModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider) { }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<BattlePassUserControl>();
            containerRegistry.Register<BattlePassUserControlViewModel>();
        }
    }
}
