using Nakara.Modules.CommonFunction.UI.Hall.ViewModels;
using Nakara.Modules.CommonFunction.UI.Hall.Views;

namespace Nakara.Modules.CommonFunction.Module
{
    internal class HallModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider) { }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<HallUserControl, HallUserControlViewModel>();
        }
    }
}
