using NarakaBladepoint.Shared.Jsons;
using NarakaBladepoint.Shared.Services.Abstractions;

namespace NarakaBladepoint.Modules.PersonalInformation.UI.HistoryMatchRecord.ViewModels
{
    internal class HistoryMatchRecordPageViewModel : ViewModelBase
    {
        private readonly ICurrentUserInformationProvider currentUserBasicInformation;
        public List<MatchDataItem> MatchDataItems { get; }

        public HistoryMatchRecordPageViewModel(
            IContainerExtension containerExtension,
            ICurrentUserInformationProvider matchDataInfomation
        )
            : base(containerExtension)
        {
            this.currentUserBasicInformation = matchDataInfomation;
            this.MatchDataItems = this.currentUserBasicInformation.GetMatchDataItemsAsync().Result;
        }
    }
}
