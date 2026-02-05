using NarakaBladepoint.Modules.CommonFunction.UI.CustomMatch.ViewModels;
using NarakaBladepoint.Modules.CommonFunction.UI.CustomMatch.Views;

namespace NarakaBladepoint.Modules.CommonFunction.Module
{
    /// <summary>
    /// CustomMatch模块，注册自定义房间相关页面和ViewModel
    /// </summary>
    internal class CustomMatchModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider) { }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // 注册自定义房间页面及其ViewModel用于导航
            containerRegistry.RegisterForNavigation<CustomMatchPage, CustomMatchPageViewModel>();
        }
    }
}
