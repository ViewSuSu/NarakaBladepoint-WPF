using Nakara.Modules.CommonFunction.UI.CommonFunction.ViewModels;
using Nakara.Modules.CommonFunction.UI.CommonFunction.Views;

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
