namespace NarakaBladepoint.Framework.Core.Bases.ViewModels
{
    /// <summary>
    /// ViewModel基类
    /// </summary>
    public abstract class ViewModelBase : BindableBase, INavigationAware
    {
        protected readonly IEventAggregator eventAggregator;
        protected readonly IRegionManager regionManager;
        protected readonly IContainerProvider containerProvider;

        protected ViewModelBase()
        {
            this.containerProvider = PrismApplicationBase.ContainerProvider;
            this.eventAggregator = this.containerProvider.Resolve<IEventAggregator>();
            this.regionManager = this.containerProvider.Resolve<IRegionManager>();
        }

        protected virtual bool IsNavigationTargetExecute(NavigationContext navigationContext)
        {
            return true;
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return IsNavigationTargetExecute(navigationContext);
        }

        protected virtual void OnNavigatedFromExecute(NavigationContext navigationContext) { }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            OnNavigatedFromExecute(navigationContext);
        }

        protected virtual void OnNavigatedToExecute(NavigationContext navigationContext) { }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            OnNavigatedToExecute(navigationContext);
        }
    }
}
