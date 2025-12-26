using Nakara.Modules.ChatBox.UI.ViewModels;
using Nakara.Modules.ChatBox.UI.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nakara.Modules.ChatBox.Module
{
    internal class ChatBoxModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<ChatBoxUserControl>();
            containerRegistry.Register<ChatBoxUserControlViewModel>();
        }
    }
}
