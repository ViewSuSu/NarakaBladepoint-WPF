using Nakara_WPF.Modules.Wealth.ViewModels;
using Nakara_WPF.Modules.Wealth.Views;

namespace Nakara_WPF.Modules.Wealth
{
    class WealthModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider) { }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<WealthUserControl>();
            containerRegistry.Register<WealthUserControlViewModel>();
        }
    }
}
