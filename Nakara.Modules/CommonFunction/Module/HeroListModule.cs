using Nakara.Modules.CommonFunction.UI.Hero.ViewModels;
using Nakara.Modules.CommonFunction.UI.Hero.Views;

namespace Nakara.Modules.CommonFunction.Module
{
    internal class HeroListModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider) { }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<HeroListPage, HeroListPageViewModel>();
        }
    }
}
