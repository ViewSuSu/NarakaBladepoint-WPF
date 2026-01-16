using NarakaBladepoint.Framework.Core.Evens;
using NarakaBladepoint.Modules.CommonFunction.Domain.Events;
using NarakaBladepoint.Modules.CommonFunction.UI.CustomMatch.Views;
using NarakaBladepoint.Modules.CommonFunction.UI.Hero.Views;
using NarakaBladepoint.Modules.CommonFunction.UI.Inventory.Views;
using NarakaBladepoint.Modules.CommonFunction.UI.Leaderboard.Views;
using NarakaBladepoint.Modules.CommonFunction.UI.SkillPoint.Views;
using NarakaBladepoint.Modules.CommonFunction.UI.Store.Views;
using NarakaBladepoint.Modules.CommonFunction.UI.Weapon.Views;

namespace NarakaBladepoint.Modules.CommonFunction.UI.CommonFunction.ViewModels
{
    public partial class CommonFunctionPageViewModel : ViewModelBase
    {
        private bool _isSelectedHall = true;

        public bool IsSelectedHall
        {
            get { return _isSelectedHall; }
            set
            {
                _isSelectedHall = value;
                RaisePropertyChanged();
                if (IsSelectedHall)
                {
                    eventAggregator.GetEvent<RemoveMainContentRegionEvent>().Publish();
                }
            }
        }

        public CommonFunctionPageViewModel(IContainerProvider containerProvider)
            : base(containerProvider)
        {
            eventAggregator
                .GetEvent<NavigateToHallEvent>()
                .Subscribe(() =>
                {
                    IsSelectedHall = true;
                });
        }

        private DelegateCommand _navigateToToWeaponCommand;
        public DelegateCommand NavigateToWeaponCommand =>
            _navigateToToWeaponCommand ??= new DelegateCommand(() =>
            {
                eventAggregator
                    .GetEvent<LoadMainContentRegionEvent>()
                    .Publish(new NavigationArgs(nameof(WeaponPage)));
            });

        private DelegateCommand _navigateToHeroCommand;
        public DelegateCommand NavigateToHeroCommand =>
            _navigateToHeroCommand ??= new DelegateCommand(() =>
            {
                eventAggregator
                    .GetEvent<LoadMainContentRegionEvent>()
                    .Publish(new NavigationArgs(nameof(HeroListPage)));
            });

        private DelegateCommand _skillPointCommand;
        public DelegateCommand SkillPointCommand =>
            _skillPointCommand ??= new DelegateCommand(() =>
            {
                eventAggregator
                    .GetEvent<LoadMainContentRegionEvent>()
                    .Publish(new NavigationArgs(nameof(SkillPointPage)));
            });

        private DelegateCommand _leaderboardCommand;
        public DelegateCommand LeaderboardCommand =>
            _leaderboardCommand ??= new DelegateCommand(() =>
            {
                eventAggregator
                    .GetEvent<LoadMainContentRegionEvent>()
                    .Publish(new NavigationArgs(nameof(LeaderboardPage)));
            });

        private DelegateCommand _navigateToInventoryCommand;
        public DelegateCommand NavigateToInventoryCommand =>
            _navigateToInventoryCommand ??= new DelegateCommand(() =>
            {
                eventAggregator
                    .GetEvent<LoadMainContentRegionEvent>()
                    .Publish(new NavigationArgs(nameof(InventoryPage)));
            });

        private DelegateCommand _storeCommand;
        public DelegateCommand StoreCommand =>
            _storeCommand ??= new DelegateCommand(() =>
            {
                eventAggregator
                    .GetEvent<LoadMainContentRegionEvent>()
                    .Publish(new NavigationArgs(nameof(StorePage)));
            });

        private DelegateCommand _customMatchCommand;
        public DelegateCommand CustomMatchCommand =>
            _customMatchCommand ??= new DelegateCommand(() =>
            {
                eventAggregator
                    .GetEvent<LoadMainContentRegionEvent>()
                    .Publish(new NavigationArgs(nameof(CustomMatchPage)));
            });
    }
}
