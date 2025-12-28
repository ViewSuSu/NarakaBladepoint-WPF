using Nakara.Modules.EventCenter.UI.LatestNews.ViewModels;
using Nakara.Modules.EventCenter.UI.LatestNews.Views;
using Nakara.Modules.EventCenter.UI.Main.ViewModels;
using Nakara.Modules.EventCenter.UI.Main.Views;

namespace Nakara.Modules.EventCenter.Module
{
    internal class EventCenterModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider) { }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<
                EventCenterMainUserControl,
                EventCenterMainUserControlViewModel
            >();
            containerRegistry.RegisterForNavigation<LatestNewsPage, LatestNewsPageViewModel>();
        }
    }
}
