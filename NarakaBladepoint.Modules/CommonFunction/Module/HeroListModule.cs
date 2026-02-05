using NarakaBladepoint.Modules.CommonFunction.UI.Hero.ViewModels;
using NarakaBladepoint.Modules.CommonFunction.UI.Hero.Views;

namespace NarakaBladepoint.Modules.CommonFunction.Module
{
    /// <summary>
    /// HeroList模块，注册英雄列表相关页面和ViewModel
    /// 包括：英雄列表主页等
    /// </summary>
    internal class HeroListModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider) { }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // 注册英雄列表页
            containerRegistry.RegisterForNavigation<HeroListPage, HeroListPageViewModel>();
        }
    }
}
