using NarakaBladepoint.Shared.Datas;
using NarakaBladepoint.Shared.Services.Abstractions;

namespace NarakaBladepoint.Modules.Social.UI.Friend.UI.ViewModels
{
    internal class FriendPageViewModel : ViewModelBase
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

        public FriendPageViewModel(
            IContainerProvider containerProvider,
            ICurrentUserInfoProvider currentUserInformationProvider
        )
            : base(containerProvider)
        {
            this.currentUserInformationProvider = currentUserInformationProvider;
            Friends = currentUserInformationProvider.GetFriendsAsync().Result;
            this.CurrentUserInfoModel = currentUserInformationProvider
                .GetCurrentUserInfoAsync()
                .Result;
        }

        public UserInformationData CurrentUserInfoModel { get; }

        private DelegateCommand _closeCommand;

        public DelegateCommand CloseCommand =>
            _closeCommand ??= new DelegateCommand(() =>
            {
                eventAggregator.GetEvent<RemoveHomePageRegionEvent>().Publish();
            });

        private DelegateCommand _settingTagCommand;

        public DelegateCommand SettingTagCommand =>
            _settingTagCommand ??= new DelegateCommand(() =>
            {
                eventAggregator.GetEvent<LoadHomePageRegionEvent>().Publish("!23");
            });

        private DelegateCommand _sayHelloCommand;

        public DelegateCommand SayHelloCommand =>
            _sayHelloCommand ??= new DelegateCommand(() => { });

        private DelegateCommand<string> _searchCommand;

        public DelegateCommand<string> SearchCommand =>
            _searchCommand ??= new DelegateCommand<string>(keyword =>
            {
                Friends = currentUserInformationProvider.GetFriendsAsync(keyword).Result;
            });
    }
}
