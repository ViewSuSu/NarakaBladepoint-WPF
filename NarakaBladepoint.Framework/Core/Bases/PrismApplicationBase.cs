using System.Windows;
using Prism.DryIoc;

namespace NarakaBladepoint.Framework.Core.Bases
{
    public abstract class PrismApplicationBase : PrismApplication
    {
        protected sealed override Window CreateShell()
        {
            ContainerProvider = Container;
            return CreateShellExecute();
        }

        protected abstract Window CreateShellExecute();

        internal static IContainerProvider ContainerProvider { get; private set; }
    }
}
