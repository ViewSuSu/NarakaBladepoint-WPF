using NarakaBladepoint.Modules.ChatBox.UI.ViewModels;
using NarakaBladepoint.Modules.ChatBox.UI.Views;

namespace NarakaBladepoint.Modules.ChatBox.Module
{
    internal class ChatBoxModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider) { }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<ChatBoxUserControl>();
            containerRegistry.RegisterSingleton<ChatBoxUserControlViewModel>();
        }
    }
}
