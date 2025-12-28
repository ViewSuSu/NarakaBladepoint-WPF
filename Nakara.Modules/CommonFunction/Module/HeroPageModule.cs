using Nakara.Modules.CommonFunction.UI.HeroPage.ViewModels;
using Nakara.Modules.CommonFunction.UI.HeroPage.Views;

namespace Nakara.Modules.CommonFunction.Module
{
    internal class HeroPageModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider) { }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<
                HeroPage,
                HeroPageViewModel
            >();
        }
    }
}
