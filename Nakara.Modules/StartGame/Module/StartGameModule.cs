using Nakara.Modules.StartGame.UI.StartGame.ViewModels;
using Nakara.Modules.StartGame.UI.StartGame.Views;

namespace Nakara.Modules.StartGame.Module
{
    internal class StartGameModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider) { }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<StartGameUserControl>();
            containerRegistry.Register<StartGameUserControlViewModel>();
        }
    }
}
