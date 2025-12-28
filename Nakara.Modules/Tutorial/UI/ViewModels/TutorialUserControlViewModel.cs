namespace Nakara.Modules.Tutorial.UI.ViewModels
{
    internal class TutorialUserControlViewModel
    {
        private readonly IEventAggregator eventAggregator;

        public TutorialUserControlViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
            ReturnCommand = new DelegateCommand(() =>
            {
                eventAggregator.GetEvent<RemoveHomePageRegionEvent>().Publish();
            });
        }

        public DelegateCommand ReturnCommand { get; set; }
    }
}
