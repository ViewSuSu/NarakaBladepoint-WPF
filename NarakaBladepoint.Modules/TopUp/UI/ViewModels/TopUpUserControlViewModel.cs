namespace NarakaBladepoint.Modules.TopUp.UI.ViewModels
{
    internal class TopUpUserControlViewModel
    {
        private readonly IEventAggregator eventAggregator;

        private DelegateCommand _returnCommand;
        public DelegateCommand ReturnCommand =>
            _returnCommand ??= new DelegateCommand(() =>
            {
                this.eventAggregator.GetEvent<RemoveHomePageRegionEvent>().Publish();
            });

        public TopUpUserControlViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
        }
    }
}
