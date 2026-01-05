using System.Windows.Media;
using NarakaBladepoint.Resources;

namespace NarakaBladepoint.Shared.Services.Infrastructure
{
    [Component(ComponentLifetime.Singleton)]
    internal class ImageSourceProvider : IImageSourceProvider
    {
        public async Task<IReadOnlyList<ImageSource>> GetAllHeroAvatarImageSources()
        {
            return ResourceImageReader.GetAllHeroAvatarImageSouces();
        }

        public async Task<IReadOnlyList<ImageSource>> GetCurrenUserAllAvatarImageSources()
        {
            return ResourceImageReader.GetAllAvatarImages();
        }
    }
}
