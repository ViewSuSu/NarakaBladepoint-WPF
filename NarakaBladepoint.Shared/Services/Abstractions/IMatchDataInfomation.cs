using NarakaBladepoint.Shared.Jsons;

namespace NarakaBladepoint.Shared.Services.Abstractions
{
    public interface IMatchDataInfomation
    {
        /// <summary>
        /// 获取对局记录
        /// </summary>
        /// <returns></returns>
        Task<List<MatchDataItem>> GetMatchDataItemsAsync();
    }
}
