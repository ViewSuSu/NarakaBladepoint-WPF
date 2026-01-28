using NarakaBladepoint.Modules.TopUp.UI.Content.ViewModels;
using NarakaBladepoint.Modules.TopUp.UI.Content.Views;
using NarakaBladepoint.Modules.TopUp.UI.Main.ViewModels;
using NarakaBladepoint.Modules.TopUp.UI.Main.Views;

namespace NarakaBladepoint.Modules.TopUp.Module
{
    internal class TopUpModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider) { }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<TopUpPage, TopUpPageViewModel>();
            containerRegistry.RegisterForNavigation<TopUpContentPage, TopUpContentPageViewModel>();
        }
    }
}
