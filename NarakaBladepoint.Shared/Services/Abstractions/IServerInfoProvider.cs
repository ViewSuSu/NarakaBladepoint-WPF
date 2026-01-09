namespace NarakaBladepoint.Shared.Services.Abstractions
{
    /// <summary>
    /// 获取服务器信息
    /// </summary>
    public interface IServerInfoProvider
    {
        /// <summary>
        /// 获取所有服务器信息
        /// </summary>
        /// <returns></returns>
        Task<List<ServerInformationModel>> GeAlltServerInfosAsync();
    }
}
