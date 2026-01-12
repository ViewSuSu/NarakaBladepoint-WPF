using System.Windows.Media;
using NarakaBladepoint.Framework.Core.Extensions;
using NarakaBladepoint.Resources;
using NarakaBladepoint.Shared.Datas;
using NarakaBladepoint.Shared.Enums;
using Prism.Mvvm;

namespace NarakaBladepoint.Modules.PersonalInformation.UI.HeroData.Models
{
    /// <summary>
    /// 英雄数据模型（用于UI绑定）
    /// </summary>
    internal class HeroDataItemModel : BindableBase
    {
        private int _heroIndex;
        private GameMode _gameMode;
        private TeamSize _teamSize;
        private SeasonType _seasonType;
        private double _gameTime;
        private int _heroScore;
        private string _rankDescription;
        private string _historicalHighestRank;
        private Recent12GamesData _recent12Games;
        private AllGamesData _allGames;
        private SpecialSkillData _specialSkill;

        /// <summary>
        /// 英雄索引
        /// </summary>
        public int HeroIndex
        {
            get { return _heroIndex; }
            set
            {
                SetProperty(ref _heroIndex, value);
                RaisePropertyChanged(nameof(HeroName));
                RaisePropertyChanged(nameof(Avatar));
            }
        }

        /// <summary>
        /// 英雄名称
        /// </summary>
        public string HeroName => Avatar.GetFileName();

        /// <summary>
        /// 游戏模式
        /// </summary>
        public GameMode GameMode
        {
            get { return _gameMode; }
            set { SetProperty(ref _gameMode, value); }
        }

        /// <summary>
        /// 排位模式
        /// </summary>
        public TeamSize TeamSize
        {
            get { return _teamSize; }
            set { SetProperty(ref _teamSize, value); }
        }

        /// <summary>
        /// 赛季类型
        /// </summary>
        public SeasonType SeasonType
        {
            get { return _seasonType; }
            set { SetProperty(ref _seasonType, value); }
        }

        /// <summary>
        /// 游戏时间（小时）
        /// </summary>
        public double GameTime
        {
            get { return _gameTime; }
            set { SetProperty(ref _gameTime, value); }
        }

        /// <summary>
        /// 英雄积分
        /// </summary>
        public int HeroScore
        {
            get { return _heroScore; }
            set { SetProperty(ref _heroScore, value); }
        }

        /// <summary>
        /// 排名描述
        /// </summary>
        public string RankDescription
        {
            get { return _rankDescription; }
            set { SetProperty(ref _rankDescription, value); }
        }

        /// <summary>
        /// 历史最高排名
        /// </summary>
        public string HistoricalHighestRank
        {
            get { return _historicalHighestRank; }
            set { SetProperty(ref _historicalHighestRank, value); }
        }

        /// <summary>
        /// 最近12场数据
        /// </summary>
        public Recent12GamesData Recent12Games
        {
            get { return _recent12Games; }
            set { SetProperty(ref _recent12Games, value); }
        }

        /// <summary>
        /// 所有场次数据
        /// </summary>
        public AllGamesData AllGames
        {
            get { return _allGames; }
            set { SetProperty(ref _allGames, value); }
        }

        /// <summary>
        /// 特殊技能数据
        /// </summary>
        public SpecialSkillData SpecialSkill
        {
            get { return _specialSkill; }
            set { SetProperty(ref _specialSkill, value); }
        }

        /// <summary>
        /// 头像图片源
        /// </summary>
        public ImageSource Avatar =>
            HeroIndex != -1 ? ResourceImageReader.GetHeroImage(HeroIndex) : null;
    }

    /// <summary>
    /// 最近12场数据（用于UI绑定）
    /// </summary>
    internal class Recent12GamesData : BindableBase
    {
        private double _topFiveRate;
        private int _averageDamage;
        private double _killDeathRatio;
        private bool _isTop1_5Percent;

        /// <summary>
        /// 前五率
        /// </summary>
        public double TopFiveRate
        {
            get { return _topFiveRate; }
            set { SetProperty(ref _topFiveRate, value); }
        }

