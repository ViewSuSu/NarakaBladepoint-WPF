using System.Windows;
using NarakaBladepoint.App.Shell;
using NarakaBladepoint.Modules;
using NarakaBladepoint.Shared.Services;

namespace NarakaBladepoint.App
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
            containerRegistry.RegisterAppLayer();
            containerRegistry.RegisterSharedLayer();
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
