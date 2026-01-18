using System.IO;
using System.Reflection;
using Newtonsoft.Json;

namespace NarakaBladepoint.Shared
{
    /// <summary>
    /// 配置读取（不缓存，每次实时读取）
    /// </summary>
    public static class ConfigurationDataReader
    {
        private static readonly string _jsonsFolderPath;

        static ConfigurationDataReader()
        {
            // 获取程序集所在路径
            var assemblyLocation = Assembly.GetExecutingAssembly().Location;
            var basePath = Path.GetDirectoryName(assemblyLocation);

            // 构建JSON文件夹路径
            _jsonsFolderPath = Path.Combine(basePath!, "Datas", "Jsons");
        }

        /// <summary>
        /// 根据类型获取配置（不缓存）
        /// </summary>
        public static T Get<T>()
            where T : class
        {
            var type = typeof(T);
            var jsonFilePath = Path.Combine(_jsonsFolderPath, $"{type.Name}.json");

            if (!File.Exists(jsonFilePath))
            {
                throw new FileNotFoundException($"找不到配置文件: {jsonFilePath}");
            }

            var jsonContent = File.ReadAllText(jsonFilePath);
            var config = JsonConvert.DeserializeObject<T>(jsonContent);

            if (config == null)
            {
                throw new InvalidOperationException($"无法反序列化类型 {type.Name} 的配置");
            }

            return config;
        }

        public static List<TItem> GetList<TItem>()
            where TItem : class
        {
            var type = typeof(TItem);
            var jsonFilePath = Path.Combine(_jsonsFolderPath, $"{type.Name}.json");
            if (!File.Exists(jsonFilePath))
            {
                throw new FileNotFoundException($"找不到配置文件: {jsonFilePath}");
            }
            var jsonContent = File.ReadAllText(jsonFilePath);
            var list = JsonConvert.DeserializeObject<List<TItem>>(jsonContent);
            if (list == null)
                {
                    throw new InvalidOperationException($"无法反序列化类型 List<{type.Name}> 的配置");
                }
                return list;
            }

            /// <summary>
            /// 根据自定义文件名获取配置列表（不缓存）
            /// </summary>
            public static List<TItem> GetList<TItem>(string jsonFileName)
                where TItem : class
            {
                var jsonFilePath = Path.Combine(_jsonsFolderPath, $"{jsonFileName}.json");
                if (!File.Exists(jsonFilePath))
                {
                    throw new FileNotFoundException($"找不到配置文件: {jsonFilePath}");
                }
                var jsonContent = File.ReadAllText(jsonFilePath);
                var list = JsonConvert.DeserializeObject<List<TItem>>(jsonContent);
                if (list == null)
                {
                    throw new InvalidOperationException($"无法反序列化类型 List<{typeof(TItem).Name}> 的配置");
                }
                return list;
            }

            /// <summary>
            /// 保存配置（覆盖本地 JSON 文件）
            /// </summary>
        /// <typeparam name="T">配置类型</typeparam>
        /// <param name="config">配置实例</param>
        /// <returns>是否保存成功</returns>
        public static bool Save<T>(T config)
        {
            if (config == null)
            {
                return false;
            }

            try
            {
                var type = typeof(T);
                var jsonFilePath = Path.Combine(_jsonsFolderPath, $"{type.Name}.json");

                // 确保目录存在
                Directory.CreateDirectory(_jsonsFolderPath);

                // 序列化（格式化，方便人工查看和编辑）
                var jsonContent = JsonConvert.SerializeObject(config, Formatting.Indented);

                // 覆盖写入
                File.WriteAllText(jsonFilePath, jsonContent);

                return true;
            }
            catch
            {
                // 可以在这里打日志
                return false;
            }
        }

        /// <summary>
        /// 保存 List 配置（覆盖本地 JSON 文件）
        /// </summary>
        /// <typeparam name="TItem">列表元素类型</typeparam>
        /// <param name="list">列表实例</param>
        /// <returns>是否保存成功</returns>
        public static bool SaveList<TItem>(IEnumerable<TItem> list)
        {
            if (list == null)
            {
                return false;
            }

            try
            {
                var type = typeof(TItem);
                var jsonFilePath = Path.Combine(_jsonsFolderPath, $"{type.Name}.json");

                // 确保目录存在
                Directory.CreateDirectory(_jsonsFolderPath);

                // 序列化（格式化，方便人工查看和编辑）
                var jsonContent = JsonConvert.SerializeObject(list, Formatting.Indented);

                // 覆盖写入
                File.WriteAllText(jsonFilePath, jsonContent);

                return true;
            }
            catch
            {
                // 可以在这里打日志
                return false;
            }
        }
    }
}
