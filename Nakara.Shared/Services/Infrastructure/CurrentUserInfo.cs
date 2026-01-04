namespace Nakara.Shared.Services.Infrastructure
{
    [Component]
    internal class CurrentUserInfo : ICurrentUserInformation
    {
        /// <summary>
        /// 获取当前用户信息
        /// </summary>
        /// <returns></returns>
        public async Task<UserInformationModel> GetCurrentUserInfoAsync()
        {
            return default;
        }
    }
}
