namespace Nakara.Modules.TopUp.UI.ViewModels
{
    internal class TopUpUserControlViewModel
    {
        private readonly IEventAggregator eventAggregator;

        public TopUpUserControlViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
            ReturnCommand = new DelegateCommand(() =>
            {
                this.eventAggregator.GetEvent<RemoveHomePageRegionEvent>().Publish();
            });
        }

        public DelegateCommand ReturnCommand { get; set; }
    }
}
