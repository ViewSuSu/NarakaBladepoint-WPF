using NarakaBladepoint.Shared.Jsons;

namespace NarakaBladepoint.Shared.Services.Abstractions
{
    /// <summary>
    /// 当前登录人基本信息服务
    /// </summary>
    public interface ICurrentUserInformationProvider
    {
        /// <summary>
        /// 获取当前用户信息
        /// </summary>
        /// <returns></returns>
        Task<UserInformationData> GetCurrentUserInfoAsync();

        /// <summary>
        /// 获取当前用户赛季信息
        /// </summary>
        /// <returns></returns>
        Task<List<SeasonDataModel>> GetPersonalSeasonsAsync();

        /// <summary>
        /// 获取当前用户对局记录
        /// </summary>
        /// <returns></returns>
        Task<List<MatchDataItem>> GetMatchDataItemsAsync();
    }
}
