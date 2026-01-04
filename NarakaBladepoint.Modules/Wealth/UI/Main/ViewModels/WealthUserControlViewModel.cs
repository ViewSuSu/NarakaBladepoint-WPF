using NarakaBladepoint.Modules.TopUp.UI.Views;
using NarakaBladepoint.Modules.Wealth.UI.Main.Models;

namespace NarakaBladepoint.Modules.Wealth.UI.Main.ViewModels
{
    public partial class WealthUserControlViewModel : BindableBase
    {
        private readonly IEventAggregator eventAggregator;

        public WealthModel WealthModel { get; } =
            new WealthModel()
            {
                GoldBrick = 12500,
                AncientCoins = 1111,
                HuanSi = 108520,
            };

        public WealthUserControlViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
            NavigateToTopUpCommand = new DelegateCommand(() =>
            {
                this.eventAggregator.GetEvent<LoadHomePageRegionEvent>()
                    .Publish(nameof(TopUpUserControl));
            });
        }

        public DelegateCommand NavigateToTopUpCommand { get; set; }
    }
}
