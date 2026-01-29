using System.Windows.Media;
using NarakaBladepoint.Framework.Core.Extensions;
using NarakaBladepoint.Resources;
using NarakaBladepoint.Shared.Enums;

namespace NarakaBladepoint.Shared.Datas
{
    /// <summary>
    /// 英雄数据模型
    /// </summary>
    public class HeroDataModel
    {
        /// <summary>
        /// 英雄索引
        /// </summary>
        public int HeroIndex { get; set; } = -1;

        /// <summary>
        /// 英雄名称
        /// </summary>
        public string HeroName => Avatar.GetFileName();

        /// <summary>
        /// 游戏模式
        /// </summary>
        public GameMode GameMode { get; set; }

        /// <summary>
        /// 排位模式
        /// </summary>
        public TeamSize TeamSize { get; set; }

        /// <summary>
        /// 赛季类型
        /// </summary>
        public SeasonType SeasonType { get; set; }

        /// <summary>
        /// 游戏时间（小时）
        /// </summary>
        public double GameTime { get; set; }

        /// <summary>
        /// 英雄积分
        /// </summary>
        public int HeroScore { get; set; }

        /// <summary>
        /// 排名描述
        /// </summary>
        public string RankDescription { get; set; }

        /// <summary>
        /// 历史最高排名
        /// </summary>
        public string HistoricalHighestRank { get; set; }

        /// <summary>
        /// 最近12场数据
        /// </summary>
        public Recent12GamesData Recent12Games { get; set; }

        /// <summary>
        /// 所有场次数据
        /// </summary>
        public AllGamesData AllGames { get; set; }

        /// <summary>
        /// 特殊技能数据
        /// </summary>
        public SpecialSkillData SpecialSkill { get; set; }

        /// <summary>
        /// 头像图片源
        /// </summary>
        public ImageSource Avatar =>
            HeroIndex != -1 ? ResourceImageReader.GetHeroAvatarImage(HeroName) : null;
    }

    /// <summary>
    /// 最近12场数据
    /// </summary>
    public class Recent12GamesData
    {
        /// <summary>
        /// 前五率
        /// </summary>
        public double TopFiveRate { get; set; }

        /// <summary>
        /// 场均伤害
        /// </summary>
        public int AverageDamage { get; set; }

        /// <summary>
        /// 击败/被击败
        /// </summary>
        public double KillDeathRatio { get; set; }

        /// <summary>
        /// 是否处于同段位前1.5%
        /// </summary>
        public bool IsTop1_5Percent { get; set; }
    }

    /// <summary>
    /// 所有场次数据
    /// </summary>
    public class AllGamesData
    {
        /// <summary>
        /// 前五率
        /// </summary>
        public double TopFiveRate { get; set; }

        /// <summary>
        /// 天选率
        /// </summary>
        public double ChampionRate { get; set; }

        /// <summary>
        /// 获胜数
        /// </summary>
        public int WinCount { get; set; }

        /// <summary>
        /// 场均伤害
        /// </summary>
        public int AverageDamage { get; set; }

        /// <summary>
        /// 单局最高伤害
        /// </summary>
        public int MaxDamagePerGame { get; set; }

        /// <summary>
        /// 场均治疗
        /// </summary>
        public int AverageHeal { get; set; }

        /// <summary>
        /// 总淘汰
        /// </summary>
        public int TotalEliminations { get; set; }

        /// <summary>
        /// 击败/被击败
        /// </summary>
        public double KillDeathRatio { get; set; }

        /// <summary>
        /// 单局最高击败
        /// </summary>
        public int MaxKillsPerGame { get; set; }

        /// <summary>
        /// 场均击败
        /// </summary>
        public double AverageKills { get; set; }

        /// <summary>
        /// 总击败
        /// </summary>
        public int TotalKills { get; set; }
    }

    /// <summary>
    /// 特殊技能数据
    /// </summary>
    public class SpecialSkillData
    {
        /// <summary>
        /// 技能名称
        /// </summary>
        public string SkillName { get; set; }

        /// <summary>
        /// 技能效果数据
        /// </summary>
        public int SkillEffectData { get; set; }

        /// <summary>
        /// 场均数据
        /// </summary>
        public double AverageData { get; set; }
    }
}
