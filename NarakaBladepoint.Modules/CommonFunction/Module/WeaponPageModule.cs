using NarakaBladepoint.Modules.CommonFunction.UI.Weapon.ViewModels;
using NarakaBladepoint.Modules.CommonFunction.UI.Weapon.Views;

namespace NarakaBladepoint.Modules.CommonFunction.Module
{
    internal class WeaponPageModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider) { }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<WeaponPage, WeaponPageViewModel>();
        }
    }
}
