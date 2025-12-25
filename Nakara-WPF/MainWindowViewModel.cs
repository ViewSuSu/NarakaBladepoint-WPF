using Nakara_WPF.Core.Consts;
using Nakara_WPF.Core.Evens;
using Nakara_WPF.Modules.Social.FriendList.Views;
using Prism.Events;
using Prism.Mvvm;

namespace Nakara_WPF
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
            _eventAggregator.GetEvent<OpenFriendPanelEvent>().Subscribe(OnShowSidePanel);

            // 订阅关闭好友面板事件
            _eventAggregator.GetEvent<CloseFriendPanelEvent>().Subscribe(OnCloseSidePanel);
        }

        private void OnShowSidePanel()
        {
            _regionManager.RequestNavigate(
                GlobalConstant.SidePanelRegion,
                nameof(FriendListUserControl)
            );
        }

        private void OnCloseSidePanel()
        {
            var region = _regionManager.Regions[GlobalConstant.SidePanelRegion];
            region.RemoveAll();
        }
    }
}
