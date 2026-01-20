using System.Windows.Media;
using NarakaBladepoint.Resources;

namespace NarakaBladepoint.Shared.Jsons
{
    public class MatchData
    {
        public List<MatchDataItem> List { get; set; }
    }

    public class MatchDataItem
    {
        /// <summary>
        /// 数据索引
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// 英雄头像
        /// </summary>
        public int AvatarIndex { get; set; }

        /// <summary>
        /// 英雄头像
        /// </summary>
        public ImageSource Avatar => ResourceImageReader.GetHeroAvatarImage(AvatarIndex);

        /// <summary>
        /// 当前排名
        /// </summary>
        public int CurrentRank { get; set; }

        public bool IsNo1 => CurrentRank == 1;
        public bool IsNo2 => CurrentRank == 2;
        public bool IsNo3To4 => CurrentRank >= 3 && CurrentRank <= 4;

        /// <summary>
        /// 所有队伍
        /// </summary>
        public int AllTeams { get; set; }

        /// <summary>
        /// 击杀数
        /// </summary>
        public int KillNumber { get; set; }

        /// <summary>
        /// 伤害
        /// </summary>
        public int Damage { get; set; }

        /// <summary>
        /// 当前段位
        /// </summary>
        public string CurrentLevel { get; set; }

        /// <summary>
        /// 比赛模式
        /// </summary>
        public string GameMode { get; set; }

        /// <summary>
        /// 比赛时间
        /// </summary>
        public DateTime GameTime { get; set; }

        /// <summary>
        /// 分数
        /// </summary>
        public int Score { get; set; }

        /// <summary>
        /// 分数变化
        /// </summary>
        public int ChangeScore { get; set; }

        /// <summary>
        /// 是否是豪杰对局
        /// </summary>
        public bool IsHightLevel => Score >= 4500;

        /// <summary>
        /// 是否加分
        /// </summary>
        public bool IsAdd { get; set; }

        /// <summary>
        /// 是否额外加分400+
        /// </summary>
        public bool IsEasy { get; set; }

        /// <summary>
        /// 对局成就图片索引列表 (索引从1开始)
        /// </summary>
        public List<int> AchievementIndexes { get; set; } = new();

        /// <summary>
        /// 对局成就图片列表
        /// </summary>
        public List<ImageSource> Achievements =>
            AchievementIndexes
                .Select(index => ResourceImageReader.GetHistoryMatchRecordImage(index))
                .Where(img => img != null)
                .ToList();
    }
}
