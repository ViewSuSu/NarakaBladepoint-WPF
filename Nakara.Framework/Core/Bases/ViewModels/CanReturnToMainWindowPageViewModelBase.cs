using Nakara.Framework.Core.Evens;

namespace Nakara.Framework.Core.Bases.ViewModels
{
    /// <summary>
    /// 可以被返回到主窗口的页面ViewModel基类
    /// </summary>
    public abstract class CanReturnToMainWindowPageViewModelBase : BindableBase
    {
        protected readonly IEventAggregator eventAggregator;

        protected CanReturnToMainWindowPageViewModelBase(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
            ReturnCommand = new DelegateCommand(() =>
            {
                this.eventAggregator.GetEvent<RemoveHomePageRegionEvent>().Publish();
            });
        }

        public DelegateCommand ReturnCommand { get; }
    }
}
