using NarakaBladepoint.Modules.CommonFunction.UI.CommonFunction.ViewModels;
using NarakaBladepoint.Modules.CommonFunction.UI.CommonFunction.Views;

namespace NarakaBladepoint.Modules.CommonFunction.Module
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
