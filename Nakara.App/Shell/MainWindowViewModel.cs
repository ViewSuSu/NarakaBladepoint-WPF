using Nakara.Modules.Social.UI.FriendList.Views;
using Nakara.Modules.StartGame.UI.ModeSelection.Views;

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

            // 订阅打开好友面板事件
            _eventAggregator
                .GetEvent<OpenFriendPanelEvent>()
                .Subscribe(
                    () =>
                    {
                        _regionManager.RequestNavigate(
                            GlobalConstant.SidePanelRegion,
                            nameof(FriendListUserControl)
                        );
                    },
                    ThreadOption.UIThread
                );

            // 订阅关闭好友面板事件
            _eventAggregator
                .GetEvent<CloseFriendPanelEvent>()
                .Subscribe(
                    () =>
                    {
                        RevemoveRegionByName(GlobalConstant.SidePanelRegion);
                    },
                    ThreadOption.UIThread
                );

            //订阅打开模式选择事件
            _eventAggregator
                .GetEvent<OpenModeSelectionEvent>()
                .Subscribe(() =>
                {
                    _regionManager.RequestNavigate(
                        GlobalConstant.HomePageRegion,
                        nameof(ModeSelectionUserControl)
                    );
                });
            //订阅关闭模式选择事件
            _eventAggregator
                .GetEvent<CloseModeSelectionEvent>()
                .Subscribe(
                    () =>
                    {
                        RevemoveRegionByName(GlobalConstant.HomePageRegion);
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
