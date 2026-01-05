using NarakaBladepoint.Shared.Jsons;
using NarakaBladepoint.Shared.Services.Abstractions;

namespace NarakaBladepoint.Modules.PersonalInformation.UI.HistoryMatchRecord.ViewModels
{
    internal class HistoryMatchRecordPageViewModel : ViewModelBase
    {
        private readonly ICurrentUserBasicInformation currentUserBasicInformation;
        public List<MatchDataItem> MatchDataItems { get; }

        public HistoryMatchRecordPageViewModel(
            IContainerExtension containerExtension,
            ICurrentUserBasicInformation matchDataInfomation
        )
            : base(containerExtension)
        {
            this.currentUserBasicInformation = matchDataInfomation;
            this.MatchDataItems = this.currentUserBasicInformation.GetMatchDataItemsAsync().Result;
        }
    }
}
