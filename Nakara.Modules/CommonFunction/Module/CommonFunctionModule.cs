using Nakara.Modules.CommonFunction.UI.ViewModels;
using Nakara.Modules.CommonFunction.UI.Views;

namespace Nakara.Modules.CommonFunction.Module
{
    internal class CommonFunctionModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider) { }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<CommonFunctionUserControl>();
            containerRegistry.Register<CommonFunctionUserControlViewModel>();
        }
    }
}
