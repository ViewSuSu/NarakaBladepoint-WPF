using System.Reflection;
using NarakaBladepoint.Framework.Core.Extensions;

namespace NarakaBladepoint.Shared.Services
{
    public static class ServiceAutoRegister
    {
        private static readonly Assembly assembly;

        static ServiceAutoRegister()
        {
            assembly = typeof(ServiceAutoRegister).Assembly;
        }

        /// <summary>
        /// 注册共享层的服务
        /// </summary>
        /// <param name="containerRegistry"></param>
        public static IContainerRegistry RegisterSharedLayer(
            this IContainerRegistry containerRegistry
        )
        {
            return containerRegistry.RegisterrComponentsByAssembly(assembly);
        }
    }
}
