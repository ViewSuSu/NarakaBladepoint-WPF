using Nakara.Framework.Core.Evens;
using Nakara.Shared.Consts;

namespace Nakara.App.Shell
{
    public partial class MainWindowViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IRegionManager _regionManager;

        public MainWindowViewModel(IEventAggregator eventAggregator, IRegionManager regionManager)
        {
            _eventAggregator = eventAggregator;
            _regionManager = regionManager;

            _eventAggregator
                .GetEvent<LoadHomePageRegionEvent>()
                .Subscribe(
                    (viewName) =>
                    {
                        RevemoveRegionByName(GlobalConstant.HomePageRegion);
                        _regionManager.RequestNavigate(GlobalConstant.HomePageRegion, viewName);
                    },
                    ThreadOption.UIThread
                );

            _eventAggregator
                .GetEvent<RemoveHomePageRegionEvent>()
                .Subscribe(
                    () =>
                    {
                        RevemoveRegionByName(GlobalConstant.HomePageRegion);
                    },
                    ThreadOption.UIThread
                );

            _eventAggregator
                .GetEvent<LoadMainContentRegionEvent>()
                .Subscribe(
                    (viewName) =>
                    {
                        RevemoveRegionByName(GlobalConstant.MainContentRegion);
                        _regionManager.RequestNavigate(GlobalConstant.MainContentRegion, viewName);
                    },
                    ThreadOption.UIThread
                );

            _eventAggregator
                .GetEvent<RemoveMainContentRegionEvent>()
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
            var region = _regionManager.Regions[regionName];
            region.RemoveAll();
        }
    }
}
