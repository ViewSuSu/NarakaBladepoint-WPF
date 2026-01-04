using NarakaBladepoint.Modules.Wealth.UI.Main.ViewModels;
using NarakaBladepoint.Modules.Wealth.UI.Main.Views;

namespace NarakaBladepoint.Modules.Wealth.Module
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
