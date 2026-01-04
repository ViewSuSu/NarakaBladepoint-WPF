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

namespace NarakaBladepoint.Modules.Social.Module
{
    internal class SocialModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider) { }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<
                SettingUserControl,
                SettingUserControlViewModel
            >();
            containerRegistry.RegisterForNavigation<EmailUserControl, EmailUserControlViewModel>();
            containerRegistry.RegisterForNavigation<MusicUserControl, MusicUserControlViewModel>();
            containerRegistry.RegisterForNavigation<
                FriendUserControl,
                FriendUserControlViewModel
            >();
            containerRegistry.Register<SocialUserControl>();
            containerRegistry.Register<SocialUserControlViewModel>();
        }
    }
}
