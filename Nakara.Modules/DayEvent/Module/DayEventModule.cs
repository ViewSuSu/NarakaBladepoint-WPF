using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nakara.Modules.DayEvent.UI.ViewModels;
using Nakara.Modules.DayEvent.UI.Views;

namespace Nakara.Modules.DayEvent.Module
{
    class DayEventModuleL : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider) { }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<DayEventUserControl>();
            containerRegistry.Register<DayEventUserControlViewModel>();
        }
    }
}
