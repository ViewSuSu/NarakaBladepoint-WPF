using Nakara.Modules.TopUp.UI.ViewModels;
using Nakara.Modules.TopUp.UI.Views;

namespace Nakara.Modules.TopUp.Module
{
    internal class TopUpModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider) { }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<TopUpUserControl, TopUpUserControlViewModel>();
        }
    }
}
