using System.IO;
using System.Reflection;
using Newtonsoft.Json;

namespace NarakaBladepoint.Shared
{
    /// <summary>
    /// 配置读取
    /// </summary>
    public static class ConfigurationDataReader
    {
        private static readonly Dictionary<Type, object> _configurations =
            new Dictionary<Type, object>();

        private static readonly string _jsonsFolderPath;

        static ConfigurationDataReader()
        {
            // 获取程序集所在路径
            var assemblyLocation = Assembly.GetExecutingAssembly().Location;
            var basePath = Path.GetDirectoryName(assemblyLocation);

            // 构建JSON文件夹路径
            _jsonsFolderPath = Path.Combine(basePath, "Datas", "Jsons");
        }

        /// <summary>
        /// 根据类型获取配置
        /// </summary>
        /// <typeparam name="T">配置类型</typeparam>
        /// <returns>配置实例</returns>
        public static T Get<T>()
            where T : class
        {
            var type = typeof(T);

            if (_configurations.TryGetValue(type, out var cachedConfig))
            {
                return (T)cachedConfig;
            }

            // 查找对应的JSON文件
            var jsonFilePath = Path.Combine(_jsonsFolderPath, $"{type.Name}.json");

            if (!File.Exists(jsonFilePath))
            {
                throw new FileNotFoundException($"找不到配置文件: {jsonFilePath}");
            }

            // 读取并反序列化JSON
            var jsonContent = File.ReadAllText(jsonFilePath);
            var config = JsonConvert.DeserializeObject<T>(jsonContent);

            if (config == null)
            {
                throw new InvalidOperationException($"无法反序列化类型 {type.Name} 的配置");
            }

            _configurations[type] = config;
            return config;
        }
    }
}
