using System;
using System.Windows.Media;
using NarakaBladepoint.Modules.StartGame.UI.HeroChose.Views;
using NarakaBladepoint.Modules.StartGame.UI.MapChose.Views;
using NarakaBladepoint.Shared.Services.Models;

namespace NarakaBladepoint.Modules.StartGame.UI.ModeSelection.ViewModels
{
    internal class ModeSelectionPageViewModel : CanRemoveHomePageRegionViewModelBase
    {
        private IEnumerable<ServerInformationModel> _serverInfos;

        public IEnumerable<ServerInformationModel> ServerInfos
        {
            get { return _serverInfos; }
            set
            {
                _serverInfos = value;
                RaisePropertyChanged();
            }
        }

        private ServerInformationModel selectedItem;
        private readonly IServerInfoProvider serverInformation;
        private readonly ICurrentUserInfoProvider currentUserInformationProvider;
        private readonly IHeroInfoProvider heroInfomation;
        private readonly IMapProvider mapProvider;

        public ServerInformationModel SelectedItem
        {
            get { return selectedItem; }
            set
            {
                selectedItem = value;
                RaisePropertyChanged();
            }
        }

        private ImageSource _firstHeroAvatar;
        private ImageSource _secondHeroAvatar;
        private ImageSource _thirdHeroAvatar;

        public ImageSource FirstHeroAvatar
        {
            get => _firstHeroAvatar;
            private set
            {
                if (_firstHeroAvatar != value)
                {
                    _firstHeroAvatar = value;
                    RaisePropertyChanged();
                }
            }
        }

        public ImageSource SecondHeroAvatar
        {
            get => _secondHeroAvatar;
            private set
            {
                if (_secondHeroAvatar != value)
                {
                    _secondHeroAvatar = value;
                    RaisePropertyChanged();
                }
            }
        }

        public ImageSource ThirdHeroAvatar
        {
            get => _thirdHeroAvatar;
            private set
            {
                if (_thirdHeroAvatar != value)
                {
                    _thirdHeroAvatar = value;
                    RaisePropertyChanged();
                }
            }
        }

        private int _selectedMapCount;

        public int SelectedMapCount
        {
            get { return _selectedMapCount; }
            set
            {
                _selectedMapCount = value;
                RaisePropertyChanged();
            }
        }

        private bool isSoloMode;

        /// <summary>
        /// 是否是单排
        /// </summary>
        public bool IsSoloMode
        {
            get { return isSoloMode; }
            set
            {
                isSoloMode = value;
                RaisePropertyChanged();
            }
        }

        private int _selectedModeIndex;

        /// <summary>
        /// 选中的模式索引
        /// </summary>
        public int SelectedModeIndex
        {
            get { return _selectedModeIndex; }
            set
            {
                _selectedModeIndex = value;
                RaisePropertyChanged();
            }
        }

        private int _selectedTabIndex;

        /// <summary>
        /// 选中的标签页索引
        /// </summary>
        public int SelectedTabIndex
        {
            get { return _selectedTabIndex; }
            set
            {
                _selectedTabIndex = value;
                RaisePropertyChanged();
            }
        }

        private TimeSpan _entertainmentLimitedTimeRemaining = TimeSpan.FromHours(3);

        /// <summary>
        /// 娱乐模式限时剩余时间
        /// </summary>
        public TimeSpan EntertainmentLimitedTimeRemaining
        {
            get { return _entertainmentLimitedTimeRemaining; }
            set
            {
                _entertainmentLimitedTimeRemaining = value;
                RaisePropertyChanged();
            }
        }

        public ModeSelectionPageViewModel(
            IServerInfoProvider serverInformation,
            ICurrentUserInfoProvider currentUserInformationProvider,
            IHeroInfoProvider heroInfomation,
            IMapProvider mapProvider
        )
        {
            this.serverInformation = serverInformation;
            this.currentUserInformationProvider = currentUserInformationProvider;
            this.heroInfomation = heroInfomation;
            this.mapProvider = mapProvider;
        }

        private DelegateCommand _choseHeroCommand;

        public DelegateCommand ChoseHeroCommand =>
            _choseHeroCommand ??= new DelegateCommand(() =>
            {
                eventAggregator
                    .GetEvent<LoadHomePageRegionEvent>()
                    .Publish(new NavigationArgs(nameof(HeroChosePage)));
            });

        private DelegateCommand _choseMapCommand;

        public DelegateCommand ChoseMapCommand =>
            _choseMapCommand ??= new DelegateCommand(() =>
            {
                eventAggregator
                    .GetEvent<LoadHomePageRegionEvent>()
                    .Publish(new NavigationArgs(nameof(MapChosePage)));
            });

        private DelegateCommand<int?> _selectModeCommand;

        public DelegateCommand<int?> SelectModeCommand =>
            _selectModeCommand ??= new DelegateCommand<int?>(modeIndex =>
            {
                if (modeIndex.HasValue)
                {
                    SelectedModeIndex = modeIndex.Value;
                }
            });

        protected override async void OnNavigatedToExecute(NavigationContext navigationContext)
        {
            this.ServerInfos = await serverInformation.GeAlltServerInfosAsync();
            SelectedItem = ServerInfos.FirstOrDefault();

            var userModel = await currentUserInformationProvider.GetCurrentUserInfoAsync();
            this.FirstHeroAvatar =
                userModel.FirstPickHeroIndex != -1
                    ? (await heroInfomation
                        .GetHeroAvatarModelByIdAsync(userModel.FirstPickHeroIndex)).Avatar
                    : null;
            this.SecondHeroAvatar =
                userModel.SecondPickHeroIndex != -1
                    ? (await heroInfomation
                        .GetHeroAvatarModelByIdAsync(userModel.SecondPickHeroIndex)).Avatar
                    : null;
            this.ThirdHeroAvatar =
                userModel.ThridPickHeroIndex != -1
                    ? (await heroInfomation
                        .GetHeroAvatarModelByIdAsync(userModel.ThridPickHeroIndex)).Avatar
                    : null;
            this.SelectedMapCount = await mapProvider.GetSelectedMapCountAsync();

            // 启动异步倒计时
            StartEntertainmentCountdown();
        }

        private System.Threading.CancellationTokenSource _countdownCancellation;

        private async void StartEntertainmentCountdown()
        {
            _countdownCancellation?.Cancel();
            _countdownCancellation = new System.Threading.CancellationTokenSource();

            try
            {
                EntertainmentLimitedTimeRemaining = TimeSpan.FromHours(3);

                while (EntertainmentLimitedTimeRemaining > TimeSpan.Zero && !_countdownCancellation.Token.IsCancellationRequested)
                {
                    await System.Threading.Tasks.Task.Delay(1000, _countdownCancellation.Token);
                    EntertainmentLimitedTimeRemaining = EntertainmentLimitedTimeRemaining.Subtract(TimeSpan.FromSeconds(1));
                }
            }
            catch (System.Threading.Tasks.TaskCanceledException)
            {
                // 倒计时被取消
            }
        }
    }
}
