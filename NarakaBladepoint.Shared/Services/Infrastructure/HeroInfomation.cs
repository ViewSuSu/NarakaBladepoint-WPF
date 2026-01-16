using System.Windows.Media;
using NarakaBladepoint.Framework.Core.Extensions;
using NarakaBladepoint.Resources;

namespace NarakaBladepoint.Shared.Services.Infrastructure
{
    [Component(ComponentLifetime.Singleton)]
    internal class HeroInfomation : IHeroInfoProvider
    {
        private readonly ICurrentUserInfoProvider currentUserInformationProvider;

        public HeroInfomation(ICurrentUserInfoProvider currentUserInformationProvider)
        {
            this.currentUserInformationProvider = currentUserInformationProvider;
        }

        public async Task<HeroAvatarModel> GetHeroAvatarModelByIdAsync(int index)
        {
            var reuslts = GetHeroAvatarModelsAsync().Result;
            return reuslts.FirstOrDefault(x => x.Index == index);
        }

        public async Task<List<HeroAvatarModel>> GetHeroAvatarModelsAsync()
        {
            return ConfigurationDataReader.GetList<HeroAvatarModel>();
        }

        public async Task<bool> GetHeroHeroTagIsSelectedAsync(int index)
        {
            var userModel = await currentUserInformationProvider.GetCurrentUserInfoAsync();
            return userModel.SelectedHeroTags.Contains(index);
        }

        public async Task<HeroTagModel> GetHeroHeroTagModelModelByIdAsync(int index)
        {
            return new HeroTagModel() { Index = index };
        }

        public async Task<List<HeroTagModel>> GetHeroTagModelsAsync()
        {
            List<HeroTagModel> tags = [];
            for (int i = 0; i < ResourceImageReader.HeroTagCount; i++)
            {
                tags.Add(new HeroTagModel() { Index = i });
            }
            return tags;
        }

        public async Task<List<HeroTagModel>> GetSelectedHeroTagModelsAsync()
        {
            List<HeroTagModel> tagModels = [];
            var userModel = await currentUserInformationProvider.GetCurrentUserInfoAsync();
            foreach (var tagIndex in userModel.SelectedHeroTags)
            {
                tagModels.Add(new HeroTagModel() { Index = tagIndex });
            }
            return tagModels;
        }
    }
}
