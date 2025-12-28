using Nakara.Modules.Tutorial.UI.Views;

namespace Nakara.Modules.Social.UI.Setting.ViewModels
{
    internal class SettingUserControlViewModel
    {
        private readonly IEventAggregator eventAggregator;

        public SettingUserControlViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
            BackToGameCommand = new DelegateCommand(() =>
            {
                this.eventAggregator.GetEvent<RemoveHomePageRegionEvent>().Publish();
            });
            NavigateToTutorialCommand = new DelegateCommand(() =>
            {
                this.eventAggregator.GetEvent<LoadHomePageRegionEvent>()
                    .Publish(nameof(TutorialUserControl));
            });
        }

        public DelegateCommand BackToGameCommand { get; set; }
        public DelegateCommand NavigateToTutorialCommand { get; set; }
    }
}
