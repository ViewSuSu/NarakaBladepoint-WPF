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
        }

        private DelegateCommand _navigateToEventCenterMainCommand;
        public DelegateCommand NavigateToEventCenterMainCommand =>
            _navigateToEventCenterMainCommand ??= new DelegateCommand(() =>
            {
                eventAggregator.GetEvent<LoadHomePageRegionEvent>()
                    .Publish(nameof(EventCenterMainUserControl));
            });

        private DelegateCommand _navigateToModelSelectionCommand;
        public DelegateCommand NavigateToModelSelectionCommand =>
            _navigateToModelSelectionCommand ??= new DelegateCommand(() =>
            {
                eventAggregator.GetEvent<LoadHomePageRegionEvent>()
                    .Publish(nameof(ModeSelectionUserControl));
            });
    }
}
