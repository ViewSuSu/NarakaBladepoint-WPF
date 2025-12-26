using System.Reflection;
using Nakara.Framework.Core.Extensions;

namespace Nakara.Modules
{
    public static class ModuleCatalogConfigManager
    {
        private static readonly Assembly assembly;

        static ModuleCatalogConfigManager()
        {
            assembly = typeof(ModuleCatalogConfigManager).Assembly;
        }

        /// <summary>
        /// 自动注册所有模块
        /// </summary>
        /// <returns></returns>
        public static ModuleCatalog ConfigAll()
        {
            return assembly.CreateModuleCatalogFromAssembly();
        }
    }
}
