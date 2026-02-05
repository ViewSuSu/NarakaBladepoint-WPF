using NarakaBladepoint.Modules.CommonFunction.UI.Weapon.ViewModels;
using NarakaBladepoint.Modules.CommonFunction.UI.Weapon.Views;

namespace NarakaBladepoint.Modules.CommonFunction.Module
{
    /// <summary>
    /// WeaponPage模块，注册武器相关页面和ViewModel
    /// 包括：武器页面等
    /// </summary>
    internal class WeaponPageModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider) { }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // 注册武器页
            containerRegistry.RegisterForNavigation<WeaponPage, WeaponPageViewModel>();
        }
    }
}
