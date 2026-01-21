using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
