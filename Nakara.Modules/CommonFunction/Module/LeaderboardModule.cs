using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nakara.Modules.CommonFunction.UI.Leaderboard.ViewModels;
using Nakara.Modules.CommonFunction.UI.Leaderboard.Views;

namespace Nakara.Modules.CommonFunction.Module
{
    internal class LeaderboardModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider) { }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<
                LeaderboardUserControl,
                LeaderboardUserControlViewModel
            >();
        }
    }
}
