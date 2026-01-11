using NarakaBladepoint.Shared.Enums;

namespace NarakaBladepoint.Shared.Datas
{
    /// <summary>
    /// 赛季统计数据模型
    /// </summary>
    public class SeasonStatisticsDataModel
    {
        /// <summary>
        /// 赛季类型
        /// </summary>
        public SeasonType SeasonType { get; set; }
        
        /// <summary>
        /// 游戏模式
        /// </summary>
        public GameMode GameMode { get; set; }
        
        /// <summary>
        /// 队伍规模
        /// </summary>
        public TeamSize TeamSize { get; set; }
        
        /// <summary>
        /// 当前赛季排名
        /// </summary>
        public string CurrentSeasonRank { get; set; }
        
        /// <summary>
        /// 游戏场次
        /// </summary>
        public int TotalMatches { get; set; }
        
        /// <summary>
        /// 天选次数
        /// </summary>
        public int ChampionCount { get; set; }
        
        /// <summary>
        /// 前五次数
        /// </summary>
        public int TopFiveCount { get; set; }
        
        /// <summary>
        /// 总击败
        /// </summary>
        public int TotalDefeats { get; set; }
        
        /// <summary>
        /// 总淘汰
        /// </summary>
        public int TotalEliminations { get; set; }
        
        /// <summary>
        /// 击败/被击败
        /// </summary>
        public double KillDeathRatio { get; set; }
        
        /// <summary>
        /// 最长生存时间（秒）
        /// </summary>
        public int LongestSurvivalTime { get; set; }
        
        /// <summary>
        /// 场均生存时间（秒）
        /// </summary>
        public int AverageSurvivalTime { get; set; }
        
        /// <summary>
        /// 最长移动距离
        /// </summary>
        public int LongestDistance { get; set; }
        
        /// <summary>
        /// 场均移动距离
        /// </summary>
        public int AverageDistance { get; set; }
        
        /// <summary>
        /// 天选率
        /// </summary>
        public double ChampionRate { get; set; }
        
        /// <summary>
        /// 前五率
        /// </summary>
        public double TopFiveRate { get; set; }
        
        /// <summary>
        /// 单局最高击杀
        /// </summary>
        public int MaxKillsPerGame { get; set; }
        
        /// <summary>
        /// 场均击杀
        /// </summary>
        public double AverageKills { get; set; }
        
        /// <summary>
        /// 单局最高伤害
        /// </summary>
        public int MaxDamagePerGame { get; set; }
        
        /// <summary>
        /// 场均伤害
        /// </summary>
        public double AverageDamage { get; set; }
        
        /// <summary>
        /// 单局最高助攻
        /// </summary>
        public int MaxAssistsPerGame { get; set; }
        
        /// <summary>
        /// 场均助攻
        /// </summary>
        public double AverageAssists { get; set; }
        
        /// <summary>
        /// 场均治疗量
        /// </summary>
        public double AverageHealing { get; set; }
        
        /// <summary>
        /// 总救援次数
        /// </summary>
        public int TotalRescues { get; set; }
        
        /// <summary>
        /// 降龙次数
        /// </summary>
        public int DragonSlayerCount { get; set; }
        
        /// <summary>
        /// 箭破天穹次数
        /// </summary>
        public int SkyArrowCount { get; set; }
    }
}