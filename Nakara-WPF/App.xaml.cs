using System.Windows;
using Nakara_WPF.Modules.CommonFunction;
using Nakara_WPF.Modules.PersonalInformation;
using Nakara_WPF.Modules.Social;
using Nakara_WPF.Modules.Wealth;

namespace Nakara_WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<MainWindow>();
            containerRegistry.Register<MainWindowViewModel>();
        }

        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            var catalog = new ModuleCatalog();
            catalog.AddModule<CommonFunctionModule>();
            catalog.AddModule<WealthModule>();
            catalog.AddModule<SocialModule>();
            catalog.AddModule<PersonalInformationModule>();
            return catalog;
        }
    }
}
