using NarakaBladepoint.Framework.Core.Evens;
using NarakaBladepoint.Modules.EventCenter.UI.Main.Views;
using NarakaBladepoint.Modules.StartGame.UI.ModeSelection.Views;

namespace NarakaBladepoint.Modules.StartGame.UI.StartGame.ViewModels
{
    internal class StartGamePageViewModel : BindableBase
    {
        private readonly IEventAggregator eventAggregator;

        public StartGamePageViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
        }

        private DelegateCommand _navigateToEventCenterMainCommand;

        public DelegateCommand NavigateToEventCenterMainCommand =>
            _navigateToEventCenterMainCommand ??= new DelegateCommand(() =>
            {
                eventAggregator
                    .GetEvent<LoadHomePageRegionEvent>()
                    .Publish(new NavigationArgs(nameof(EventCenterMainPage)));
            });

        private DelegateCommand _navigateToModelSelectionCommand;

        public DelegateCommand NavigateToModelSelectionCommand =>
            _navigateToModelSelectionCommand ??= new DelegateCommand(() =>
            {
                eventAggregator
                    .GetEvent<LoadHomePageRegionEvent>()
                    .Publish(new NavigationArgs(nameof(ModeSelectionPage)));
            });
    }
}
