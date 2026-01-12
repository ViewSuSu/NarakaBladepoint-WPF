namespace NarakaBladepoint.Modules.DayEvent.UI.ViewModels
{
    internal class DayEventPageViewModel : ViewModelBase
    {
        public DayEventPageViewModel(IContainerProvider containerProvider)
            : base(containerProvider)
        {
        }

        private DelegateCommand _taskDetailsComamnd;
        public DelegateCommand TaskDetailsComamnd =>
            _taskDetailsComamnd ??= new DelegateCommand(() => { });
    }
}
