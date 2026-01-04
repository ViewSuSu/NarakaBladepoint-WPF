namespace NarakaBladepoint.Shared.Services.Infrastructure
{
    [Component]
    internal class ServerInformation : IServerInformation
    {
        public async Task<List<ServerInformationModel>> GetServerInformationAsync()
        {
            return new List<ServerInformationModel>()
            {
                new ServerInformationModel() { City = "杭州市", NetworkLatency = 20 },
                new ServerInformationModel() { City = "广州市", NetworkLatency = 55 },
            };
        }
    }
}
