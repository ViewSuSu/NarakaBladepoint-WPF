using NarakaBladepoint.Shared.Enums;
using NarakaBladepoint.Shared.Jsons;

namespace NarakaBladepoint.Shared.Services.Infrastructure
{
    [Component(ComponentLifetime.Singleton)]
    internal class CurrentUserBasicInformation : ICurrentUserInfoProvider
    {
        public async Task<UserInformationData> GetCurrentUserInfoAsync()
        {
            return ConfigurationDataReader.Get<UserInformationData>();
        }

        public async Task<List<FriendDataItem>> GetFriendsAsync(string nameKeyword = null)
        {
            return string.IsNullOrEmpty(nameKeyword)
                ? ConfigurationDataReader.Get<FriendData>().List
                : ConfigurationDataReader
                    .Get<FriendData>()
                    .List.Where(x => x.Name.Contains(nameKeyword))
                    .ToList();
        }

        public async Task<List<MatchDataItem>> GetMatchDataItemsAsync()
        {
            return ConfigurationDataReader.Get<MatchData>().List;
        }

        public async Task<List<SeasonDataModel>> GetPersonalSeasonsAsync()
        {
            var random = new Random();
            var result = Enum.GetValues(typeof(SeasonType))
                .Cast<SeasonType>()
                .Select(seasonType => new SeasonDataModel
                {
                    SeasonType = seasonType,
                    GameTime = random.Next(10, 500),
                    TotalMatches = random.Next(10, 300),
                    TotalDefeats = random.Next(10, 200),
                    RankedMatches = random.Next(0, 100),
                    EndlessTrialMatches = random.Next(0, 50),
                    QuickMatchMatches = random.Next(0, 100),
                    DarkDomainMatches = random.Next(0, 50),
                    PvEMatches = random.Next(0, 80),
                    LeyLineWarMatches = random.Next(0, 60),
                    JourneyToGodMatches = random.Next(0, 40),
                    HexagramCalamityMatches = random.Next(0, 30),
                    InfiniteEscortMatches = random.Next(0, 20),
                    FourFiendsCalamityMatches = random.Next(0, 20),
                })
                .ToList();
            return result;
        }
    }
}
