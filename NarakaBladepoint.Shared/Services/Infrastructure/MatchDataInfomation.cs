using NarakaBladepoint.Shared.Jsons;

namespace NarakaBladepoint.Shared.Services.Infrastructure
{
    [Component]
    internal class MatchDataInfomation : IMatchDataInfomation
    {
        public async Task<List<MatchDataItem>> GetMatchDataItemsAsync()
        {
            return ConfigurationDataReader.Get<MatchData>().List;
        }
    }
}
