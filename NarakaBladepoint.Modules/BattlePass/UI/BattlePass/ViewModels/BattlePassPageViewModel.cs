using NarakaBladepoint.Modules.BattlePass.UI.BattlePass.Views;

namespace NarakaBladepoint.Modules.BattlePass.UI.BattlePass.ViewModels
{
    internal class BattlePassPageViewModel : ViewModelBase
    {
        public BattlePassPageViewModel(ICurrentUserInfoProvider currentUserInformationProvider)
        {
            this.CurrentUserInformationModel = currentUserInformationProvider
                .GetCurrentUserInfoAsync()
                .Result;
        }

        public UserInformationData CurrentUserInformationModel { get; }

        private DelegateCommand _navigateToContentCommand;

        public DelegateCommand NavigateToContentCommand =>
            _navigateToContentCommand ??= new DelegateCommand(() =>
            {
                eventAggregator
                    .GetEvent<LoadMainContentRegionEvent>()
                    .Publish(new NavigationArgs(nameof(BattlePassContentPage)));
            });
    }
}
