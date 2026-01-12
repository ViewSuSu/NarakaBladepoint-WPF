using NarakaBladepoint.App.Shell.Infrastructure;
using NarakaBladepoint.Framework.Core.Bases.ViewModels;
using NarakaBladepoint.Framework.Core.Evens;
using NarakaBladepoint.Shared.Consts;

namespace NarakaBladepoint.App.Shell
{
    internal partial class MainWindowViewModel : ViewModelBase
    {
        private readonly HomePageVisualNavigator homePageVisualNavigator;

        public MainWindowViewModel(
            IContainerProvider containerProvider,
            HomePageVisualNavigator homePageVisualNavigator
        )
            : base(containerProvider)
        {
            this.homePageVisualNavigator = homePageVisualNavigator;
            this.eventAggregator.GetEvent<LoadHomePageRegionEvent>()
                .Subscribe(
                    args =>
                    {
                        this.homePageVisualNavigator.RequestNavigate(args.ViewName, args.Parameter);
                    },
                    ThreadOption.UIThread
                );

            this.eventAggregator.GetEvent<RemoveHomePageRegionEvent>()
                .Subscribe(() => this.homePageVisualNavigator.RemoveTop(), ThreadOption.UIThread);

            this.eventAggregator.GetEvent<RemoveAllHomePageRegionEvent>()
                .Subscribe(() => this.homePageVisualNavigator.RemoveAll(), ThreadOption.UIThread);

            this.eventAggregator.GetEvent<LoadMainContentRegionEvent>()
                .Subscribe(
                    (args) =>
                    {
                        if (args.Parameter != default)
                            this.regionManager.RequestNavigate(
                                GlobalConstant.MainContentRegion,
                                args.ViewName,
                                args.Parameter
                            );
                        else
                            this.regionManager.RequestNavigate(
                                GlobalConstant.MainContentRegion,
                                args.ViewName
                            );
                    },
                    ThreadOption.UIThread
                );

            this.eventAggregator.GetEvent<RemoveMainContentRegionEvent>()
                .Subscribe(
                    () =>
                    {
                        RevemoveRegionByName(GlobalConstant.MainContentRegion);
                    },
                    ThreadOption.UIThread
                );
        }

        private void RevemoveRegionByName(string regionName)
        {
            var region = regionManager.Regions[regionName];
            region.RemoveAll();
        }
    }
}
