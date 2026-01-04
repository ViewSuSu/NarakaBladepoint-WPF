namespace NarakaBladepoint.Framework.Core.Attrbuites
{
    /// <summary>
    /// 组件注册生命周期
    /// </summary>
    public enum ComponentLifetime
    {
        /// <summary>
        /// 瞬时模式（每次请求都创建新实例）
        /// </summary>
        Transient,

        /// <summary>
        /// 单例模式（整个应用程序生命周期内只有一个实例）
        /// </summary>
        Singleton,

        /// <summary>
        /// 作用域模式（同一作用域内保持单例）
        /// </summary>
        Scoped,
    }

    /// <summary>
    /// 组件注册特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class ComponentAttribute : Attribute
    {
        /// <summary>
        /// 初始化组件特性
        /// </summary>
        /// <param name="lifetime">生命周期</param>
        /// <param name="registerSelf">是否注册自身类型</param>
        public ComponentAttribute(
            ComponentLifetime lifetime = ComponentLifetime.Transient,
            bool registerSelf = false
        )
        {
            Lifetime = lifetime;
            RegisterSelf = registerSelf;
        }

        /// <summary>
        /// 组件生命周期
        /// </summary>
        public ComponentLifetime Lifetime { get; }

        /// <summary>
        /// 是否注册自身类型
        /// </summary>
        public bool RegisterSelf { get; }
    }
}
