using NarakaBladepoint.Modules.EventCenter.UI.Main.Views;
using NarakaBladepoint.Modules.StartGame.UI.ModeSelection.Views;

namespace NarakaBladepoint.Modules.StartGame.UI.StartGame.ViewModels
{
    internal class StartGamePageViewModel : ViewModelBase
    {
        private bool _isLoadingGame;

        public bool IsLoadingGame
        {
            get { return _isLoadingGame; }
            set
            {
                if (_isLoadingGame == value)
                {
                    return;
                }
                _isLoadingGame = value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(StartGameContent));
            }
        }

        public string StartGameContent => IsLoadingGame ? "取消" : "开始游戏";

        public StartGamePageViewModel(ITipMessageService tipMessageService)
        {
            this.tipMessageService = tipMessageService;
            eventAggregator
                .GetEvent<QueueStatusChangedEvent>()
                .Subscribe(isload =>
                {
                    IsLoadingGame = isload;
                });
        }

        private DelegateCommand _startGameCommand;

        /// <summary>
        /// 开始游戏命令
        /// </summary>
        public DelegateCommand StartGameCommand =>
            _startGameCommand ??= new DelegateCommand(() =>
            {
                IsLoadingGame = !_isLoadingGame;
                eventAggregator.GetEvent<QueueStatusChangedEvent>().Publish(IsLoadingGame);
            });

        private DelegateCommand _navigateToEventCenterMainCommand;

        public DelegateCommand NavigateToEventCenterMainCommand =>
            _navigateToEventCenterMainCommand ??= new DelegateCommand(() =>
            {
                eventAggregator
                    .GetEvent<LoadHomePageRegionEvent>()
                    .Publish(new NavigationArgs(nameof(EventCenterMainPage)));
            });

        private DelegateCommand _navigateToModelSelectionCommand;
        private readonly ITipMessageService tipMessageService;

        public DelegateCommand NavigateToModelSelectionCommand =>
            _navigateToModelSelectionCommand ??= new DelegateCommand(() =>
            {
                eventAggregator
                    .GetEvent<LoadHomePageRegionEvent>()
                    .Publish(new NavigationArgs(nameof(ModeSelectionPage)));
            });
    }
}
