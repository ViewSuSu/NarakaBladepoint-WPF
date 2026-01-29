using NarakaBladepoint.Modules.EventCenter.Domain.EventParameters;
using NarakaBladepoint.Modules.EventCenter.UI.Main.Views;

namespace NarakaBladepoint.Modules.DayEvent.UI.ViewModels
{
    internal class DayEventPageViewModel : ViewModelBase
    {
        public DayEventPageViewModel() { }

        private DelegateCommand _taskDetailsComamnd;

        public DelegateCommand TaskDetailsComamnd =>
            _taskDetailsComamnd ??= new DelegateCommand(() =>
            {
                eventAggregator
                    .GetEvent<LoadHomePageRegionEvent>()
                    .Publish(
                        new NavigationArgs(
                            nameof(EventCenterMainPage),
                            new NavigatedToEventCenterParameter(true)
                        )
                    );
            });
    }
}
