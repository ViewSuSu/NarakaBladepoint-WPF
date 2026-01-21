using NarakaBladepoint.Framework.Core.Evens;
using NarakaBladepoint.Modules.TopUp.UI.Views;
using NarakaBladepoint.Shared.Datas;
using NarakaBladepoint.Shared.Services.Abstractions;

namespace NarakaBladepoint.Modules.Wealth.UI.Main.ViewModels
{
    public partial class WealthPageViewModel : ViewModelBase
    {
        public UserInformationData CurrentUserModel { get; }

        private DelegateCommand _navigateToTopUpCommand;

        public WealthPageViewModel(
            ICurrentUserInfoProvider currentUserInfoProvider
        )
        {
            this.CurrentUserModel = currentUserInfoProvider.GetCurrentUserInfoAsync().Result;
        }

        public DelegateCommand NavigateToTopUpCommand =>
            _navigateToTopUpCommand ??= new DelegateCommand(() =>
            {
                this.eventAggregator.GetEvent<LoadHomePageRegionEvent>()
                    .Publish(new NavigationArgs(nameof(TopUpPage)));
            });
    }
}
