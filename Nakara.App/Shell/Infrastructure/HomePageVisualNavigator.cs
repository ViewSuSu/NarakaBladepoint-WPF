using Nakara.Framework.Core.Attrbuites;
using Nakara.Shared.Consts;

namespace Nakara.App.Shell.Infrastructure
{
    [Component(ComponentLifetime.Singleton)]
    public class HomePageVisualNavigator
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
                // 如果没有空区域，移除最底层视图，整体下移
                RemoveBottomView();

                // 重新查找空区域（现在应该是最顶层）
                emptyLayer = _layers.First(layer =>
                    !_regionManager.Regions[layer].ActiveViews.Any()
                );
                _regionManager.RequestNavigate(emptyLayer, viewName);
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
                var region = _regionManager.Regions[_layers[i]];
                if (region.ActiveViews.Any())
                {
                    region.RemoveAll();
                    break;
                }
            }
        }

        /// <summary>
        /// 移除最底层视图，整体下移
        /// </summary>
        private void RemoveBottomView()
        {
            // 移除最底层
            _regionManager.Regions[_layers[0]].RemoveAll();

            // 将中层移到底层
            var middleView = _regionManager.Regions[_layers[1]].ActiveViews.FirstOrDefault();
            if (middleView != null)
            {
                var viewName = GetViewName(middleView);
                _regionManager.Regions[_layers[1]].Remove(middleView);
                _regionManager.RequestNavigate(_layers[0], viewName);
            }

            // 将顶层移到中层
            var topView = _regionManager.Regions[_layers[2]].ActiveViews.FirstOrDefault();
            if (topView != null)
            {
                var viewName = GetViewName(topView);
                _regionManager.Regions[_layers[2]].Remove(topView);
                _regionManager.RequestNavigate(_layers[1], viewName);
            }
        }

        private string GetViewName(object view)
        {
            return view.GetType().Name;
        }
    }
}
