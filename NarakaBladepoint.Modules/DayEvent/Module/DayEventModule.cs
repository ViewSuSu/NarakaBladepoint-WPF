using NarakaBladepoint.Modules.DayEvent.UI.ViewModels;
using NarakaBladepoint.Modules.DayEvent.UI.Views;

namespace NarakaBladepoint.Modules.DayEvent.Module
{
    internal class DayEventModuleL : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider) { }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<DayEventPage>();
            containerRegistry.Register<DayEventPageViewModel>();
        }
    }
}
