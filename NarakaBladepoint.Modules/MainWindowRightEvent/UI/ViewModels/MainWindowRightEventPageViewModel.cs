namespace NarakaBladepoint.Modules.MainWindowRightEvent.UI.ViewModels
{
    internal class MainWindowRightEventPageViewModel : ViewModelBase
    {
        public MainWindowRightEventPageViewModel(IContainerProvider containerProvider)
            : base(containerProvider) { }

        private DelegateCommand _eventOneCommand;

        public DelegateCommand EventOneCommand =>
            _eventOneCommand ??= new DelegateCommand(() => { });

        private DelegateCommand _eventFourCommand;

        public DelegateCommand EventFourCommand =>
            _eventFourCommand ??= new DelegateCommand(() => { });

        private DelegateCommand _eventEightCommand;

        public DelegateCommand EventEightCommand =>
            _eventEightCommand ??= new DelegateCommand(() => { });
    }
}
