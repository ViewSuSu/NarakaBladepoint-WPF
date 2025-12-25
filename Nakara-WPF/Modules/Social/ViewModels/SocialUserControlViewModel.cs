using Nakara_WPF.Core.Evens;

namespace Nakara_WPF.Modules.Social.ViewModels
{
    public class SocialUserControlViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;

        public SocialUserControlViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            OpenFrendListCommand = new DelegateCommand(OnOpenFriendList);
        }

        public DelegateCommand OpenFrendListCommand { get; }

        private void OnOpenFriendList()
        {
            _eventAggregator.GetEvent<OpenFriendPanelEvent>().Publish();
        }
    }
}
