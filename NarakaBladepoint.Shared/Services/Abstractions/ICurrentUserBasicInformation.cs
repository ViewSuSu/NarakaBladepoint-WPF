using NarakaBladepoint.Shared.Jsons;

namespace NarakaBladepoint.Shared.Services.Abstractions
{
    /// <summary>
    /// 获取当前登录人基本信息
    /// </summary>
    public interface ICurrentUserBasicInformation
    {
        /// <summary>
        /// 获取当前用户信息
        /// </summary>
        /// <returns></returns>
        Task<UserInformationModel> GetCurrentUserInfoAsync();

        /// <summary>
        /// 获取当前用户赛季信息
        /// </summary>
        /// <returns></returns>
        Task<List<PersonalSeasonDataModel>> GetPersonalSeasonsAsync();

        /// <summary>
        /// 获取当前用户对局记录
        /// </summary>
        /// <returns></returns>
        Task<List<MatchDataItem>> GetMatchDataItemsAsync();
    }
}
