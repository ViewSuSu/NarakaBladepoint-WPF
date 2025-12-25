using Nakara_WPF.Modules.Social.FriendList.Services;
using Nakara_WPF.Modules.Social.FriendList.ViewModels;
using Nakara_WPF.Modules.Social.FriendList.Views;
using Nakara_WPF.Modules.Social.ViewModels;
using Nakara_WPF.Modules.Social.Views;

namespace Nakara_WPF.Modules.Social
{
    class SocialModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider) { }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<
                FriendListUserControl,
                FriendListUserControlViewModel
            >();
            containerRegistry.Register<SocialUserControl>();
            containerRegistry.Register<SocialUserControlViewModel>();
            containerRegistry.Register<IFriendService, FriendService>();
        }
    }
}
