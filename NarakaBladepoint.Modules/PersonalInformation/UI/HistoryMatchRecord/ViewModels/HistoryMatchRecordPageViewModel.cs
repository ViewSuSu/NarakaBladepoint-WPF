using NarakaBladepoint.Framework.Core.Evens;
using NarakaBladepoint.Modules.PersonalInformation.UI.HistoryMatchRecordDetail.Views;
using NarakaBladepoint.Shared.Jsons;
using NarakaBladepoint.Shared.Services.Abstractions;

namespace NarakaBladepoint.Modules.PersonalInformation.UI.HistoryMatchRecord.ViewModels
{
    internal class HistoryMatchRecordPageViewModel : ViewModelBase
    {
        private readonly ICurrentUserInfoProvider currentUserBasicInformation;
        public List<MatchDataItem> MatchDataItems { get; }

        private DelegateCommand<MatchDataItem> _detailCommand;

        public HistoryMatchRecordPageViewModel(
            IContainerProvider containerProvider,
            ICurrentUserInfoProvider matchDataInfomation
        )
            : base(containerProvider)
        {
            this.currentUserBasicInformation = matchDataInfomation;
            this.MatchDataItems = this.currentUserBasicInformation.GetMatchDataItemsAsync().Result;
        }

        public DelegateCommand<MatchDataItem> DetailCommand =>
            _detailCommand ??= new DelegateCommand<MatchDataItem>(
                (item) =>
                {
                    eventAggregator
                        .GetEvent<LoadMainContentRegionEvent>()
                        .Publish(
                            new NavigationArgs(
                                nameof(HistoryMatchRecordDetailPage),
                                new NavigationParameters() { { nameof(MatchDataItem), item } }
                            )
                        );
                }
            );
    }
}
