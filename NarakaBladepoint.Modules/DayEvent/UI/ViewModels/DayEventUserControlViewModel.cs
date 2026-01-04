namespace NarakaBladepoint.Modules.DayEvent.UI.ViewModels
{
    internal class DayEventUserControlViewModel : ViewModelBase
    {
        public DayEventUserControlViewModel(IContainerExtension containerExtension)
            : base(containerExtension)
        {
            TaskDetailsComamnd = new DelegateCommand(() => { });
        }

        public DelegateCommand TaskDetailsComamnd { get; set; }
    }
}
