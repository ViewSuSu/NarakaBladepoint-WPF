using NarakaBladepoint.Modules.Social.UI.Email.ViewModels;
using NarakaBladepoint.Modules.Social.UI.Email.Views;
using NarakaBladepoint.Modules.Social.UI.Friend.UI.ViewModels;
using NarakaBladepoint.Modules.Social.UI.Friend.UI.Views;
using NarakaBladepoint.Modules.Social.UI.Music.ViewModels;
using NarakaBladepoint.Modules.Social.UI.Music.Views;
using NarakaBladepoint.Modules.Social.UI.Setting.ViewModels;
using NarakaBladepoint.Modules.Social.UI.Setting.Views;
using NarakaBladepoint.Modules.Social.UI.Social.ViewModels;
using NarakaBladepoint.Modules.Social.UI.Social.Views;
using NarakaBladepoint.Modules.Social.UI.StatusTag.ViewModels;
using NarakaBladepoint.Modules.Social.UI.StatusTag.Views;

namespace NarakaBladepoint.Modules.Social.UI.Module
{
    /// <summary>
    /// Social模块，注册社交相关页面和ViewModel
    /// 包括：设置、邮箱、状态标签、音乐、好友列表等
    /// </summary>
    internal class SocialModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider) { }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // 注册设置页面
            containerRegistry.RegisterForNavigation<SettingPage, SettingPageViewModel>();

            // 注册邮箱页面
            containerRegistry.RegisterForNavigation<EmailPage, EmailPageViewModel>();

            // 注册状态标签页面
            containerRegistry.RegisterForNavigation<StatusTagPage, StatusTagPageViewModel>();

            // 注册音乐页面
            containerRegistry.RegisterForNavigation<MusicPage, MusicPageViewModel>();

            // 注册好友页面
            containerRegistry.RegisterForNavigation<FriendPage, FriendPageViewModel>();

            // 注册社交主页面及ViewModel（不用于导航）
            containerRegistry.Register<SocialPage>();
            containerRegistry.Register<SocialPageViewModel>();
        }
    }
}
