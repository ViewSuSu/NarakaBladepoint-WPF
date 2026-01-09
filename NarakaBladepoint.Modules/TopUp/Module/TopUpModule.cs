using NarakaBladepoint.Modules.TopUp.UI.ViewModels;
using NarakaBladepoint.Modules.TopUp.UI.Views;

namespace NarakaBladepoint.Modules.TopUp.Module
{
    internal class TopUpModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider) { }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<TopUpPage, TopUpPageViewModel>();
        }
    }
}
