namespace Nakara.Framework.Core.Attrbuites
{
    /// <summary>
    /// 模块初始化模式特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class ModuleInitializationModeAttribute : Attribute
    {
        /// <summary>
        /// 初始化模块初始化模式特性
        /// </summary>
        /// <param name="initializationMode">初始化模式</param>
        public ModuleInitializationModeAttribute(
            InitializationMode initializationMode = InitializationMode.WhenAvailable
        )
        {
            InitializationMode = initializationMode;
        }

        /// <summary>
        /// 初始化模式
        /// </summary>
        public InitializationMode InitializationMode { get; }
    }
}
