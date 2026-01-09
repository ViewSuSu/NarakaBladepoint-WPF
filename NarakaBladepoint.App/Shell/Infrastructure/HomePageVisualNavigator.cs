using NarakaBladepoint.Framework.Core.Attrbuites;
using NarakaBladepoint.Shared.Consts;

namespace NarakaBladepoint.App.Shell.Infrastructure
{
    [Component(ComponentLifetime.Singleton)]
    internal class HomePageVisualNavigator
    {
        public HomePageVisualNavigator(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        private readonly IRegionManager _regionManager;

        private readonly string[] _layers =
        {
            GlobalConstant.HomePageRegion1,
            GlobalConstant.HomePageRegion2,
            GlobalConstant.HomePageRegion3,
        };

        /// <summary>
        /// 添加视图到最顶层
        /// </summary>
        public void RequestNavigate(string viewName)
        {
            // 查找第一个空区域
            var emptyLayer = _layers.FirstOrDefault(layer =>
                !_regionManager.Regions[layer].ActiveViews.Any()
            );

            if (emptyLayer != null)
            {
                // 如果有空区域，直接使用
                _regionManager.RequestNavigate(emptyLayer, viewName);
            }
            else
            {
                // 如果没有空区域，直接报错
                throw new InvalidOperationException("没有可用的导航位置，所有层都已满");
            }
        }

        /// <summary>
        /// 移除顶层视图
        /// </summary>
        public void RemoveTop()
        {
            // 从顶层开始查找有视图的区域
            for (int i = _layers.Length - 1; i >= 0; i--)
            {
                IRegion region = _regionManager.Regions[_layers[i]];
                if (region.ActiveViews.Any())
                {
                    // 移除顶层视图
                    region.RemoveAll();

                    // 查找下面一层是否有视图需要重新激活
                    if (i > 0)
                    {
                        for (int j = i - 1; j >= 0; j--)
                        {
                            var lowerRegion = _regionManager.Regions[_layers[j]];
                            var view = lowerRegion.Views.FirstOrDefault();
                            _regionManager.RequestNavigate(_layers[j], view.GetType().Name);
                        }
                    }
                    break;
                }
            }
        }
    }
}
