using System.Collections.ObjectModel;
using Nakara.Modules.Social.Domain.FriendList;
using Nakara.Modules.Social.Domain.FriendList.Interfaces;

namespace Nakara.Modules.Social.UI.FriendList.ViewModels
{
    internal class FriendListUserControlViewModel : BindableBase, IActiveAware
    {
        private readonly IFriendService _friendService;
        private readonly IEventAggregator _eventAggregator;
        private bool _isActive;
        private bool _isDataLoaded;

        public FriendListUserControlViewModel(
            IFriendService friendService,
            IEventAggregator eventAggregator
        )
        {
            _friendService = friendService;
            _eventAggregator = eventAggregator;

            CloseCommand = new DelegateCommand(() =>
            {
                _eventAggregator.GetEvent<CloseFriendPanelEvent>().Publish();
            });

            RefreshCommand = new DelegateCommand(async () => await LoadDataAsync());
        }

        public DelegateCommand CloseCommand { get; }
        public DelegateCommand RefreshCommand { get; }

        // 好友列表数据
        private ObservableCollection<Friend> _friends = new ObservableCollection<Friend>();

        public ObservableCollection<Friend> Friends
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
                IEnumerable<Friend> friendList = await _friendService.GetFriendsAsync();
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
