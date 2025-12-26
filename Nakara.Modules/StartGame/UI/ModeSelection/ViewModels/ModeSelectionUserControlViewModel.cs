namespace Nakara.Modules.StartGame.UI.ModeSelection.ViewModels
{
    internal class ModeSelectionUserControlViewModel : BindableBase
    {
        private readonly IEventAggregator eventAggregator;
        private readonly IServerInformation serverInformation;

        public ModeSelectionUserControlViewModel(
            IEventAggregator eventAggregator,
            IServerInformation serverInformation
        )
        {
            this.eventAggregator = eventAggregator;
            this.serverInformation = serverInformation;
            CloseCommand = new DelegateCommand(() =>
            {
                this.eventAggregator.GetEvent<CloseModeSelectionEvent>().Publish();
            });
        }

        public DelegateCommand CloseCommand { get; }
    }
}
