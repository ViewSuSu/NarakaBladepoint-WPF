using System.Collections.ObjectModel;
using NarakaBladepoint.Shared.Datas;
using NarakaBladepoint.Shared.Services.Abstractions;
using NarakaBladepoint.Shared.Services.Models;

namespace NarakaBladepoint.Modules.Social.UI.Friend.UI.ViewModels
{
    internal class FriendUserControlViewModel : BindableBase, IActiveAware
    {
        private readonly ICurrentUserFriendInfo _friendService;
        private readonly ICurrentUserInformationProvider currentUserInformationProvider;
        private readonly IEventAggregator _eventAggregator;
        private bool _isActive;
        private bool _isDataLoaded;

        public FriendUserControlViewModel(
            ICurrentUserFriendInfo friendService,
            ICurrentUserInformationProvider currentUserInformationProvider,
            IEventAggregator eventAggregator
        )
        {
            _friendService = friendService;
            this.currentUserInformationProvider = currentUserInformationProvider;
            _eventAggregator = eventAggregator;

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
        }

        public DelegateCommand CloseCommand { get; }
        public UserInformationModel CurrentUserInfoModel { get; }
        public DelegateCommand SettingTagCommand { get; }
        public DelegateCommand SayHelloCommand { get; }
        public DelegateCommand<string> SearchCommand { get; }

        // 好友列表数据
        private ObservableCollection<FriendData> _friends = new ObservableCollection<FriendData>();

        public ObservableCollection<FriendData> Friends
        {
            get => _friends;
            set => SetProperty(ref _friends, value);
        }

        // 加载状态
        private bool _isLoading;

        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }

        // IActiveAware 实现
        public bool IsActive
        {
            get => _isActive;
            set
            {
                if (SetProperty(ref _isActive, value))
                {
                    OnActiveChanged();
                    IsActiveChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public event EventHandler IsActiveChanged;

        private void OnActiveChanged()
        {
            if (_isActive && !_isDataLoaded)
            {
                // 只有在显示且未加载过数据时才加载
                LoadDataAsync();
            }
        }

        private async Task LoadDataAsync()
        {
            IsLoading = true;
            try
            {
                IEnumerable<FriendData> friendList = await _friendService.GetFriendsAsync();
                Friends.Clear();
                foreach (var friend in friendList)
                {
                    Friends.Add(friend);
                }
                _isDataLoaded = true;
            }
            catch (Exception ex)
            {
                // 处理异常
                Console.WriteLine($"加载好友列表失败: {ex.Message}");
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}
