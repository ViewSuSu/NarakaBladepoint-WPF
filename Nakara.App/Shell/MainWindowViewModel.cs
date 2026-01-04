using Nakara.Framework.Core.Bases.ViewModels;
using Nakara.Framework.Core.Evens;
using Nakara.Shared.Consts;

namespace Nakara.App.Shell
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel(IContainerExtension containerExtension)
            : base(containerExtension)
        {
            this.eventAggregator.GetEvent<LoadHomePageRegionEvent>()
                .Subscribe(
                    (viewName) =>
                    {
                        this.regionManager.RequestNavigate(
                            GlobalConstant.HomePageRegion1,
                            viewName
                        );
                    },
                    ThreadOption.UIThread
                );

            this.eventAggregator.GetEvent<RemoveHomePageRegionEvent>()
                .Subscribe(
                    () =>
                    {
                        RevemoveRegionByName(GlobalConstant.HomePageRegion1);
                    },
                    ThreadOption.UIThread
                );

            this.eventAggregator.GetEvent<LoadHomePageRegionEvent2>()
                .Subscribe(
                    (viewName) =>
                    {
                        this.regionManager.RequestNavigate(
                            GlobalConstant.HomePageRegion2,
                            viewName
                        );
                    },
                    ThreadOption.UIThread
                );

            this.eventAggregator.GetEvent<RemoveHomePageRegionEvent2>()
                .Subscribe(
                    () =>
                    {
                        RevemoveRegionByName(GlobalConstant.HomePageRegion2);
                    },
                    ThreadOption.UIThread
                );

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
