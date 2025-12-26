using System.Windows;
using Nakara.App.Shell;
using Nakara.Framework;
using Nakara.Modules;

namespace Nakara.App
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
            containerRegistry.RegisterCoreServices();
        }

        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            return ModuleCatalogConfigManager.ConfigAll();
        }
    }
}
