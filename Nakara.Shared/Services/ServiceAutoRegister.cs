using System.Reflection;
using Nakara.Framework.Core.Extensions;

namespace Nakara.Shared.Services
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
        public static IContainerRegistry RegisterSharedServices(
            this IContainerRegistry containerRegistry
        )
        {
            return containerRegistry.RegisterComponents(assembly);
        }
    }
}
