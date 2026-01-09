namespace NarakaBladepoint.Modules.DayEvent.UI.ViewModels
{
    internal class DayEventUserControlViewModel : ViewModelBase
    {
        public DayEventUserControlViewModel(IContainerProvider containerProvider)
            : base(containerProvider)
        {
        }

        private DelegateCommand _taskDetailsComamnd;
        public DelegateCommand TaskDetailsComamnd =>
            _taskDetailsComamnd ??= new DelegateCommand(() => { });
    }
}
