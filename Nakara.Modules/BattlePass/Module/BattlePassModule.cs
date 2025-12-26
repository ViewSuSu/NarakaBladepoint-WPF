using Nakara.Modules.BattlePass.UI.BattlePass.ViewModels;
using Nakara.Modules.BattlePass.UI.BattlePass.Views;
using Nakara.Modules.BattlePass.UI.BattlePassDetails.ViewModels;
using Nakara.Modules.BattlePass.UI.BattlePassDetails.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nakara.Modules.BattlePass.Module
{
    internal class BattlePassModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<BattlePassUserControl>();
            containerRegistry.Register<BattlePassUserControlViewModel>();
        }
    }
}
