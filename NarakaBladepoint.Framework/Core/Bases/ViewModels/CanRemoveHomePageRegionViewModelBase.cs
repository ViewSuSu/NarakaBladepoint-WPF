using NarakaBladepoint.Framework.Core.Evens;

namespace NarakaBladepoint.Framework.Core.Bases.ViewModels
{
    /// <summary>
    /// 可以被返回到主窗口的页面ViewModel基类
    /// </summary>
    public abstract class CanRemoveHomePageRegionViewModelBase : ViewModelBase
    {
        protected CanRemoveHomePageRegionViewModelBase(IContainerExtension containerExtension)
            : base(containerExtension)
        {
            ReturnCommand = new DelegateCommand(() =>
            {
                this.eventAggregator.GetEvent<RemoveHomePageRegionEvent>().Publish();
            });
        }

        public DelegateCommand ReturnCommand { get; }
    }
}
