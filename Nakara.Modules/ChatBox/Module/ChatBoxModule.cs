using Nakara.Modules.ChatBox.UI.ViewModels;
using Nakara.Modules.ChatBox.UI.Views;

namespace Nakara.Modules.ChatBox.Module
{
    internal class ChatBoxModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider) { }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<ChatBoxUserControl>();
            containerRegistry.Register<ChatBoxUserControlViewModel>();
        }
    }
}
