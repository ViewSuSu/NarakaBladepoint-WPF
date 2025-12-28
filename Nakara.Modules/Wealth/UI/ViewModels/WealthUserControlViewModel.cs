using Nakara.Modules.TopUp.UI.Views;
using Nakara.Modules.Wealth.UI.Models;

namespace Nakara.Modules.Wealth.UI.ViewModels
{
    public partial class WealthUserControlViewModel : BindableBase
    {
        private readonly IEventAggregator eventAggregator;

        public WealthModel WealthModel { get; } =
            new WealthModel() { GoldBrick = 12500, AncientCoins = 1111 };

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
