using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nakara.Modules.MainWindowRightEvent.UI.ViewModels;
using Nakara.Modules.MainWindowRightEvent.UI.Views;

namespace Nakara.Modules.MainWindowRightEvent.Module
{
    internal class MainWindowRightEventModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider) { }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<MainWindowRightEventUserControl>();
            containerRegistry.Register<MainWindowRightEventUserControlViewModel>();
        }
    }
}
