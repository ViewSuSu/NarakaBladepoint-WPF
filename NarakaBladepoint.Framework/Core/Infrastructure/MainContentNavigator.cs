using NarakaBladepoint.Framework.Core.Consts;

namespace NarakaBladepoint.Framework.Core.Infrastructure
{
    /// <summary>
    /// 主内容区域导航器
    ///
    /// 用途：管理 MainContentRegion 的导航和内容切换
    /// 特点：一次只显示一个内容，新内容会替换旧内容（相比 HomePageVisualNavigator 的堆叠显示）
    ///
    /// 使用场景：
    /// - 列表页面切换
    /// - 详情页面显示
    /// - 内容区域的内容替换
    /// </summary>
    [Component(ComponentLifetime.Singleton)]
    public class MainContentNavigator
    {
        public static event EventHandler? Removed;

        public MainContentNavigator(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public MainContentNavigator() { }

        private readonly IRegionManager _regionManager;

        /// <summary>
        /// 是否有活跃的主内容
        ///
        /// 用途：检查 MainContentRegion 是否包含活跃的视图
        /// 常用于判断是否有主内容页面打开
        /// </summary>
        public bool HasActiveContent
        {
            get
            {
                return _regionManager.Regions[GlobalConstant.MainContentRegion].ActiveViews.Any();
            }
        }

        /// <summary>
        /// 导航到指定内容
        /// 新内容会替换旧内容（不是堆叠显示）
        /// </summary>
        public void RequestNavigate(
            string viewName,
            NavigationParameters navigationParameters = null
        )
        {
            if (navigationParameters != default && navigationParameters != null)
            {
                _regionManager.RequestNavigate(
                    GlobalConstant.MainContentRegion,
                    viewName,
                    navigationParameters
                );
            }
            else
            {
                _regionManager.RequestNavigate(GlobalConstant.MainContentRegion, viewName);
            }
        }

        /// <summary>
        /// 清除主内容区域的所有视图
        /// </summary>
        public void Remove()
        {
            var region = _regionManager.Regions[GlobalConstant.MainContentRegion];
            if (region.Views.Any())
            {
                region.RemoveAll();
                Removed?.Invoke(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// 获取当前活跃的内容视图名称
        /// </summary>
        public string GetActiveContentName()
        {
            var region = _regionManager.Regions[GlobalConstant.MainContentRegion];
            var activeView = region.ActiveViews.FirstOrDefault();
            return activeView?.GetType().Name ?? string.Empty;
        }
    }
}
