using Nakara.Modules.Social.Domain.FriendList.Interfaces;
using Nakara.Modules.Social.Infrastructure.FriendList;
using Nakara.Modules.Social.UI.FriendList.ViewModels;
using Nakara.Modules.Social.UI.FriendList.Views;
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
            containerRegistry.Register<SocialUserControl>();
            containerRegistry.Register<SocialUserControlViewModel>();
            containerRegistry.Register<IFriendService, FriendService>();
        }
    }
}
