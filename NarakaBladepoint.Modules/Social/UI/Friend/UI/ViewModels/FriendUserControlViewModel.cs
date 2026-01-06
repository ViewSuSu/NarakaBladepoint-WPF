using System.Collections.ObjectModel;
using NarakaBladepoint.Shared.Datas;
using NarakaBladepoint.Shared.Services.Abstractions;
using NarakaBladepoint.Shared.Services.Models;

namespace NarakaBladepoint.Modules.Social.UI.Friend.UI.ViewModels
{
    internal class FriendUserControlViewModel : ViewModelBase
    {
        private readonly ICurrentUserFriendInfo currentUserFriendInfo;

        private List<FriendDataItem> _friends = [];
        public List<FriendDataItem> Friends
        {
            get { return _friends; }
            set
            {
                _friends = value;
                RaisePropertyChanged();
            }
        }

        public FriendUserControlViewModel(
            IContainerProvider containerProvider,
            ICurrentUserInformationProvider currentUserInformationProvider,
            ICurrentUserFriendInfo currentUserFriendInfo
        )
            : base(containerProvider)
        {
            this.currentUserFriendInfo = currentUserFriendInfo;
            Friends = currentUserFriendInfo.GetFriendsAsync().Result;

            CloseCommand = new DelegateCommand(() =>
            {
                eventAggregator.GetEvent<RemoveHomePageRegionEvent>().Publish();
            });
            this.CurrentUserInfoModel = currentUserInformationProvider
                .GetCurrentUserInfoAsync()
                .Result;
            SettingTagCommand = new DelegateCommand(() => { });
            SearchCommand = new DelegateCommand<string>(keyword =>
            {
                Friends = this.currentUserFriendInfo.GetFriendsAsync(keyword).Result;
            });
            SayHelloCommand = new DelegateCommand(() => { });
        }

        public DelegateCommand CloseCommand { get; }
        public UserInformationModel CurrentUserInfoModel { get; }
        public DelegateCommand SettingTagCommand { get; }
        public DelegateCommand SayHelloCommand { get; }
        public DelegateCommand<string> SearchCommand { get; }
    }
}
