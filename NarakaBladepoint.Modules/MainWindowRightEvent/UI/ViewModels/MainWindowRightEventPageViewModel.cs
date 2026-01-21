namespace NarakaBladepoint.Modules.MainWindowRightEvent.UI.ViewModels
{
    internal class MainWindowRightEventPageViewModel : ViewModelBase
    {
        private readonly ITipMessageService tipMessageService;

        public MainWindowRightEventPageViewModel(
            ITipMessageService tipMessageService
        )
        {
            this.tipMessageService = tipMessageService;
        }

        private DelegateCommand _eventOneCommand;

        public DelegateCommand EventOneCommand =>
            _eventOneCommand ??= new DelegateCommand(async () =>
            {
                await tipMessageService.ShowTipMessageAsync(
                    new TipMessageWithHighlightArgs("开发中...")
                );
            });

        private DelegateCommand _eventTwoCommand;

        public DelegateCommand EventTwoCommand =>
            _eventTwoCommand ??= new DelegateCommand(async () =>
            {
                await tipMessageService.ShowTipMessageAsync(
                    new TipMessageWithHighlightArgs("开发中...")
                );
            });

        private DelegateCommand _eventThreeCommand;

        public DelegateCommand EventThreeCommand =>
            _eventThreeCommand ??= new DelegateCommand(async () =>
            {
                await tipMessageService.ShowTipMessageAsync(
                    new TipMessageWithHighlightArgs("开发中...")
                );
            });

        private DelegateCommand _eventFourCommand;

        public DelegateCommand EventFourCommand =>
            _eventFourCommand ??= new DelegateCommand(async () =>
            {
                await tipMessageService.ShowTipMessageAsync(
                    new TipMessageWithHighlightArgs("开发中...")
                );
            });

        private DelegateCommand _eventFiveCommand;

        public DelegateCommand EventFiveCommand =>
            _eventFiveCommand ??= new DelegateCommand(async () =>
            {
                await tipMessageService.ShowTipMessageAsync(
                    new TipMessageWithHighlightArgs("开发中...")
                );
            });

        private DelegateCommand _eventSixCommand;

        public DelegateCommand EventSixCommand =>
            _eventSixCommand ??= new DelegateCommand(async () =>
            {
                await tipMessageService.ShowTipMessageAsync(
                    new TipMessageWithHighlightArgs("开发中...")
                );
            });

        private DelegateCommand _eventSevenCommand;

        public DelegateCommand EventSevenCommand =>
            _eventSevenCommand ??= new DelegateCommand(async () =>
            {
                await tipMessageService.ShowTipMessageAsync(
                    new TipMessageWithHighlightArgs("开发中...")
                );
            });

        private DelegateCommand _eventEightCommand;

        public DelegateCommand EventEightCommand =>
            _eventEightCommand ??= new DelegateCommand(async () =>
            {
                await tipMessageService.ShowTipMessageAsync(
                    new TipMessageWithHighlightArgs("开发中...")
                );
            });
    }
}
