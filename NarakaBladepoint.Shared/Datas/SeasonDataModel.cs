using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NarakaBladepoint.Framework.Core.Extensions;
using NarakaBladepoint.Shared.Enums;
using Newtonsoft.Json;

namespace NarakaBladepoint.Shared.Datas
{
    /// <summary>
    /// 个人赛季信息
    /// </summary>
    public class SeasonDataModel
    {
        public SeasonType SeasonType { get; set; }

        /// <summary>
        /// 赛季类型
        /// </summary>
        public string Season => SeasonType.GetDescription();

        /// <summary>
        /// 游戏时间
        /// </summary>
        public int GameTime { get; set; }

        /// <summary>
        /// 总场次
        /// </summary>
        public int TotalMatches { get; set; }

        /// <summary>
        /// 总击败
        /// </summary>
        public int TotalDefeats { get; set; }

        /// <summary>
        /// 排位赛场次
        /// </summary>
        public int RankedMatches { get; set; }

        /// <summary>
        /// 无尽试炼场次
        /// </summary>
        public int EndlessTrialMatches { get; set; }

        /// <summary>
        /// 快速比赛场次
        /// </summary>
        public int QuickMatchMatches { get; set; }

        /// <summary>
        /// 暗域狂潮场次
        /// </summary>
        public int DarkDomainMatches { get; set; }

        /// <summary>
        /// 人机试炼场次
        /// </summary>
        public int PvEMatches { get; set; }

        /// <summary>
        /// 地脉之战场次
        /// </summary>
        public int LeyLineWarMatches { get; set; }

        /// <summary>
        /// 征神之路场次
        /// </summary>
        public int JourneyToGodMatches { get; set; }

        /// <summary>
        /// 六芒劫场次
        /// </summary>
        public int HexagramCalamityMatches { get; set; }

        /// <summary>
        /// 无间镖客场次
        /// </summary>
        public int InfiniteEscortMatches { get; set; }

        /// <summary>
        /// 四煞劫场次
        /// </summary>
        public int FourFiendsCalamityMatches { get; set; }
    }
}
