using Nakara.Modules.Wealth.UI.ViewModels;
using Nakara.Modules.Wealth.UI.Views;

namespace Nakara.Modules.Wealth.Module
{
    internal class WealthModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider) { }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<WealthUserControl>();
            containerRegistry.Register<WealthUserControlViewModel>();
        }
    }
}
