using Nakara.Shared.Jsons;
using Nakara.Shared.Services.Abstractions;

namespace Nakara.Modules.PersonalInformation.UI.HistoryMatchRecord.ViewModels
{
    internal class HistoryMatchRecordPageViewModel
    {
        private readonly IMatchDataInfomation matchDataInfomation;

        public List<MatchDataItem> MatchDataItems { get; }

        public HistoryMatchRecordPageViewModel(IMatchDataInfomation matchDataInfomation)
        {
            this.matchDataInfomation = matchDataInfomation;
            this.MatchDataItems = matchDataInfomation.GetMatchDataItemsAsync().Result;
        }
    }
}
