using System.Windows.Media;
using NarakaBladepoint.Framework.Core.Extensions;
using NarakaBladepoint.Resources;

namespace NarakaBladepoint.Shared.Services.Infrastructure
{
    [Component(ComponentLifetime.Singleton)]
    internal class HeroInfomation : IHeroInfomation
    {
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
