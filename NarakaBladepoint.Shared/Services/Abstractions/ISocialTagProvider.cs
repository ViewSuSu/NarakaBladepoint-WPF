namespace NarakaBladepoint.Shared.Services.Abstractions
{
    /// <summary>
    /// 社交标签提供者接口
    /// </summary>
    public interface ISocialTagProvider
    {
        /// <summary>
        /// 获取社交标签
        /// </summary>
        /// <returns></returns>
        Task<List<SocialTagData>> GetSocialTags();

        Task<SocialTagData> GetSocialTagByIndex(int index);

        /// <summary>
        /// 根据枚举获取社交标签
        /// </summary>
        /// <param name="socialTagType"></param>
        /// <returns></returns>
        Task<List<SocialTagData>> GetSocialTags(SocialTagType socialTagType);

        /// <summary>
        /// 判断是否被选中
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        Task<bool> GetSocialTagIsSelectedByIndex(int index);

        Task<SocialTagMicData> GetSocialTagMicDataByIndex(int index);

        Task<SocialTagOnlineData> GetSocialTagOnlineDataByIndex(int index);

        Task<List<SocialTagMicData>> GetSocialTagMicDatas();

        Task<List<SocialTagOnlineData>> GetSocialTagOnlineDatas();

        Task<bool> GetSocialTagMicDataIsSelectedByIndex(int index);

        Task<bool> GetSocialTagOnlineDataIsSelectedByIndex(int index);
    }
}
