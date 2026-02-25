using System.IO;
using System.Reflection;
using Newtonsoft.Json;

namespace NarakaBladepoint.Shared
{
    /// <summary>
    /// 配置数据读取器
    /// </summary>
    /// <remarks>
    /// 提供从JSON文件读取和保存配置数据的功能。
    /// 特点：
    /// - 不缓存数据，每次调用都实时读取和反序列化
    /// - 支持泛型配置类和列表配置
    /// - 自动处理路径计算，基于程序集运行目录
    /// - 配置文件位置：{程序集目录}/Datas/Jsons/{文件名}.json
    /// - 使用 Newtonsoft.Json 进行序列化和反序列化
    /// </remarks>
    public static class ConfigurationDataReader
    {
        /// <summary>
        /// JSON文件夹的完整路径
        /// </summary>
        private static readonly string _jsonsFolderPath;

        /// <summary>
        /// 静态构造函数，初始化JSON文件夹路径
        /// </summary>
        static ConfigurationDataReader()
        {
            // 获取当前执行程序集的物理位置
            var assemblyLocation = Assembly.GetExecutingAssembly().Location;

            // 获取程序集所在的目录
            var basePath = Path.GetDirectoryName(assemblyLocation);

            // 构建JSON配置文件夹的完整路径：{程序集目录}/Datas/Jsons
            _jsonsFolderPath = Path.Combine(basePath!, "Datas", "Jsons");
        }

        /// <summary>
        /// 根据类型获取配置（不缓存）
        /// </summary>
        /// <typeparam name="T">配置类型，类型名将用作JSON文件名</typeparam>
        /// <returns>反序列化后的配置实例</returns>
        /// <exception cref="FileNotFoundException">对应的JSON配置文件不存在时抛出</exception>
        /// <exception cref="InvalidOperationException">反序列化失败时抛出</exception>
        /// <remarks>
        /// 方法会在Datas/Jsons文件夹中查找名为 {T.Name}.json 的文件
        /// 例如：Get&lt;MatchData&gt;() 会查找 MatchData.json
        /// </remarks>
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

