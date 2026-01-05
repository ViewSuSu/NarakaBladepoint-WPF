using NarakaBladepoint.Modules.BattlePass.UI.BattlePassDetails.Views;
using NarakaBladepoint.Shared.Services.Abstractions;
using NarakaBladepoint.Shared.Services.Models;

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

        public UserInformationModel CurrentUserInformationModel { get; }
    }
}
