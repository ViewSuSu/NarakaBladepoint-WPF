using Nakara.Modules.StartGame.UI.EventCenter.Views;
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
            NavigateToActivityEnterCommand = new DelegateCommand(() =>
            {
                this.eventAggregator.GetEvent<LoadHomePageRegionEvent>()
                    .Publish(nameof(EventCenterUserControl));
            });
        }

        public DelegateCommand StartGameCommand { get; set; }
        public DelegateCommand NavigateToActivityEnterCommand { get; set; }
    }
}
