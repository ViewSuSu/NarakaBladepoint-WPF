using NarakaBladepoint.Framework.Core.Infrastructure;
using NarakaBladepoint.Modules.CommonFunction.Domain.Events;
using NarakaBladepoint.Modules.CommonFunction.UI.CustomMatch.Views;
using NarakaBladepoint.Modules.CommonFunction.UI.GuildHall.Views;
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
        public CommonFunctionPageViewModel()
        {
            eventAggregator
                .GetEvent<NavigateToHallEvent>()
                .Subscribe(() =>
                {
                    IsSelectedHall = true;
                    RemoveMainContentCommand.Execute();
                });
            MainContentNavigator.Removed += MainContentNavigator_Removed;
        }

        private DelegateCommand _removeMainContentCommand;

        public DelegateCommand RemoveMainContentCommand =>
            _removeMainContentCommand ??= new DelegateCommand(OnRemoveMainContent);

        private void OnRemoveMainContent()
        {
            eventAggregator.GetEvent<RemoveMainContentRegionEvent>().Publish();
        }

        #region Tab Selection Properties

        private bool _isSelectedHero;

        public bool IsSelectedHero
        {
            get => _isSelectedHero;
            set
            {
                _isSelectedHero = value;
                RaisePropertyChanged();
                if (value)
                {
                    eventAggregator
                        .GetEvent<LoadMainContentRegionEvent>()
                        .Publish(new NavigationArgs(nameof(HeroListPage)));
                }
            }
        }

        private bool _isSelectedWeapon;

        public bool IsSelectedWeapon
        {
            get => _isSelectedWeapon;
            set
            {
                _isSelectedWeapon = value;
                RaisePropertyChanged();
                if (value)
                {
                    eventAggregator
                        .GetEvent<LoadMainContentRegionEvent>()
                        .Publish(new NavigationArgs(nameof(WeaponPage)));
                }
            }
        }

        private bool _isSelectedSkillPoint;

        public bool IsSelectedSkillPoint
        {
            get => _isSelectedSkillPoint;
            set
            {
                _isSelectedSkillPoint = value;
                RaisePropertyChanged();
                if (value)
                {
                    eventAggregator
                        .GetEvent<LoadMainContentRegionEvent>()
                        .Publish(new NavigationArgs(nameof(SkillPointPage)));
                }
            }
        }

        private bool _isSelectedHall = true;

        public bool IsSelectedHall
        {
            get => _isSelectedHall;
            set
            {
                _isSelectedHall = value;
                RaisePropertyChanged();
            }
        }

        private bool _isSelectedLeaderboard;

        public bool IsSelectedLeaderboard
        {
            get => _isSelectedLeaderboard;
            set
            {
                _isSelectedLeaderboard = value;
                RaisePropertyChanged();
                if (value)
                {
                    eventAggregator
                        .GetEvent<LoadMainContentRegionEvent>()
                        .Publish(new NavigationArgs(nameof(LeaderboardPage)));
                }
            }
        }

        private bool _isSelectedInventory;

        public bool IsSelectedInventory
        {
            get => _isSelectedInventory;
            set
            {
                _isSelectedInventory = value;
                RaisePropertyChanged();
                if (value)
                {
                    eventAggregator
                        .GetEvent<LoadMainContentRegionEvent>()
                        .Publish(new NavigationArgs(nameof(InventoryMainContentPage)));
                }
            }
        }

        private bool _isSelectedStore;

        public bool IsSelectedStore
        {
            get => _isSelectedStore;
            set
            {
                _isSelectedStore = value;
                RaisePropertyChanged();
                if (value)
                {
                    eventAggregator
                        .GetEvent<LoadMainContentRegionEvent>()
                        .Publish(new NavigationArgs(nameof(StorePage)));
                }
            }
        }

        private bool _isSelectedCustomMatch;

        public bool IsSelectedCustomMatch
        {
            get => _isSelectedCustomMatch;
            set
            {
                _isSelectedCustomMatch = value;
                RaisePropertyChanged();
                if (value)
                {
                    eventAggregator
                        .GetEvent<LoadMainContentRegionEvent>()
                        .Publish(new NavigationArgs(nameof(CustomMatchPage)));
                }
            }
        }

        private bool _isSelectedGuildHall;

        public bool IsSelectedGuildHall
        {
            get => _isSelectedGuildHall;
            set
            {
                _isSelectedGuildHall = value;
                RaisePropertyChanged();
                if (value)
                {
                    eventAggregator
                        .GetEvent<LoadMainContentRegionEvent>()
                        .Publish(new NavigationArgs(nameof(GuildHallMainContentPage)));
                }
            }
        }

        #endregion Tab Selection Properties

        private void MainContentNavigator_Removed(object? sender, EventArgs e)
        {
            IsSelectedHall = true;
        }
    }
}
