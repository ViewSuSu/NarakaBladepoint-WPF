namespace NarakaBladepoint.Shared.Services.Abstractions
{
    /// <summary>
    /// 获取服务器信息
    /// </summary>
    public interface IServerInformation
    {
        Task<List<ServerInformationModel>> GetServerInformationAsync();
    }
}
