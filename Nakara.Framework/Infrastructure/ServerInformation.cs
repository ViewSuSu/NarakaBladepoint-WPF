namespace Nakara.Framework.Infrastructure
{
    [Component]
    internal class ServerInformation : IServerInformation
    {
        public async Task<List<ServerInformation>> GetServerInformationAsync()
        {
            return default;
        }
    }
}
