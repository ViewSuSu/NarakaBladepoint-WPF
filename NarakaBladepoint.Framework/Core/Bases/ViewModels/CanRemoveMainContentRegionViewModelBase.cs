using NarakaBladepoint.Framework.Core.Evens;

namespace NarakaBladepoint.Framework.Core.Bases.ViewModels
{
    public abstract class CanRemoveMainContentRegionViewModelBase : ViewModelBase
    {
        private DelegateCommand _returnCommand;

        protected CanRemoveMainContentRegionViewModelBase() { }

        public DelegateCommand ReturnCommand
        {
            get
            {
                if (_returnCommand == null)
                {
                    _returnCommand = new DelegateCommand(() =>
                    {
                        this.eventAggregator.GetEvent<RemoveMainContentRegionEvent>().Publish();
                    });
                }
                return _returnCommand;
            }
        }
    }
}
