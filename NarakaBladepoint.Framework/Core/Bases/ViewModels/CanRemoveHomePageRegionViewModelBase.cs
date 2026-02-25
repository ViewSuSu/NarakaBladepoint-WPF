using NarakaBladepoint.Framework.Core.Evens;
using Prism.Commands;

namespace NarakaBladepoint.Framework.Core.Bases.ViewModels
{
    /// <summary>
    /// 可以被返回到主窗口的页面ViewModel基类
    /// </summary>
    public abstract class CanRemoveHomePageRegionViewModelBase : ViewModelBase
    {
        private DelegateCommand _returnCommand;
        private DelegateCommand _removeAllHomePageCommand;

        protected CanRemoveHomePageRegionViewModelBase() { }

        public DelegateCommand ReturnCommand
        {
            get
            {
                if (_returnCommand == null)
                {
                    _returnCommand = new DelegateCommand(() =>
                    {
                        this.eventAggregator.GetEvent<RemoveHomePageRegionEvent>().Publish();
                    });
                }
                return _returnCommand;
            }
        }

        public DelegateCommand RemoveAllHomePageCommand
        {
            get
            {
                if (_removeAllHomePageCommand == null)
                {
                    _removeAllHomePageCommand = new DelegateCommand(() =>
                    {
                        this.eventAggregator.GetEvent<RemoveAllHomePageRegionEvent>().Publish();
                    });
                }
                return _removeAllHomePageCommand;
            }
        }
    }
}
