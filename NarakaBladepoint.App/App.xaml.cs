using System.Windows;
using NarakaBladepoint.App.Shell;
using NarakaBladepoint.Modules;
using NarakaBladepoint.Shared.Services;

namespace NarakaBladepoint.App
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Framework.Core.Bases.PrismApplicationBase
    {
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<MainWindow>();
            containerRegistry.Register<MainWindowViewModel>();
            containerRegistry.RegisterAppLayer();
            containerRegistry.RegisterSharedLayer();
            containerRegistry.RegisterModuleLayer();
        }

        protected override Window CreateShellExecute()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            return ModuleCatalogConfigManager.ConfigAll();
        }
    }
}
