using NarakaBladepoint.Framework.Core.Evens;
using NarakaBladepoint.Modules.Social.UI.Email.Views;
using NarakaBladepoint.Modules.Social.UI.Friend.UI.Views;
using NarakaBladepoint.Modules.Social.UI.Setting.Views;
using NarakaBladepoint.Modules.Tutorial.UI.Views;

namespace NarakaBladepoint.Modules.Social.UI.Social.ViewModels
{
    public class SocialPageViewModel : ViewModelBase
    {
        public SocialPageViewModel() { }

        private DelegateCommand _navigateToMusicCommand;

        public DelegateCommand NavigateToMusicCommand =>
            _navigateToMusicCommand ??= new DelegateCommand(() =>
            {
                eventAggregator.GetEvent<LoadHomePageRegionEvent>().Publish(new NavigationArgs(nameof(TutorialPage)));
            });

        private DelegateCommand _navigateToTutorialCommand;

        public DelegateCommand NavigateToTutorialCommand =>
            _navigateToTutorialCommand ??= new DelegateCommand(() =>
            {
                eventAggregator.GetEvent<LoadHomePageRegionEvent>().Publish(new NavigationArgs(nameof(TutorialPage)));
            });

        private DelegateCommand _navigateToEmailCommand;

        public DelegateCommand NavigateToEmailCommand =>
            _navigateToEmailCommand ??= new DelegateCommand(() =>
            {
                eventAggregator.GetEvent<LoadHomePageRegionEvent>().Publish(new NavigationArgs(nameof(EmailPage)));
            });

        private DelegateCommand _navigateToSettingCommand;

        public DelegateCommand NavigateToSettingCommand =>
            _navigateToSettingCommand ??= new DelegateCommand(() =>
            {
                eventAggregator.GetEvent<LoadHomePageRegionEvent>().Publish(new NavigationArgs(nameof(SettingPage)));
            });

        private DelegateCommand _navigateToFrendListCommand;

        public DelegateCommand NavigateToFrendListCommand =>
            _navigateToFrendListCommand ??= new DelegateCommand(() =>
            {
                eventAggregator.GetEvent<LoadHomePageRegionEvent>().Publish(new NavigationArgs(nameof(FriendPage)));
            });
    }
}
