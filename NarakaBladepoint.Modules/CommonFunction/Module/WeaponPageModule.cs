using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NarakaBladepoint.Modules.CommonFunction.UI.Leaderboard.ViewModels;
using NarakaBladepoint.Modules.CommonFunction.UI.Leaderboard.Views;
using NarakaBladepoint.Modules.CommonFunction.UI.Weapon.ViewModels;
using NarakaBladepoint.Modules.CommonFunction.UI.Weapon.Views;

namespace NarakaBladepoint.Modules.CommonFunction.Module
{
    internal class WeaponPageModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider) { }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<WeaponPage, WeaponPageViewModel>();
        }
    }
}
