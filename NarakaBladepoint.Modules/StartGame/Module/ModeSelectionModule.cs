using NarakaBladepoint.Modules.StartGame.UI.HeroChose.ViewModels;
using NarakaBladepoint.Modules.StartGame.UI.HeroChose.Views;
using NarakaBladepoint.Modules.StartGame.UI.MapChose.ViewModels;
using NarakaBladepoint.Modules.StartGame.UI.MapChose.Views;
using NarakaBladepoint.Modules.StartGame.UI.ModeSelection.ViewModels;
using NarakaBladepoint.Modules.StartGame.UI.ModeSelection.Views;

namespace NarakaBladepoint.Modules.StartGame.Module
{
    internal class ModeSelectionModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider) { }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<
                ModeSelectionPage,
                ModeSelectionPageViewModel
            >();
            containerRegistry.RegisterForNavigation<HeroChosePage, HeroChosePageViewModel>();
            containerRegistry.RegisterForNavigation<MapChosePage, MapChosePageViewModel>();
        }
    }
}
