using NarakaBladepoint.Modules.CommonFunction.UI.CustomMatch.Views;
using NarakaBladepoint.Modules.CommonFunction.UI.Hero.Views;
using NarakaBladepoint.Modules.CommonFunction.UI.Inventory.Views;
using NarakaBladepoint.Modules.CommonFunction.UI.Leaderboard.Views;
using NarakaBladepoint.Modules.CommonFunction.UI.SkillPoint.Views;
using NarakaBladepoint.Modules.CommonFunction.UI.Store.Views;

namespace NarakaBladepoint.Modules.CommonFunction.UI.CommonFunction.ViewModels
{
    public partial class CommonFunctionUserControlViewModel : ViewModelBase
    {
        public CommonFunctionUserControlViewModel(IContainerProvider containerProvider)
            : base(containerProvider)
        {
        }

        private DelegateCommand _navigateToHeroCommand;
        public DelegateCommand NavigateToHeroCommand =>
            _navigateToHeroCommand ??= new DelegateCommand(() =>
            {
                eventAggregator.GetEvent<LoadMainContentRegionEvent>()
                    .Publish(nameof(HeroListPage));
            });

        private DelegateCommand _navigateToHallCommand;
        public DelegateCommand NavigateToHallCommand =>
            _navigateToHallCommand ??= new DelegateCommand(() =>
            {
                eventAggregator.GetEvent<RemoveMainContentRegionEvent>().Publish();
            });

        private DelegateCommand _skillPointCommand;
        public DelegateCommand SkillPointCommand =>
            _skillPointCommand ??= new DelegateCommand(() =>
            {
                eventAggregator.GetEvent<LoadMainContentRegionEvent>()
                    .Publish(nameof(SkillPointUserControl));
            });

        private DelegateCommand _leaderboardCommand;
        public DelegateCommand LeaderboardCommand =>
            _leaderboardCommand ??= new DelegateCommand(() =>
            {
                eventAggregator.GetEvent<LoadMainContentRegionEvent>()
                    .Publish(nameof(LeaderboardUserControl));
            });

        private DelegateCommand _navigateToInventoryCommand;
        public DelegateCommand NavigateToInventoryCommand =>
            _navigateToInventoryCommand ??= new DelegateCommand(() =>
            {
                eventAggregator.GetEvent<LoadMainContentRegionEvent>()
                    .Publish(nameof(InventoryUserControl));
            });

        private DelegateCommand _storeCommand;
        public DelegateCommand StoreCommand =>
            _storeCommand ??= new DelegateCommand(() =>
            {
                eventAggregator.GetEvent<LoadMainContentRegionEvent>()
                    .Publish(nameof(StoreUserControl));
            });

        private DelegateCommand _customMatchCommand;
        public DelegateCommand CustomMatchCommand =>
            _customMatchCommand ??= new DelegateCommand(() =>
            {
                eventAggregator.GetEvent<LoadMainContentRegionEvent>()
                    .Publish(nameof(CustomMatchUserControl));
            });
    }
}