        /// <summary>
        /// 根据泛型类型获取配置列表（不缓存）
        /// </summary>
        /// <typeparam name="TItem">列表元素类型，用于推断JSON文件名</typeparam>
        /// <returns>反序列化后的列表实例</returns>
        /// <exception cref="FileNotFoundException">配置文件不存在时抛出</exception>
        /// <exception cref="InvalidOperationException">反序列化失败时抛出</exception>
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
        /// <typeparam name="TItem">列表元素类型</typeparam>
        /// <param name="jsonFileName">JSON文件名（不包含.json扩展名）</param>
        /// <returns>反序列化后的列表实例</returns>
        /// <exception cref="FileNotFoundException">配置文件不存在时抛出</exception>
        /// <exception cref="InvalidOperationException">反序列化失败时抛出</exception>
        /// <remarks>
        /// 当文件名与类型名不一致时使用此方法，例如：
        /// GetList&lt;MatchDataItem&gt;("MatchData") 读取 MatchData.json
        /// </remarks>
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
                throw new InvalidOperationException(
                    $"无法反序列化类型 List<{typeof(TItem).Name}> 的配置"
                );
            }
            return list;
        }

        /// <summary>
        /// 保存配置（覆盖本地 JSON 文件）
        /// </summary>
        /// <typeparam name="T">配置类型</typeparam>
        /// <param name="config">配置实例，不能为null</param>
        /// <returns>是否保存成功。当config为null时返回false，异常时也返回false</returns>
        /// <remarks>
        /// 该方法将配置对象序列化为格式化的JSON（便于人工查看和编辑），
        /// 然后覆盖写入到文件 {_jsonsFolderPath}/{T.Name}.json
        /// 如果目录不存在，会自动创建
        /// 异常被捕获，不会抛出，仅返回false表示失败
        /// </remarks>
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

                // 确保目录存在，如果不存在则创建
                Directory.CreateDirectory(_jsonsFolderPath);

                // 使用缩进格式序列化，方便人工查看和编辑
                var jsonContent = JsonConvert.SerializeObject(config, Formatting.Indented);

                // 覆盖写入JSON文件
                File.WriteAllText(jsonFilePath, jsonContent);

                return true;
            }
            catch
            {
                // 捕获所有异常，返回false表示保存失败
                // TODO: 可以在这里添加日志记录异常信息
                return false;
            }
        }

        /// <summary>
        /// 保存 List 配置（覆盖本地 JSON 文件）
        /// </summary>
        /// <typeparam name="TItem">列表元素类型</typeparam>
        /// <param name="list">列表实例，不能为null</param>
        /// <returns>是否保存成功。当list为null时返回false，异常时也返回false</returns>
        /// <remarks>
        /// 该方法将列表序列化为格式化的JSON（便于人工查看和编辑），
        /// 然后覆盖写入到文件 {_jsonsFolderPath}/{TItem.Name}.json
        /// 如果目录不存在，会自动创建
        /// 异常被捕获，不会抛出，仅返回false表示失败
        ///
        /// 使用示例：
        /// var items = new List&lt;MatchDataItem&gt; { ... };
        /// ConfigurationDataReader.SaveList&lt;MatchDataItem&gt;(items);
        /// 将保存为 MatchDataItem.json
        /// </remarks>
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

                // 确保目录存在，如果不存在则创建
                Directory.CreateDirectory(_jsonsFolderPath);

                // 使用缩进格式序列化，方便人工查看和编辑
                var jsonContent = JsonConvert.SerializeObject(list, Formatting.Indented);

                // 覆盖写入JSON文件
                File.WriteAllText(jsonFilePath, jsonContent);

                return true;
            }
            catch
            {
                // 捕获所有异常，返回false表示保存失败
                // TODO: 可以在这里添加日志记录异常信息
                return false;
            }
        }

        /// <summary>
        /// 保存 List 配置到自定义名称的 JSON 文件（覆盖本地 JSON 文件）
        /// </summary>
        /// <typeparam name="TItem">列表元素类型</typeparam>
        /// <param name="list">列表实例，不能为null</param>
        /// <param name="jsonFileName">JSON文件名（不包含.json扩展名）</param>
        /// <returns>是否保存成功。当list为null时返回false，异常时也返回false</returns>
        /// <remarks>
        /// 该方法将列表序列化为格式化的JSON（便于人工查看和编辑），
        /// 然后覆盖写入到文件 {_jsonsFolderPath}/{jsonFileName}.json
        /// 如果目录不存在，会自动创建
        /// 异常被捕获，不会抛出，仅返回false表示失败
        ///
        /// 使用示例：
        /// var items = new List&lt;SkillPointData&gt; { ... };
        /// ConfigurationDataReader.SaveList&lt;SkillPointData&gt;(items, "tianfu1");
        /// 将保存为 tianfu1.json
        /// </remarks>
        public static bool SaveList<TItem>(IEnumerable<TItem> list, string jsonFileName)
        {
            if (list == null)
            {
                return false;
            }

            try
            {
                var jsonFilePath = Path.Combine(_jsonsFolderPath, $"{jsonFileName}.json");

                // 确保目录存在，如果不存在则创建
                Directory.CreateDirectory(_jsonsFolderPath);

                // 使用缩进格式序列化，方便人工查看和编辑
                var jsonContent = JsonConvert.SerializeObject(list, Formatting.Indented);

                // 覆盖写入JSON文件
                File.WriteAllText(jsonFilePath, jsonContent);

                return true;
            }
            catch
            {
                // 捕获所有异常，返回false表示保存失败
                // TODO: 可以在这里添加日志记录异常信息
                return false;
            }
        }
    }
}
