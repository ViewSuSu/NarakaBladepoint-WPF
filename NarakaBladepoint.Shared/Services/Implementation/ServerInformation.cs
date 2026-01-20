namespace NarakaBladepoint.Shared.Services.Implementation
{
    [Component(ComponentLifetime.Singleton)]
    internal class ServerInformation : IServerInfoProvider
    {
        public async Task<List<ServerInformationModel>> GeAlltServerInfosAsync()
        {
            return new List<ServerInformationModel>()
            {
                new ServerInformationModel() { City = "杭州市", NetworkLatency = 20 },
                new ServerInformationModel() { City = "广州市", NetworkLatency = 55 },
            };
        }
    }
}
