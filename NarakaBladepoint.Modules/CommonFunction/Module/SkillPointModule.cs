using NarakaBladepoint.Modules.CommonFunction.UI.SkillPoint.ViewModels;
using NarakaBladepoint.Modules.CommonFunction.UI.SkillPoint.Views;

namespace NarakaBladepoint.Modules.CommonFunction.Module
{
    /// <summary>
    /// SkillPoint模块，注册技能点相关页面和ViewModel
    /// 包括：技能点页面等
    /// </summary>
    internal class SkillPointModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider) { }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // 注册技能点页
            containerRegistry.RegisterForNavigation<SkillPointPage, SkillPointPageViewModel>();
        }
    }
}
