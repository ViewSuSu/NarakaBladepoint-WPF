using System.Windows.Media.Imaging;
using NarakaBladepoint.Framework.Core.Bases.ViewModels;
using NarakaBladepoint.Framework.Core.Infrastructure;
using NarakaBladepoint.Resources;

namespace NarakaBladepoint.Modules.EventCenter.UI.MoonGazingPavilion.ViewModels
{
    internal class MoonGazingPavilionPageViewModel : CanRemoveMainContentRegionViewModelBase
    {
        private IEnumerable<BitmapImage> _moonGazingPavilionImages;

        public IEnumerable<BitmapImage> MoonGazingPavilionImages
        {
            get => _moonGazingPavilionImages;
            set => SetProperty(ref _moonGazingPavilionImages, value);
        }

        public MoonGazingPavilionPageViewModel()
        {
            LoadMoonGazingPavilionImages();
        }

        private void LoadMoonGazingPavilionImages()
        {
            var images = ResourceImageReader.GetAllMoonGazingPavilionImages();
            MoonGazingPavilionImages = images.Cast<BitmapImage>().ToList();
        }
    }
}
