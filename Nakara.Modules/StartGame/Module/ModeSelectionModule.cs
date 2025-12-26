using Nakara.Modules.StartGame.UI.ModeSelection.ViewModels;
using Nakara.Modules.StartGame.UI.ModeSelection.Views;

namespace Nakara.Modules.StartGame.Module
{
    internal class ModeSelectionModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider) { }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<
                ModeSelectionUserControl,
                ModeSelectionUserControlViewModel
            >();
        }
    }
}
