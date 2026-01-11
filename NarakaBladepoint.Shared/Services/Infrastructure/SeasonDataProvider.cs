using System.Threading.Tasks;
using NarakaBladepoint.Resources;
using NarakaBladepoint.Shared.Datas;
using NarakaBladepoint.Shared.Enums;
using NarakaBladepoint.Shared.Services.Abstractions;

namespace NarakaBladepoint.Shared.Services.Infrastructure
{
    /// <summary>
    /// 赛季数据提供器实现
    /// </summary>
    [Component(ComponentLifetime.Singleton)]
    internal class SeasonDataProvider : ISeasonDataProvider
    {
        /// <summary>
        /// 获取赛季统计数据
        /// </summary>
        /// <param name="gameMode">游戏模式</param>
        /// <param name="teamSize">排位模式</param>
        /// <param name="seasonType">赛季类型</param>
        /// <returns>赛季统计数据</returns>
        public async Task<SeasonStatisticsDataModel> GetSeasonStatisticsAsync(
            GameMode gameMode,
            TeamSize teamSize,
            SeasonType seasonType
        )
        {
            // 从JSON文件中读取赛季统计数据
            var seasonStatisticsList = ConfigurationDataReader.GetList<SeasonStatisticsDataModel>();

            // 查找匹配的数据
            var seasonStatistics = seasonStatisticsList.FirstOrDefault(data =>
                data.GameMode == gameMode
                && data.TeamSize == teamSize
                && data.SeasonType == seasonType
            );

            return seasonStatistics;
        }
    }
}
