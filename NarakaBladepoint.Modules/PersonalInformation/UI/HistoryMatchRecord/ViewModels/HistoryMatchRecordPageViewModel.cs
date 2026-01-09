using NarakaBladepoint.Shared.Jsons;
using NarakaBladepoint.Shared.Services.Abstractions;

namespace NarakaBladepoint.Modules.PersonalInformation.UI.HistoryMatchRecord.ViewModels
{
    internal class HistoryMatchRecordPageViewModel : ViewModelBase
    {
        private readonly ICurrentUserInfoProvider currentUserBasicInformation;
        public List<MatchDataItem> MatchDataItems { get; }

        public HistoryMatchRecordPageViewModel(
            IContainerProvider containerProvider,
            ICurrentUserInfoProvider matchDataInfomation
        )
            : base(containerProvider)
        {
            this.currentUserBasicInformation = matchDataInfomation;
            this.MatchDataItems = this.currentUserBasicInformation.GetMatchDataItemsAsync().Result;
        }
    }
}
