using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nakara.Modules.CommonFunction.UI.SkillPoint.ViewModels;
using Nakara.Modules.CommonFunction.UI.SkillPoint.Views;

namespace Nakara.Modules.CommonFunction.Module
{
    internal class SkillPointModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider) { }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<
                SkillPointUserControl,
                SkillPointUserControlViewModel
            >();
        }
    }
}
