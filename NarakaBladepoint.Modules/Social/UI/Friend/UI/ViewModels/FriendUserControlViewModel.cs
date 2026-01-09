using System.Collections.ObjectModel;
using NarakaBladepoint.Shared.Datas;
using NarakaBladepoint.Shared.Services.Abstractions;

namespace NarakaBladepoint.Modules.Social.UI.Friend.UI.ViewModels
{
    internal class FriendUserControlViewModel : ViewModelBase
    {
        private List<FriendDataItem> _friends = [];
        private readonly ICurrentUserInfoProvider currentUserInformationProvider;

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
            ICurrentUserInfoProvider currentUserInformationProvider
        )
            : base(containerProvider)
        {
            this.currentUserInformationProvider = currentUserInformationProvider;
            Friends = currentUserInformationProvider.GetFriendsAsync().Result;

            CloseCommand = new DelegateCommand(() =>
            {
                eventAggregator.GetEvent<RemoveHomePageRegionEvent>().Publish();
            });
            this.CurrentUserInfoModel = currentUserInformationProvider
                .GetCurrentUserInfoAsync()
                .Result;
            SettingTagCommand = new DelegateCommand(() =>
            {
                eventAggregator.GetEvent<LoadHomePageRegionEvent>().Publish("!23");
            });
            SearchCommand = new DelegateCommand<string>(keyword =>
            {
                Friends = this.currentUserInformationProvider.GetFriendsAsync(keyword).Result;
            });
            SayHelloCommand = new DelegateCommand(() => { });
        }

        public DelegateCommand CloseCommand { get; }
        public UserInformationData CurrentUserInfoModel { get; }
        public DelegateCommand SettingTagCommand { get; }
        public DelegateCommand SayHelloCommand { get; }
        public DelegateCommand<string> SearchCommand { get; }
    }
}
