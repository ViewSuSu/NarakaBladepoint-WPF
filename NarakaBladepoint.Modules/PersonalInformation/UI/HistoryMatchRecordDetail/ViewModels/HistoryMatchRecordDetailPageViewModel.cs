using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NarakaBladepoint.Shared.Jsons;
using Prism.Common;

namespace NarakaBladepoint.Modules.PersonalInformation.UI.HistoryMatchRecordDetail.ViewModels
{
    internal class HistoryMatchRecordDetailPageViewModel : CanRemoveHomePageRegionViewModelBase
    {
        private readonly ICurrentUserInfoProvider currentUserInfoProvider;

        public HistoryMatchRecordDetailPageViewModel(
            IContainerProvider containerProvider,
            ICurrentUserInfoProvider currentUserInfoProvider
        )
            : base(containerProvider)
        {
            this.currentUserInfoProvider = currentUserInfoProvider;
        }

        protected override void OnNavigatedToExecute(NavigationContext navigationContext)
        {
            if (
                navigationContext.Parameters.TryGetValue<MatchDataItem>(
                    nameof(MatchDataItem),
                    out var matchDataItem
                )
            ) { }
        }
    }
}
