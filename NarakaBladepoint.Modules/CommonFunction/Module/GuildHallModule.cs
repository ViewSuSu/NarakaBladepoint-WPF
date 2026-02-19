using NarakaBladepoint.Modules.CommonFunction.UI.GuildHall.ViewModels;
using NarakaBladepoint.Modules.CommonFunction.UI.GuildHall.Views;

namespace NarakaBladepoint.Modules.CommonFunction.Module
{
    /// <summary>
    /// GuildHall模块，注册聚义厅相关页面和ViewModel
    /// 包括：聚义厅主页、主内容页、商店页、活动页等
    /// </summary>
    internal class GuildHallModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider) { }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // 注册聚义厅主内容页
            containerRegistry.RegisterForNavigation<GuildHallMainContentPage, GuildHallMainContentPageViewModel>();

            // 注册聚义厅主页
            containerRegistry.RegisterForNavigation<GuildHallPage, GuildHallPageViewModel>();

            // 注册聚义厅商店页
            containerRegistry.RegisterForNavigation<GuildHallStorePage, GuildHallStorePageViewModel>();

            // 注册聚义厅活动页
            containerRegistry.RegisterForNavigation<GuildHallEventPage, GuildHallEventPageViewModel>();
        }
    }
}
