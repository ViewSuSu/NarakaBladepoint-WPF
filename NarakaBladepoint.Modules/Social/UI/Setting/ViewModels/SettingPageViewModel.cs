using NarakaBladepoint.Framework.Core.Evens;
using NarakaBladepoint.Modules.Tutorial.UI.Views;

namespace NarakaBladepoint.Modules.Social.UI.Setting.ViewModels
{
    internal class SettingPageViewModel : CanRemoveHomePageRegionViewModelBase
    {
        public SettingPageViewModel() { }

        private DelegateCommand _navigateToTutorialCommand;

        public DelegateCommand NavigateToTutorialCommand =>
            _navigateToTutorialCommand ??= new DelegateCommand(() =>
            {
                RemoveAllHomePageCommand.Execute();
                eventAggregator.GetEvent<LoadHomePageRegionEvent>().Publish(new NavigationArgs(nameof(TutorialPage)));
            });

        private DelegateCommand _exitCommand;

        public DelegateCommand ExitCommand =>
            _exitCommand ??= new DelegateCommand(() =>
            {
                System.Windows.Application.Current.Shutdown();
            });
    }
}
