using NarakaBladepoint.Modules.CommonFunction.UI.Leaderboard.ViewModels;
using NarakaBladepoint.Modules.CommonFunction.UI.Leaderboard.Views;

namespace NarakaBladepoint.Modules.CommonFunction.Module
{
    internal class LeaderboardModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider) { }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<
                LeaderboardPage,
                LeaderboardPageViewModel
            >();
        }
    }
}
