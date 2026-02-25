using NarakaBladepoint.Shared.Datas;

namespace NarakaBladepoint.Shared.Services.Implementation
{
    /// <summary>
    /// 天赋技能点数据提供器实现
    /// </summary>
    [Component(ComponentLifetime.Singleton)]
    internal class TianfuSkillPointProvider : ITianfuSkillPointProvider
    {
        /// <summary>
        /// 获取天赋技能点配置
        /// </summary>
        public async Task<TianfuSkillPointData> GetTianfuSkillPointAsync(string tianfuName)
        {
            try
            {
                // 使用自定义文件名读取，例如：tianfu1.json, tianfu2.json, tianfu3.json
                var skillPoints = ConfigurationDataReader.GetList<SkillPointData>(tianfuName);

                // 计算剩余技能点（总共20个，减去已分配的）
                var totalAllocated = skillPoints.Sum(sp => sp.CurrentLearned);
                var remainingPoints = 20 - totalAllocated;

                var tianfuData = new TianfuSkillPointData
                {
                    TianfuName = tianfuName,
                    RemainingPoints = remainingPoints,
                    SkillPoints = skillPoints
                };

                return await Task.FromResult(tianfuData);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Failed to load {tianfuName} skill point data: {ex.Message}");
                // 返回默认配置
                return await CreateDefaultTianfuSkillPointAsync(tianfuName);
            }
        }

        /// <summary>
        /// 保存天赋技能点配置
        /// </summary>
        public async Task<bool> SaveTianfuSkillPointAsync(TianfuSkillPointData tianfuData)
        {
            if (tianfuData == null)
            {
                return false;
            }

            try
            {
                // 使用自定义文件名保存
                var result = ConfigurationDataReader.SaveList<SkillPointData>(
                    tianfuData.SkillPoints,
                    tianfuData.TianfuName
                );
                return await Task.FromResult(result);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Failed to save {tianfuData.TianfuName} skill point data: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 创建默认的天赋技能点配置
        /// </summary>
        private Task<TianfuSkillPointData> CreateDefaultTianfuSkillPointAsync(string tianfuName)
        {
            var skillPointConfigs = CreateSkillPointConfigs();

            var skillPoints = new List<SkillPointData>();
            int index = 0;

            foreach (var config in skillPointConfigs)
            {
                skillPoints.Add(new SkillPointData
                {
                    Index = index,
                    SkillName = config.Name,
                    CircleLevel = config.Circle,
                    Position = config.Position,
                    Direction = config.Direction,
                    CurrentLearned = 0,
                    TotalLearnable = config.Max,
                    IsLearnable = true
                });
                index++;
            }

            var tianfuData = new TianfuSkillPointData
            {
                TianfuName = tianfuName,
                RemainingPoints = 20,
                SkillPoints = skillPoints
            };

            return Task.FromResult(tianfuData);
        }

        /// <summary>
        /// 创建技能点配置
        /// </summary>
        private List<(string Name, int Circle, string Position, string Direction, int Max)> CreateSkillPointConfigs()
        {
            const int FIRST_CIRCLE_MAX = 4;
            const int SECOND_CIRCLE_MAX = 4;
            const int THIRD_CIRCLE_MAX = 2;

            var configs = new List<(string, int, string, string, int)>
            {
                // LeftUp corner
                ("Lu_TenOClock", 1, "LeftUp", "TenOClock", FIRST_CIRCLE_MAX),
                ("Ma_ElevenOClock", 1, "LeftUp", "ElevenOClock", FIRST_CIRCLE_MAX),
                ("He_TenOClock", 2, "LeftUp", "TenOClock", SECOND_CIRCLE_MAX),
                ("Ou_ElevenOClock", 2, "LeftUp", "ElevenOClock", SECOND_CIRCLE_MAX),
                ("Xiong_TenOClock", 3, "LeftUp", "TenOClock", THIRD_CIRCLE_MAX),
                ("Shi_ElevenOClock", 3, "LeftUp", "ElevenOClock", THIRD_CIRCLE_MAX),

                // RightUp corner
                ("Lu_TenOClock_MirrorH", 1, "RightUp", "TenOClock", FIRST_CIRCLE_MAX),
                ("Ma_ElevenOClock_MirrorH", 1, "RightUp", "ElevenOClock", FIRST_CIRCLE_MAX),
                ("He_TenOClock_MirrorH", 2, "RightUp", "TenOClock", SECOND_CIRCLE_MAX),
                ("Ou_ElevenOClock_MirrorH", 2, "RightUp", "ElevenOClock", SECOND_CIRCLE_MAX),
                ("Xiong_TenOClock_MirrorH", 3, "RightUp", "TenOClock", THIRD_CIRCLE_MAX),
                ("Shi_ElevenOClock_MirrorH", 3, "RightUp", "ElevenOClock", THIRD_CIRCLE_MAX),

                // LeftDown corner
                ("Lu_TenOClock_MirrorV", 1, "LeftDown", "TenOClock", FIRST_CIRCLE_MAX),
                ("Ma_ElevenOClock_MirrorV", 1, "LeftDown", "ElevenOClock", FIRST_CIRCLE_MAX),
                ("He_TenOClock_MirrorV", 2, "LeftDown", "TenOClock", SECOND_CIRCLE_MAX),
                ("Ou_ElevenOClock_MirrorV", 2, "LeftDown", "ElevenOClock", SECOND_CIRCLE_MAX),
                ("Xiong_TenOClock_MirrorV", 3, "LeftDown", "TenOClock", THIRD_CIRCLE_MAX),
                ("Shi_ElevenOClock_MirrorV", 3, "LeftDown", "ElevenOClock", THIRD_CIRCLE_MAX),

                // RightDown corner
                ("Lu_TenOClock_MirrorHV", 1, "RightDown", "TenOClock", FIRST_CIRCLE_MAX),
                ("Ma_ElevenOClock_MirrorHV", 1, "RightDown", "ElevenOClock", FIRST_CIRCLE_MAX),
                ("He_TenOClock_MirrorHV", 2, "RightDown", "TenOClock", SECOND_CIRCLE_MAX),
                ("Ou_ElevenOClock_MirrorHV", 2, "RightDown", "ElevenOClock", SECOND_CIRCLE_MAX),
                ("Xiong_TenOClock_MirrorHV", 3, "RightDown", "TenOClock", THIRD_CIRCLE_MAX),
                ("Shi_ElevenOClock_MirrorHV", 3, "RightDown", "ElevenOClock", THIRD_CIRCLE_MAX),
            };

            return configs;
        }
    }
}
