using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nakara_WPF.Modules.CommonFunction.ViewModels;
using Nakara_WPF.Modules.CommonFunction.Views;

namespace Nakara_WPF.Modules.CommonFunction
{
    class CommonFunctionModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider) { }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<CommonFunctionUserControl>();
            containerRegistry.Register<CommonFunctionUserControlViewModel>();
        }
    }
}
