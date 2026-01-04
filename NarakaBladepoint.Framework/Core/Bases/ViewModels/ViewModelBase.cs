namespace NarakaBladepoint.Framework.Core.Bases.ViewModels
{
    /// <summary>
    /// ViewModel基类
    /// </summary>
    public abstract class ViewModelBase : BindableBase
    {
        protected readonly IEventAggregator eventAggregator;
        protected readonly IRegionManager regionManager;

        protected ViewModelBase(IContainerExtension containerExtension)
        {
            this.eventAggregator = containerExtension.Resolve<IEventAggregator>();
            this.regionManager = containerExtension.Resolve<IRegionManager>();
        }
    }
}
