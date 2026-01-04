using NarakaBladepoint.Modules.BattlePass.UI.BattlePassDetails.ViewModels;
using NarakaBladepoint.Modules.BattlePass.UI.BattlePassDetails.Views;

namespace NarakaBladepoint.Modules.BattlePass.Module
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
