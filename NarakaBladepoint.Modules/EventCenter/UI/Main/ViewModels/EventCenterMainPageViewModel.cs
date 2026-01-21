using NarakaBladepoint.Modules.EventCenter.Domain.EventParameters;
using NarakaBladepoint.Modules.EventCenter.Domain.Events;

namespace NarakaBladepoint.Modules.EventCenter.UI.Main.ViewModels
{
    internal class EventCenterMainPageViewModel : CanRemoveHomePageRegionViewModelBase
    {
        private bool _isSelectedLimitedEvent = false;
        public bool IsSelectedLimitedEvent
        {
            get { return _isSelectedLimitedEvent; }
            set
            {
                _isSelectedLimitedEvent = value;
                RaisePropertyChanged();
            }
        }

        public EventCenterMainPageViewModel() { }

        protected override void OnNavigatedToExecute(NavigationContext navigationContext)
        {
            if (
                navigationContext.Parameters.TryGetValue(
                    nameof(NavigatedToEventCenterParameter.IsSelectedLimitedEvent),
                    out bool isSelectedLimitedEventParameter
                )
            )
            {
                this.IsSelectedLimitedEvent = isSelectedLimitedEventParameter;
            }
        }
    }
}
