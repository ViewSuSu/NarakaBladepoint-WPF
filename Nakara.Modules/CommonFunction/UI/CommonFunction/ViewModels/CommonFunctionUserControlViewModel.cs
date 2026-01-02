using Nakara.Modules.CommonFunction.UI.CustomMatch.Views;
using Nakara.Modules.CommonFunction.UI.Hero.Views;
using Nakara.Modules.CommonFunction.UI.Inventory.Views;
using Nakara.Modules.CommonFunction.UI.Leaderboard.Views;
using Nakara.Modules.CommonFunction.UI.SkillPoint.Views;
using Nakara.Modules.CommonFunction.UI.Store.Views;

namespace Nakara.Modules.CommonFunction.UI.CommonFunction.ViewModels
{
    public partial class CommonFunctionUserControlViewModel : ViewModelBase
    {
        public CommonFunctionUserControlViewModel(IContainerExtension containerExtension)
            : base(containerExtension)
        {
            NavigateToHeroCommand = new DelegateCommand(() =>
            {
                this.eventAggregator.GetEvent<LoadMainContentRegionEvent>()
                    .Publish(nameof(HeroListPage));
            });
            NavigateToHallCommand = new DelegateCommand(() =>
            {
                this.eventAggregator.GetEvent<RemoveMainContentRegionEvent>().Publish();
            });
            SkillPointCommand = new DelegateCommand(() =>
            {
                this.eventAggregator.GetEvent<LoadMainContentRegionEvent>()
                    .Publish(nameof(SkillPointUserControl));
            });
            LeaderboardCommand = new DelegateCommand(() =>
            {
                this.eventAggregator.GetEvent<LoadMainContentRegionEvent>()
                    .Publish(nameof(LeaderboardUserControl));
            });
            NavigateToInventoryCommand = new DelegateCommand(() =>
            {
                this.eventAggregator.GetEvent<LoadMainContentRegionEvent>()
                    .Publish(nameof(InventoryUserControl));
            });
            StoreCommand = new DelegateCommand(() =>
            {
                this.eventAggregator.GetEvent<LoadMainContentRegionEvent>()
                    .Publish(nameof(StoreUserControl));
            });
            CustomMatchCommand = new DelegateCommand(() =>
            {
                this.eventAggregator.GetEvent<LoadMainContentRegionEvent>()
                    .Publish(nameof(CustomMatchUserControl));
            });
        }

        public DelegateCommand NavigateToHeroCommand { get; }
        public DelegateCommand NavigateToHallCommand { get; }
        public DelegateCommand SkillPointCommand { get; }
        public DelegateCommand LeaderboardCommand { get; }
        public DelegateCommand NavigateToInventoryCommand { get; }
        public DelegateCommand StoreCommand { get; }
        public DelegateCommand CustomMatchCommand { get; }
    }
}
