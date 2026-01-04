using System.Reflection;
using NarakaBladepoint.Framework.Core.Attrbuites;

namespace NarakaBladepoint.Modules
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
            return CreateModuleCatalogFromAssembly(assembly);
        }

        /// <summary>
        /// 获取模块的初始化模式
        /// </summary>
        /// <param name="moduleType">模块类型</param>
        /// <returns>初始化模式，如果没有指定特性则返回默认值</returns>
        private static InitializationMode GetModuleInitializationMode(Type moduleType)
        {
            if (moduleType == null)
                throw new ArgumentNullException(nameof(moduleType));

            // 检查是否应用了 ModuleInitializationModeAttribute 特性
            var attribute =
                moduleType
                    .GetCustomAttributes(typeof(ModuleInitializationModeAttribute), false)
                    .FirstOrDefault() as ModuleInitializationModeAttribute;

            // 如果没有指定特性，返回默认值
            return attribute?.InitializationMode ?? InitializationMode.WhenAvailable;
        }

        /// <summary>
        /// 从Assembly中获取所有实现IModule的类型
        /// </summary>
        /// <param name="assembly">目标程序集</param>
        /// <returns>IModule类型列表</returns>
        private static IEnumerable<Type> GetModuleTypes(Assembly assembly)
        {
            if (assembly == null)
                throw new ArgumentNullException(nameof(assembly));

            return assembly
                .GetTypes()
                .Where(t => typeof(IModule).IsAssignableFrom(t) && !t.IsAbstract && !t.IsInterface);
        }

        /// <summary>
        /// 从Assembly中创建模块目录
        /// </summary>
        /// <param name="assembly">目标程序集</param>
        /// <returns>配置好的ModuleCatalog</returns>
        private static ModuleCatalog CreateModuleCatalogFromAssembly(Assembly assembly)
        {
            if (assembly == null)
                throw new ArgumentNullException(nameof(assembly));
            var catalog = new ModuleCatalog();
            var moduleTypes = GetModuleTypes(assembly);
            foreach (var moduleType in moduleTypes)
            {
                var initializationMode = GetModuleInitializationMode(moduleType);
                catalog.AddModule(moduleType, initializationMode);
            }
            return catalog;
        }
    }
}
