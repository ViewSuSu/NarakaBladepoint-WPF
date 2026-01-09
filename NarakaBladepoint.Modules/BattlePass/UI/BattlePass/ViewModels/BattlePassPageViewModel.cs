using NarakaBladepoint.Modules.BattlePass.UI.BattlePassDetails.Views;
using NarakaBladepoint.Shared.Datas;
using NarakaBladepoint.Shared.Services.Abstractions;

namespace NarakaBladepoint.Modules.BattlePass.UI.BattlePass.ViewModels
{
    internal class BattlePassPageViewModel : ViewModelBase
    {
        public BattlePassPageViewModel(
            IContainerProvider containerProvider,
            ICurrentUserInfoProvider currentUserInformationProvider
        )
            : base(containerProvider)
        {
            this.eventAggregator.GetEvent<LoadMainContentRegionEvent>()
                .Publish(nameof(BattlePassMainContentPage));

            this.CurrentUserInformationModel = currentUserInformationProvider
                .GetCurrentUserInfoAsync()
                .Result;
        }

        public UserInformationData CurrentUserInformationModel { get; }
    }
}
