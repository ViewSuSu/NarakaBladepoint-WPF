using NarakaBladepoint.Resources;
using NarakaBladepoint.Shared.Enums;

namespace NarakaBladepoint.Shared.Services.Implementation
{
    /// <summary>
    /// 英雄数据提供器实现
    /// </summary>
    [Component(ComponentLifetime.Singleton)]
    internal class HeroDataProvider : IHeroDataProvider
    {
        /// <summary>
        /// 获取所有英雄数据
        /// </summary>
        /// <param name="gameMode">游戏模式</param>
        /// <param name="teamSize">排位模式</param>
        /// <param name="seasonType">赛季类型</param>
        /// <returns>英雄数据列表</returns>
        public async Task<List<HeroDataModel>> GetHeroDataListAsync(
            GameMode gameMode,
            TeamSize teamSize,
            SeasonType seasonType
        )
        {
            var heroDataList = ConfigurationDataReader.GetList<HeroDataModel>();

            // 生成所有英雄的数据列表，对于没有数据的英雄使用默认值
            var allHeroDataList = new List<HeroDataModel>();

            // 根据英雄总数生成所有英雄的数据
            for (int heroIndex = 0; heroIndex < ResourceImageReader.HeroCount; heroIndex++)
            {
                // 查找是否有对应英雄的数据
                var heroData = heroDataList.FirstOrDefault(hero =>
                    hero.HeroIndex == heroIndex
                    && hero.GameMode == gameMode
                    && hero.TeamSize == teamSize
                    && hero.SeasonType == seasonType
                );

                if (heroData == null)
                {
                    // 如果没有数据，创建一个默认的英雄数据对象
                    heroData = new HeroDataModel
                    {
                        HeroIndex = heroIndex,
                        GameMode = gameMode,
                        TeamSize = teamSize,
                        SeasonType = seasonType,
                        GameTime = 0,
                        HeroScore = 0,
                        RankDescription = string.Empty,
                        HistoricalHighestRank = string.Empty,
                        Recent12Games = new Recent12GamesData
                        {
                            TopFiveRate = 0,
                            AverageDamage = 0,
                            KillDeathRatio = 0,
                            IsTop1_5Percent = false,
                        },
                        AllGames = new AllGamesData
                        {
                            TopFiveRate = 0,
                            ChampionRate = 0,
                            WinCount = 0,
                            AverageDamage = 0,
                            MaxDamagePerGame = 0,
                            AverageHeal = 0,
                            TotalEliminations = 0,
                            KillDeathRatio = 0,
                            MaxKillsPerGame = 0,
                            AverageKills = 0,
                            TotalKills = 0,
                        },
                        SpecialSkill = new SpecialSkillData
                        {
                            SkillName = string.Empty,
                            SkillEffectData = 0,
                            AverageData = 0,
                        },
                    };
                }

                allHeroDataList.Add(heroData);
            }

            return allHeroDataList;
        }
    }
}
