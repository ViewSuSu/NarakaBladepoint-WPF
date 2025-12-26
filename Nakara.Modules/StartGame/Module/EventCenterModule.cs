using Nakara.Modules.StartGame.UI.EventCenter.ViewModels;
using Nakara.Modules.StartGame.UI.EventCenter.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nakara.Modules.StartGame.Module
{
    internal class EventCenterModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<EventCenterUserControl, EventCenterUserControlViewModel>();
        }
    }
}
