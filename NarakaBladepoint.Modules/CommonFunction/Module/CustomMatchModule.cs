using NarakaBladepoint.Modules.CommonFunction.UI.CustomMatch.ViewModels;
using NarakaBladepoint.Modules.CommonFunction.UI.CustomMatch.Views;

namespace NarakaBladepoint.Modules.CommonFunction.Module
{
    internal class CustomMatchModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider) { }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<CustomMatchPage, CustomMatchPageViewModel>();
        }
    }
}
