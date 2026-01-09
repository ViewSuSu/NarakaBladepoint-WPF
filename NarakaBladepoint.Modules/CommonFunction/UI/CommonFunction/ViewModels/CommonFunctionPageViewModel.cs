using NarakaBladepoint.Modules.CommonFunction.UI.CustomMatch.Views;
using NarakaBladepoint.Modules.CommonFunction.UI.Hero.Views;
using NarakaBladepoint.Modules.CommonFunction.UI.Inventory.Views;
using NarakaBladepoint.Modules.CommonFunction.UI.Leaderboard.Views;
using NarakaBladepoint.Modules.CommonFunction.UI.SkillPoint.Views;
using NarakaBladepoint.Modules.CommonFunction.UI.Store.Views;

namespace NarakaBladepoint.Modules.CommonFunction.UI.CommonFunction.ViewModels
{
    public partial class CommonFunctionPageViewModel : ViewModelBase
    {
        public CommonFunctionPageViewModel(IContainerProvider containerProvider)
            : base(containerProvider) { }

        private DelegateCommand _navigateToHeroCommand;

        public DelegateCommand NavigateToHeroCommand =>
            _navigateToHeroCommand ??= new DelegateCommand(() =>
            {
                eventAggregator
                    .GetEvent<LoadMainContentRegionEvent>()
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
                eventAggregator
                    .GetEvent<LoadMainContentRegionEvent>()
                    .Publish(nameof(SkillPointPage));
            });

        private DelegateCommand _leaderboardCommand;

        public DelegateCommand LeaderboardCommand =>
            _leaderboardCommand ??= new DelegateCommand(() =>
            {
                eventAggregator
                    .GetEvent<LoadMainContentRegionEvent>()
                    .Publish(nameof(LeaderboardPage));
            });

        private DelegateCommand _navigateToInventoryCommand;

        public DelegateCommand NavigateToInventoryCommand =>
            _navigateToInventoryCommand ??= new DelegateCommand(() =>
            {
                eventAggregator
                    .GetEvent<LoadMainContentRegionEvent>()
                    .Publish(nameof(InventoryPage));
            });

        private DelegateCommand _storeCommand;

        public DelegateCommand StoreCommand =>
            _storeCommand ??= new DelegateCommand(() =>
            {
                eventAggregator.GetEvent<LoadMainContentRegionEvent>().Publish(nameof(StorePage));
            });

        private DelegateCommand _customMatchCommand;

        public DelegateCommand CustomMatchCommand =>
            _customMatchCommand ??= new DelegateCommand(() =>
            {
                eventAggregator
                    .GetEvent<LoadMainContentRegionEvent>()
                    .Publish(nameof(CustomMatchPage));
            });
    }
}
