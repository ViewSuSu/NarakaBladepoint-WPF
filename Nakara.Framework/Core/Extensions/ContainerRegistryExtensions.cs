using System.Reflection;

namespace Nakara.Framework.Core.Extensions
{
    /// <summary>
    /// 容器注册扩展方法
    /// </summary>
    public static class ContainerRegistryExtensions
    {
        /// <summary>
        /// 自动注册指定程序集中标记了 ComponentAttribute 的类型
        /// </summary>
        public static IContainerRegistry RegisterComponents(
            this IContainerRegistry registry,
            Assembly assembly
        )
        {
            if (registry == null)
                throw new ArgumentNullException(nameof(registry));
            if (assembly == null)
                throw new ArgumentNullException(nameof(assembly));

            // 获取所有标记了 ComponentAttribute 的类
            var componentTypes = assembly
                .GetTypes()
                .Where(t =>
                    t.IsClass && !t.IsAbstract && t.IsDefined(typeof(ComponentAttribute), false)
                );

            foreach (var componentType in componentTypes)
            {
                RegisterComponent(registry, componentType);
            }

            return registry;
        }

        /// <summary>
        /// 注册单个组件
        /// </summary>
        private static void RegisterComponent(IContainerRegistry registry, Type componentType)
        {
            var attribute =
                componentType
                    .GetCustomAttributes(typeof(ComponentAttribute), false)
                    .FirstOrDefault() as ComponentAttribute;

            if (attribute == null)
                return;

            // 获取组件实现的所有接口（排除系统接口）
            var interfaces = componentType
                .GetInterfaces()
                .Where(i => i != typeof(IDisposable) && i != typeof(IAsyncDisposable))
                .ToList();

            // 如果有接口，则注册所有接口
            if (interfaces.Count > 0)
            {
                foreach (var interfaceType in interfaces)
                {
                    RegisterWithLifetime(
                        registry,
                        interfaceType,
                        componentType,
                        attribute.Lifetime
                    );
                }
            }

            // 如果设置了 RegisterSelf 或没有接口，则注册自身
            if (attribute.RegisterSelf || interfaces.Count == 0)
            {
                RegisterWithLifetime(registry, componentType, componentType, attribute.Lifetime);
            }
        }

        /// <summary>
        /// 根据生命周期注册类型
        /// </summary>
        private static void RegisterWithLifetime(
            IContainerRegistry registry,
            Type fromType,
            Type toType,
            ComponentLifetime lifetime
        )
        {
            switch (lifetime)
            {
                case ComponentLifetime.Singleton:
                    registry.RegisterSingleton(fromType, toType);
                    break;

                case ComponentLifetime.Transient:
                    registry.Register(fromType, toType);
                    break;

                case ComponentLifetime.Scoped:
                    registry.RegisterScoped(fromType, toType);
                    break;

                default:
                    registry.Register(fromType, toType);
                    break;
            }
        }
    }
}
