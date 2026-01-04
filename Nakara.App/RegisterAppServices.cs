using System.Reflection;
using Nakara.Framework.Core.Extensions;

namespace Nakara.App
{
    internal static class RegisterAppServices
    {
        private static readonly Assembly assembly;

        static RegisterAppServices()
        {
            assembly = typeof(RegisterAppServices).Assembly;
        }

        /// <summary>
        /// 注册App层的服务
        /// </summary>
        /// <param name="containerRegistry"></param>
        public static IContainerRegistry RegisterAppLayer(this IContainerRegistry containerRegistry)
        {
            return containerRegistry.RegisterrComponentsByAssembly(assembly);
        }
    }
}
