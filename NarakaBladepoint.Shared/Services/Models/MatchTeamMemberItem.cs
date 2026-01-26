using System.Collections.Generic;
using System.Windows.Media;

namespace NarakaBladepoint.Shared.Services.Models
{
    /// <summary>
    /// 对局队伍成员信息
    /// </summary>
    public class MatchTeamMemberItem
    {
        /// <summary>
        /// 玩家昵称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 玩家头像
        /// </summary>
        public ImageSource Avatar { get; set; }

        /// <summary>
        /// 玩家成就/称号图片列表
        /// </summary>
        public List<ImageSource> Titles { get; set; } = new();

        /// <summary>
        /// 生存时间
        /// </summary>
        public string SurvivalTime { get; set; }

        /// <summary>
        /// 击败数
        /// </summary>
        public int TeamKills { get; set; }

        /// <summary>
        /// 总伤害
        /// </summary>
        public int TotalDamage { get; set; }

        /// <summary>
        /// 总治疗量
        /// </summary>
        public int TotalHealing { get; set; }

        /// <summary>
        /// 救援次数
        /// </summary>
        public int AwardedTeams { get; set; }

        /// <summary>
        /// 亲密度（经验）
        /// </summary>
        public int Experience { get; set; }

        /// <summary>
        /// 是否是当前用户（用于突出显示）
        /// </summary>
        public bool IsCurrentUser { get; set; }
    }
}
