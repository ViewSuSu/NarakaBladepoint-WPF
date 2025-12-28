using Nakara.Modules.Social.UI.Email.Views;
using Nakara.Modules.Social.UI.FriendList.Views;
using Nakara.Modules.Social.UI.Setting.Views;
using Nakara.Modules.Tutorial.UI.Views;

namespace Nakara.Modules.Social.UI.Social.ViewModels
{
    public class SocialUserControlViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;

        public SocialUserControlViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            NavigateToMusicCommand = new DelegateCommand(() =>
            {
                _eventAggregator
                    .GetEvent<LoadHomePageRegionEvent>()
                    .Publish(nameof(FriendListUserControl));
            });
            NavigateToTutorialCommand = new DelegateCommand(() =>
            {
                _eventAggregator
                    .GetEvent<LoadHomePageRegionEvent>()
                    .Publish(nameof(TutorialUserControl));
            });
            NavigateToEmailCommand = new DelegateCommand(() =>
            {
                _eventAggregator
                    .GetEvent<LoadHomePageRegionEvent>()
                    .Publish(nameof(EmailUserControl));
            });
            NavigateToFrendListCommand = new DelegateCommand(() =>
            {
                _eventAggregator
                    .GetEvent<LoadSidePanelRegionEvent>()
                    .Publish(nameof(FriendListUserControl));
            });
            NavigateToSettingCommand = new DelegateCommand(() =>
            {
                _eventAggregator
                    .GetEvent<LoadHomePageRegionEvent>()
                    .Publish(nameof(SettingUserControl));
            });
        }

        public DelegateCommand NavigateToMusicCommand { get; }
        public DelegateCommand NavigateToTutorialCommand { get; }
        public DelegateCommand NavigateToEmailCommand { get; }
        public DelegateCommand NavigateToFrendListCommand { get; }
        public DelegateCommand NavigateToSettingCommand { get; }
    }
}
