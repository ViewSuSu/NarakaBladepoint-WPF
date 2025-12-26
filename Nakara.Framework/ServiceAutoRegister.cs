using System.Reflection;
using Nakara.Framework.Core.Extensions;

namespace Nakara.Framework
{
    public static class ServiceAutoRegister
    {
        private static readonly Assembly assembly;

        static ServiceAutoRegister()
        {
            assembly = typeof(ServiceAutoRegister).Assembly;
        }

        /// <summary>
        /// 注册核心层的服务
        /// </summary>
        /// <param name="containerRegistry"></param>
        public static void RegisterCoreServices(this IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterComponents(assembly);
        }
    }
}
