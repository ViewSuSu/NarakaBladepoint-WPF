using NarakaBladepoint.Modules.MainWindowRightEvent.UI.ViewModels;
using NarakaBladepoint.Modules.MainWindowRightEvent.UI.Views;

namespace NarakaBladepoint.Modules.MainWindowRightEvent.Module
{
    internal class MainWindowRightEventModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider) { }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<MainWindowRightEventPage>();
            containerRegistry.Register<MainWindowRightEventPageViewModel>();
        }
    }
}
