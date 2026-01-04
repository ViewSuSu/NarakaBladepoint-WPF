namespace NarakaBladepoint.Modules.BattlePass.UI.BattlePassDetails.ViewModels
{
    internal class BattlePassMainContentUserControlViewModel
    {
        private readonly IEventAggregator eventAggregator;

        public BattlePassMainContentUserControlViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
            //NavigateToPassportCommand = new DelegateCommand(() =>
            //{
            //    this.eventAggregator.GetEvent<LoadBattlePassDetailMainContentRegionEvent>().Subscribe(OnLoadBattlePassDetailMainContentRegion);
            //});
            //NavigateToTaskCommand = new DelegateCommand(() =>
            //{
            //    this.eventAggregator.GetEvent<LoadBattlePassDetailMainContentRegionEvent>().Subscribe(OnLoadBattlePassDetailMainContentRegion);
            //});
            //NavigateToSeasonRewardCommand = new DelegateCommand(() =>
            //{
            //    this.eventAggregator.GetEvent<LoadBattlePassDetailMainContentRegionEvent>().Subscribe(OnLoadBattlePassDetailMainContentRegion);
            //});
        }

        public DelegateCommand NavigateToPassportCommand { get; }
        public DelegateCommand NavigateToTaskCommand { get; }
        public DelegateCommand NavigateToSeasonRewardCommand { get; }
    }
}
