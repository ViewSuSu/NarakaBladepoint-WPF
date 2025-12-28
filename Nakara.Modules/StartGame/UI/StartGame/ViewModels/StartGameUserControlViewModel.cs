using Nakara.Modules.EventCenter.UI.LatestNews.Views;
using Nakara.Modules.EventCenter.UI.Main.Views;
using Nakara.Modules.StartGame.UI.ModeSelection.Views;

namespace Nakara.Modules.StartGame.UI.StartGame.ViewModels
{
    internal class StartGameUserControlViewModel : BindableBase
    {
        private readonly IEventAggregator eventAggregator;

        public StartGameUserControlViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
            StartGameCommand = new DelegateCommand(() =>
            {
                this.eventAggregator.GetEvent<LoadHomePageRegionEvent>()
                    .Publish(nameof(ModeSelectionUserControl));
            });
            NavigateToEventCenterMainCommand = new DelegateCommand(() =>
            {
                this.eventAggregator.GetEvent<LoadHomePageRegionEvent>()
                    .Publish(nameof(EventCenterMainUserControl));
            });
        }

        public DelegateCommand StartGameCommand { get; set; }
        public DelegateCommand NavigateToEventCenterMainCommand { get; set; }
    }
}
