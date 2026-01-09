using NarakaBladepoint.Modules.Tutorial.UI.ViewModels;
using NarakaBladepoint.Modules.Tutorial.UI.Views;

namespace NarakaBladepoint.Modules.Tutorial.Module
{
    internal class TutorialModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider) { }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<TutorialPage, TutorialPageViewModel>();
        }
    }
}
