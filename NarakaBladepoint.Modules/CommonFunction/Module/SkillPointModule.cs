using NarakaBladepoint.Modules.CommonFunction.UI.SkillPoint.ViewModels;
using NarakaBladepoint.Modules.CommonFunction.UI.SkillPoint.Views;

namespace NarakaBladepoint.Modules.CommonFunction.Module
{
    internal class SkillPointModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider) { }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<SkillPointPage, SkillPointPageViewModel>();
        }
    }
}
