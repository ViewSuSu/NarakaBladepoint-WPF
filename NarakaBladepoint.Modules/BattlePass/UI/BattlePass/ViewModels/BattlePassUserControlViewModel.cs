using NarakaBladepoint.Modules.BattlePass.UI.BattlePassDetails.Views;
using NarakaBladepoint.Shared.Datas;
using NarakaBladepoint.Shared.Services.Abstractions;

namespace NarakaBladepoint.Modules.BattlePass.UI.BattlePass.ViewModels
{
    internal class BattlePassUserControlViewModel : ViewModelBase
    {
        public BattlePassUserControlViewModel(
            IContainerProvider containerProvider,
            ICurrentUserInformationProvider currentUserInformationProvider
        )
            : base(containerProvider)
        {
            this.eventAggregator.GetEvent<LoadMainContentRegionEvent>()
                .Publish(nameof(BattlePassMainContentUserControl));

            this.CurrentUserInformationModel = currentUserInformationProvider
                .GetCurrentUserInfoAsync()
                .Result;
        }

        public UserInformationData CurrentUserInformationModel { get; }
    }
}
