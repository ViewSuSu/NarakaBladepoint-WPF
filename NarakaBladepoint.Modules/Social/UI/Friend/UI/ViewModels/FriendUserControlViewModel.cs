using System.Collections.ObjectModel;
using NarakaBladepoint.Shared.Datas;
using NarakaBladepoint.Shared.Services.Abstractions;
using NarakaBladepoint.Shared.Services.Models;

namespace NarakaBladepoint.Modules.Social.UI.Friend.UI.ViewModels
{
    internal class FriendUserControlViewModel : ViewModelBase
    {
        private readonly ICurrentUserFriendInfo _friendService;
        private readonly IEventAggregator _eventAggregator;

        // 好友列表数据
        private ObservableCollection<FriendData> _friends = new ObservableCollection<FriendData>();

        public ObservableCollection<FriendData> Friends
        {
            get => _friends;
            set => SetProperty(ref _friends, value);
        }

        public FriendUserControlViewModel(
            IContainerExtension containerExtension,
            ICurrentUserInformationProvider currentUserInformationProvider
        )
            : base(containerExtension)
        {
            CloseCommand = new DelegateCommand(() =>
            {
                _eventAggregator.GetEvent<RemoveHomePageRegionEvent>().Publish();
            });
            this.CurrentUserInfoModel = currentUserInformationProvider
                .GetCurrentUserInfoAsync()
                .Result;
            SettingTagCommand = new DelegateCommand(() => { });
            SearchCommand = new DelegateCommand<string>(keyword => { });
            SayHelloCommand = new DelegateCommand(() => { });
            LoadDataAsync();
        }

        public DelegateCommand CloseCommand { get; }
        public UserInformationModel CurrentUserInfoModel { get; }
        public DelegateCommand SettingTagCommand { get; }
        public DelegateCommand SayHelloCommand { get; }
        public DelegateCommand<string> SearchCommand { get; }

        private async Task LoadDataAsync()
        {
            try
            {
                IEnumerable<FriendData> friendList = await _friendService.GetFriendsAsync();
                Friends.Clear();
                foreach (var friend in friendList)
                {
                    Friends.Add(friend);
                }
            }
            catch (Exception ex)
            {
                // 处理异常
                Console.WriteLine($"加载好友列表失败: {ex.Message}");
            }
            finally { }
        }
    }
}
