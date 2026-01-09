using NarakaBladepoint.Modules.TopUp.UI.Views;
using NarakaBladepoint.Modules.Wealth.UI.Main.Models;

namespace NarakaBladepoint.Modules.Wealth.UI.Main.ViewModels
{
    public partial class WealthPageViewModel : BindableBase
    {
        private readonly IEventAggregator eventAggregator;

        public WealthModel WealthModel { get; } =
            new WealthModel()
            {
                GoldBrick = 12500,
                AncientCoins = 1111,
                HuanSi = 108520,
            };

        private DelegateCommand _navigateToTopUpCommand;

        public DelegateCommand NavigateToTopUpCommand =>
            _navigateToTopUpCommand ??= new DelegateCommand(() =>
            {
                this.eventAggregator.GetEvent<LoadHomePageRegionEvent>().Publish(nameof(TopUpPage));
            });

        public WealthPageViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
        }
    }
}
