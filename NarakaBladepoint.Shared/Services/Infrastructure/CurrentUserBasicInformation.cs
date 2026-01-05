using NarakaBladepoint.Shared.Enums;
using NarakaBladepoint.Shared.Jsons;

namespace NarakaBladepoint.Shared.Services.Infrastructure
{
    [Component(ComponentLifetime.Singleton)]
    internal class CurrentUserBasicInformation : ICurrentUserInformationProvider
    {
        public async Task<UserInformationModel> GetCurrentUserInfoAsync()
        {
            return new UserInformationModel()
            {
                Id = 153153121323213,
                AvatarIndex = 0,
                Name = "野排劳张",
                Level = 425,
                Exp = 50,
                Credits = 10000,
                ActiveValue = 825,
                TotalFavorites = 2656232,
                LoginDays = 425,
                WeaponSkins = 2123,
                HeroSkins = 862,
            };
        }

        public async Task<List<MatchDataItem>> GetMatchDataItemsAsync()
        {
            return ConfigurationDataReader.Get<MatchData>().List;
        }

        public async Task<List<PersonalSeasonDataModel>> GetPersonalSeasonsAsync()
        {
            var random = new Random();
            var result = Enum.GetValues(typeof(SeasonType))
                .Cast<SeasonType>()
                .Select(seasonType => new PersonalSeasonDataModel
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
