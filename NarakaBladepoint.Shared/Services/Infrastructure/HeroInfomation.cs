using System.Windows.Media;
using NarakaBladepoint.Framework.Core.Extensions;
using NarakaBladepoint.Resources;

namespace NarakaBladepoint.Shared.Services.Infrastructure
{
    [Component(ComponentLifetime.Singleton)]
    internal class HeroInfomation : IHeroInfomation
    {
        private readonly ICurrentUserInformationProvider currentUserInformationProvider;

        public HeroInfomation(ICurrentUserInformationProvider currentUserInformationProvider)
        {
            this.currentUserInformationProvider = currentUserInformationProvider;
        }

        public async Task<HeroAvatarModel> GetHeroAvatarModelByIdAsync(int index)
        {
            ValidateIndex(index);
            var imagesouce = ResourceImageReader.GetHeroImage(index);
            return CreateHeroAvatarModel(index, imagesouce);
        }

        public async Task<List<HeroAvatarModel>> GetHeroAvatarModelsAsync()
        {
            List<HeroAvatarModel> heroAvatarModels = [];
            for (int i = 0; i < ResourceImageReader.HeroCount; i++)
            {
                var imagesouce = ResourceImageReader.GetHeroImage(i);
                heroAvatarModels.Add(CreateHeroAvatarModel(i, imagesouce));
            }
            return heroAvatarModels;
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

        private HeroAvatarModel CreateHeroAvatarModel(int index, ImageSource imagesouce)
        {
            return new HeroAvatarModel()
            {
                Index = index,
                Avatar = imagesouce,
                Name = imagesouce.GetFileName(),
            };
        }

        private void ValidateIndex(int index)
        {
            if (index < 0 || index >= ResourceImageReader.HeroCount)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(index),
                    $"Index must be between 0 and {ResourceImageReader.HeroCount - 1}"
                );
            }
        }
    }
}
