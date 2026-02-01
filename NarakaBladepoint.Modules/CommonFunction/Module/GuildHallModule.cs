using NarakaBladepoint.Modules.CommonFunction.UI.GuildHall.ViewModels;
using NarakaBladepoint.Modules.CommonFunction.UI.GuildHall.Views;

namespace NarakaBladepoint.Modules.CommonFunction.Module
{
    internal class GuildHallModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider) { }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<GuildHallMainContentPage, GuildHallMainContentPageViewModel>();
            containerRegistry.RegisterForNavigation<GuildHallPage, GuildHallPageViewModel>();
            containerRegistry.RegisterForNavigation<GuildHallStorePage>();
            containerRegistry.RegisterForNavigation<GuildHallEventPage>();
        }
    }
}
