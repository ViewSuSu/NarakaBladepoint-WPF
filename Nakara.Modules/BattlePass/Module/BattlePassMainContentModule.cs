using Nakara.Modules.BattlePass.UI.BattlePassDetails.ViewModels;
using Nakara.Modules.BattlePass.UI.BattlePassDetails.Views;

namespace Nakara.Modules.BattlePass.Module
{
    internal class BattlePassMainContentModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider) { }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<
                BattlePassMainContentUserControl,
                BattlePassMainContentUserControlViewModel
            >();
            containerRegistry.Register<TaskPage>();
            containerRegistry.Register<TaskPageViewModel>();
        }
    }
}
