using System;
using System.Linq;
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

        public HomePageVisualNavigator() { }

        private readonly IRegionManager _regionManager;

        private readonly string[] _layers =
        {
            GlobalConstant.HomePageRegion1,
            GlobalConstant.HomePageRegion2,
            GlobalConstant.HomePageRegion3,
            GlobalConstant.HomePageRegion4,
        };

        /// <summary>
        /// 是否有活跃的 Region
        ///
        /// 用途：检查是否存在至少一个 Region 包含活跃的视图
        /// 常用于判断是否有界面层打开（如弹窗、详情页等）
        /// </summary>
        public bool HasActiveRegion
        {
            get { return _layers.Any(layer => _regionManager.Regions[layer].ActiveViews.Any()); }
        }

        /// <summary>
        /// 添加视图到最顶层
        /// </summary>
        public void RequestNavigate(string viewName, NavigationParameters nacigationParameters)
        {
            var emptyLayer = _layers.FirstOrDefault(layer =>
                !_regionManager.Regions[layer].ActiveViews.Any()
            );

            if (emptyLayer != null)
            {
                if (nacigationParameters != default)
                    _regionManager.RequestNavigate(emptyLayer, viewName, nacigationParameters);
                else
                    _regionManager.RequestNavigate(emptyLayer, viewName);
            }
            else
            {
                throw new InvalidOperationException("没有可用的导航位置，所有层都已满");
            }
        }

        /// <summary>
        /// 移除顶层视图
        /// </summary>
        public void RemoveTop()
        {
            for (int i = _layers.Length - 1; i >= 0; i--)
            {
                IRegion region = _regionManager.Regions[_layers[i]];
                if (region.ActiveViews.Any())
                {
                    region.RemoveAll();

                    if (i > 0)
                    {
                        for (int j = i - 1; j >= 0; j--)
                        {
                            var lowerRegion = _regionManager.Regions[_layers[j]];
                            var view = lowerRegion.Views.FirstOrDefault();
                            if (view != null)
                            {
                                _regionManager.RequestNavigate(_layers[j], view.GetType().Name);
                                break;
                            }
                        }
                    }
                    break;
                }
            }
        }

        /// <summary>
        /// 移除所有视图
        /// </summary>
        public void RemoveAll()
        {
            foreach (var layer in _layers)
            {
                IRegion region = _regionManager.Regions[layer];
                if (region.Views.Any())
                {
                    region.RemoveAll();
                }
            }
        }
    }
}
