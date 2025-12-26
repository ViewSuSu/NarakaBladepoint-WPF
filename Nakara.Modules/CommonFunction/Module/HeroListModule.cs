using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nakara.Modules.CommonFunction.UI.HeroList;
using Nakara.Modules.CommonFunction.UI.HeroList.ViewModels;
using Nakara.Modules.CommonFunction.UI.HeroList.Views;

namespace Nakara.Modules.CommonFunction.Module
{
    internal class HeroListModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider) { }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<
                HeroListUserControl,
                HeroListUserControlViewModel
            >();
        }
    }
}