        /// <summary>
        /// 场均伤害
        /// </summary>
        public int AverageDamage
        {
            get { return _averageDamage; }
            set { SetProperty(ref _averageDamage, value); }
        }

        /// <summary>
        /// 击败/被击败
        /// </summary>
        public double KillDeathRatio
        {
            get { return _killDeathRatio; }
            set { SetProperty(ref _killDeathRatio, value); }
        }

        /// <summary>
        /// 是否处于同段位前1.5%
        /// </summary>
        public bool IsTop1_5Percent
        {
            get { return _isTop1_5Percent; }
            set { SetProperty(ref _isTop1_5Percent, value); }
        }
    }

    /// <summary>
    /// 所有场次数据（用于UI绑定）
    /// </summary>
    internal class AllGamesData : BindableBase
    {
        private double _topFiveRate;
        private double _championRate;
        private int _winCount;
        private int _averageDamage;
        private int _maxDamagePerGame;
        private int _averageHeal;
        private int _totalEliminations;
        private double _killDeathRatio;
        private int _maxKillsPerGame;
        private double _averageKills;
        private int _totalKills;

        /// <summary>
        /// 前五率
        /// </summary>
        public double TopFiveRate
        {
            get { return _topFiveRate; }
            set { SetProperty(ref _topFiveRate, value); }
        }

        /// <summary>
        /// 天选率
        /// </summary>
        public double ChampionRate
        {
            get { return _championRate; }
            set { SetProperty(ref _championRate, value); }
        }

        /// <summary>
        /// 获胜数
        /// </summary>
        public int WinCount
        {
            get { return _winCount; }
            set { SetProperty(ref _winCount, value); }
        }

        /// <summary>
        /// 场均伤害
        /// </summary>
        public int AverageDamage
        {
            get { return _averageDamage; }
            set { SetProperty(ref _averageDamage, value); }
        }

        /// <summary>
        /// 单局最高伤害
        /// </summary>
        public int MaxDamagePerGame
        {
            get { return _maxDamagePerGame; }
            set { SetProperty(ref _maxDamagePerGame, value); }
        }

        /// <summary>
        /// 场均治疗
        /// </summary>
        public int AverageHeal
        {
            get { return _averageHeal; }
            set { SetProperty(ref _averageHeal, value); }
        }

        /// <summary>
        /// 总淘汰
        /// </summary>
        public int TotalEliminations
        {
            get { return _totalEliminations; }
            set { SetProperty(ref _totalEliminations, value); }
        }

        /// <summary>
        /// 击败/被击败
        /// </summary>
        public double KillDeathRatio
        {
            get { return _killDeathRatio; }
            set { SetProperty(ref _killDeathRatio, value); }
        }

        /// <summary>
        /// 单局最高击败
        /// </summary>
        public int MaxKillsPerGame
        {
            get { return _maxKillsPerGame; }
            set { SetProperty(ref _maxKillsPerGame, value); }
        }

        /// <summary>
        /// 场均击败
        /// </summary>
        public double AverageKills
        {
            get { return _averageKills; }
            set { SetProperty(ref _averageKills, value); }
        }

        /// <summary>
        /// 总击败
        /// </summary>
        public int TotalKills
        {
            get { return _totalKills; }
            set { SetProperty(ref _totalKills, value); }
        }
    }

    /// <summary>
    /// 特殊技能数据（用于UI绑定）
    /// </summary>
    internal class SpecialSkillData : BindableBase
    {
        private string _skillName;
        private int _skillEffectData;
        private double _averageData;

        /// <summary>
        /// 技能名称
        /// </summary>
        public string SkillName
        {
            get { return _skillName; }
            set { SetProperty(ref _skillName, value); }
        }

        /// <summary>
        /// 技能效果数据
        /// </summary>
        public int SkillEffectData
        {
            get { return _skillEffectData; }
            set { SetProperty(ref _skillEffectData, value); }
        }

        /// <summary>
        /// 场均数据
        /// </summary>
        public double AverageData
        {
            get { return _averageData; }
            set { SetProperty(ref _averageData, value); }
        }
    }
}
