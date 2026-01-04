using NarakaBladepoint.Modules.BattlePass.UI.BattlePassDetails.Views;

namespace NarakaBladepoint.Modules.BattlePass.UI.BattlePass.ViewModels
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
