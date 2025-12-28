using Nakara.Modules.BattlePass.UI.BattlePassDetails.Views;

namespace Nakara.Modules.BattlePass.UI.BattlePass.ViewModels
{
    internal class BattlePassUserControlViewModel
    {
        private readonly IEventAggregator eventAggregator;

        public BattlePassUserControlViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
            this.eventAggregator.GetEvent<LoadMainContentRegionEvent>()
                .Publish(nameof(BattlePassMainContentUserControl));
        }
    }
}
