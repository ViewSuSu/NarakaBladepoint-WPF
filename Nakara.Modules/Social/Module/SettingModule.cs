using Nakara.Modules.Social.UI.Setting.ViewModels;
using Nakara.Modules.Social.UI.Setting.Views;

namespace Nakara.Modules.Social.Module
{
    internal class SettingModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider) { }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<SettingUserControl>();
            containerRegistry.Register<SettingUserControlViewModel>();
        }
    }
}
