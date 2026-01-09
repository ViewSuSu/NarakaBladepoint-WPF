using NarakaBladepoint.Modules.StartGame.UI.StartGame.ViewModels;
using NarakaBladepoint.Modules.StartGame.UI.StartGame.Views;

namespace NarakaBladepoint.Modules.StartGame.Module
{
    internal class StartGameModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider) { }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<StartGamePage>();
            containerRegistry.Register<StartGamePageViewModel>();
        }
    }
}
