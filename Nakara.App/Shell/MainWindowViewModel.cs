using Nakara.App.Shell.Infrastructure;
using Nakara.Framework.Core.Bases.ViewModels;
using Nakara.Framework.Core.Evens;
using Nakara.Shared.Consts;

namespace Nakara.App.Shell
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        private readonly HomePageVisualNavigator homePageVisualNavigator;

        public MainWindowViewModel(
            IContainerExtension containerExtension,
            HomePageVisualNavigator homePageVisualNavigator
        )
            : base(containerExtension)
        {
            this.homePageVisualNavigator = homePageVisualNavigator;
            this.eventAggregator.GetEvent<LoadHomePageRegionEvent>()
                .Subscribe(
                    viewName => this.homePageVisualNavigator.RequestNavigate(viewName),
                    ThreadOption.UIThread
                );

            this.eventAggregator.GetEvent<RemoveHomePageRegionEvent>()
                .Subscribe(() => this.homePageVisualNavigator.RemoveTop(), ThreadOption.UIThread);

            this.eventAggregator.GetEvent<LoadMainContentRegionEvent>()
                .Subscribe(
                    (viewName) =>
                    {
                        this.regionManager.RequestNavigate(
                            GlobalConstant.MainContentRegion,
                            viewName
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

            this.eventAggregator.GetEvent<LoadRightSidePanelRegionEvent>()
                .Subscribe(
                    (viewName) =>
                    {
                        this.regionManager.RequestNavigate(
                            GlobalConstant.RightSidePanelRegion,
                            viewName
                        );
                    },
                    ThreadOption.UIThread
                );

            this.eventAggregator.GetEvent<RemoveRightSidePanelRegionEvent>()
                .Subscribe(
                    () =>
                    {
                        RevemoveRegionByName(GlobalConstant.RightSidePanelRegion);
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
