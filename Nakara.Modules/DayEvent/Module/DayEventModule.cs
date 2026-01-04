using Nakara.Modules.DayEvent.UI.ViewModels;
using Nakara.Modules.DayEvent.UI.Views;

namespace Nakara.Modules.DayEvent.Module
{
    internal class DayEventModuleL : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider) { }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<DayEventUserControl>();
            containerRegistry.Register<DayEventUserControlViewModel>();
        }
    }
}
