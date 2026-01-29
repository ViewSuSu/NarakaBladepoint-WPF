using NarakaBladepoint.Shared.Enums;

namespace NarakaBladepoint.Shared.Services.Abstractions
{
    /// <summary>
    /// 英雄数据提供器接口
    /// </summary>
    public interface IHeroDataProvider
    {
        /// <summary>
        /// 获取所有英雄数据
        /// </summary>
        /// <param name="gameMode">游戏模式</param>
        /// <param name="teamSize">排位模式</param>
        /// <param name="seasonType">赛季类型</param>
        /// <returns>英雄数据列表</returns>
        Task<List<HeroDataModel>> GetHeroDataListAsync(
            GameMode gameMode,
            TeamSize teamSize,
            SeasonType seasonType
        );
    }
}
