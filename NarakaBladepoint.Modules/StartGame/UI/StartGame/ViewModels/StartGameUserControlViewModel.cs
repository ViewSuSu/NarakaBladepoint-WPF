using NarakaBladepoint.Modules.EventCenter.UI.Main.Views;
using NarakaBladepoint.Modules.StartGame.UI.ModeSelection.Views;

namespace NarakaBladepoint.Modules.StartGame.UI.StartGame.ViewModels
{
    internal class StartGameUserControlViewModel : BindableBase
    {
        private readonly IEventAggregator eventAggregator;

        public StartGameUserControlViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
            NavigateToEventCenterMainCommand = new DelegateCommand(() =>
            {
                this.eventAggregator.GetEvent<LoadHomePageRegionEvent>()
                    .Publish(nameof(EventCenterMainUserControl));
            });
            NavigateToModelSelectionCommand = new DelegateCommand(() =>
            {
                this.eventAggregator.GetEvent<LoadHomePageRegionEvent>()
                    .Publish(nameof(ModeSelectionUserControl));
            });
        }

        public DelegateCommand NavigateToEventCenterMainCommand { get; set; }
        public DelegateCommand NavigateToModelSelectionCommand { get; set; }
    }
}
