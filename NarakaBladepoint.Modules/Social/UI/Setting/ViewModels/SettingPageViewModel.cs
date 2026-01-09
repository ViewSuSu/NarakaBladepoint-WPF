using NarakaBladepoint.Modules.Tutorial.UI.Views;

namespace NarakaBladepoint.Modules.Social.UI.Setting.ViewModels
{
    internal class SettingPageViewModel : CanRemoveHomePageRegionViewModelBase
    {
        public SettingPageViewModel(IContainerProvider containerProvider)
            : base(containerProvider) { }

        private DelegateCommand _navigateToTutorialCommand;

        public DelegateCommand NavigateToTutorialCommand =>
            _navigateToTutorialCommand ??= new DelegateCommand(() =>
            {
                eventAggregator.GetEvent<LoadHomePageRegionEvent>().Publish(nameof(TutorialPage));
            });

        private DelegateCommand _exitCommand;

        public DelegateCommand ExitCommand =>
            _exitCommand ??= new DelegateCommand(() =>
            {
                System.Windows.Application.Current.Shutdown();
            });
    }
}
