using Nakara.Modules.Social.Domain.FriendList.Interfaces;
using Nakara.Modules.Social.Infrastructure.FriendList;
using Nakara.Modules.Social.UI.Email.ViewModels;
using Nakara.Modules.Social.UI.Email.Views;
using Nakara.Modules.Social.UI.FriendList.ViewModels;
using Nakara.Modules.Social.UI.FriendList.Views;
using Nakara.Modules.Social.UI.Music.ViewModels;
using Nakara.Modules.Social.UI.Music.Views;
using Nakara.Modules.Social.UI.Setting.ViewModels;
using Nakara.Modules.Social.UI.Setting.Views;
using Nakara.Modules.Social.UI.Social.ViewModels;
using Nakara.Modules.Social.UI.Social.Views;

namespace Nakara.Modules.Social.Module
{
    internal class SocialModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider) { }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<
                FriendListUserControl,
                FriendListUserControlViewModel
            >();
            containerRegistry.RegisterForNavigation<
                SettingUserControl,
                SettingUserControlViewModel
            >();
            containerRegistry.RegisterForNavigation<EmailUserControl, EmailUserControlViewModel>();
            containerRegistry.RegisterForNavigation<MusicUserControl, MusicUserControlViewModel>();
            containerRegistry.Register<SocialUserControl>();
            containerRegistry.Register<SocialUserControlViewModel>();
            containerRegistry.Register<IFriendService, FriendService>();
        }
    }
}
