using System.Threading.Tasks;
using NarakaBladepoint.Shared.Datas;
using NarakaBladepoint.Shared.Enums;

namespace NarakaBladepoint.Shared.Services.Abstractions
{
    /// <summary>
    /// 赛季数据提供器接口
    /// </summary>
    public interface ISeasonDataProvider
    {
        /// <summary>
        /// 获取赛季统计数据
        /// </summary>
        /// <param name="gameMode">游戏模式</param>
        /// <param name="teamSize">排位模式</param>
        /// <param name="seasonType">赛季类型</param>
        /// <returns>赛季统计数据</returns>
        Task<SeasonStatisticsDataModel> GetSeasonStatisticsAsync(
            GameMode gameMode,
            TeamSize teamSize,
            SeasonType seasonType
        );
    }
}