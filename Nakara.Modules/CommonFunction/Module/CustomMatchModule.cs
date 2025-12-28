using Nakara.Modules.CommonFunction.UI.CustomMatch.ViewModels;
using Nakara.Modules.CommonFunction.UI.CustomMatch.Views;

namespace Nakara.Modules.CommonFunction.Module
{
    internal class CustomMatchModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider) { }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<
                CustomMatchUserControl,
                CustomMatchUserControlViewModel
            >();
        }
    }
}
