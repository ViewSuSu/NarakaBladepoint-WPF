using NarakaBladepoint.Modules.CommonFunction.UI.CommonFunction.ViewModels;
using NarakaBladepoint.Modules.CommonFunction.UI.CommonFunction.Views;

namespace NarakaBladepoint.Modules.CommonFunction.Module
{
    /// <summary>
    /// CommonFunction主模块，注册CommonFunctionPage和相关ViewModel
    /// 其他功能模块通过单独的Module文件进行注册，如：
    /// - CustomMatchModule: 自定义房间
    /// - StoreModule: 商店
    /// - LeaderboardModule: 排行榜
    /// - HeroListModule: 英雄列表
    /// - InventoryModule: 背包
    /// 等
    /// </summary>
    internal class CommonFunctionModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider) { }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // 注册主页面和ViewModel
            containerRegistry.Register<CommonFunctionPage>();
            containerRegistry.Register<CommonFunctionPageViewModel>();
        }
    }
}
