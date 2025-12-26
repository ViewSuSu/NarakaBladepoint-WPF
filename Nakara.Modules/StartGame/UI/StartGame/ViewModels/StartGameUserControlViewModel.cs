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
                this.eventAggregator.GetEvent<OpenModeSelectionEvent>().Publish();
            });
        }

        public DelegateCommand StartGameCommand { get; set; }
    }
}
