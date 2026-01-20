using NarakaBladepoint.Framework.Core.Evens;
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
            this.CurrentUserInformationModel = currentUserInformationProvider
                .GetCurrentUserInfoAsync()
                .Result;
        }

        public UserInformationData CurrentUserInformationModel { get; }
    }
}
