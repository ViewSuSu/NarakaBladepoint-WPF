using System.Windows.Media;

namespace Nakara.Shared.Jsons
{
    public class MatchData
    {
        public List<MatchDataItem> List { get; set; }
    }

    public class MatchDataItem
    {
        /// <summary>
        /// 英雄头像
        /// </summary>
        public int AvatarIndex { get; set; }

        /// <summary>
        /// 英雄头像
        /// </summary>
        public ImageSource Avatar => ResourceImageReader.GetHeroImage(AvatarIndex);

        /// <summary>
        /// 当前排名
        /// </summary>
        public int CurrentRank { get; set; }

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
        public double Damage { get; set; }

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
        /// 是否是豪杰对局
        /// </summary>
        public bool IsHightLevel { get; set; }
    }
}
