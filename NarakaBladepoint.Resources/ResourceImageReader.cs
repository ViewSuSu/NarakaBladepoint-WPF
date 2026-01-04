using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace NarakaBladepoint.Shared
{
    public static class ResourceImageReader
    {
        private static readonly List<ImageSource> _heroImages = new List<ImageSource>();

        static ResourceImageReader()
        {
            var assemblyLocation = typeof(ResourceImageReader).Assembly.Location;
            var basePath = Path.GetDirectoryName(assemblyLocation);
            var heroFolderPath = Path.Combine(basePath, "Image", "Hero");

            if (!Directory.Exists(heroFolderPath))
                return;

            var imageFiles = Directory.GetFiles(heroFolderPath, "*.png");

            foreach (var imagePath in imageFiles)
            {
                var bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(imagePath, UriKind.Absolute);
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.EndInit();
                bitmap.Freeze();
                _heroImages.Add(bitmap);
            }
        }

        /// <summary>
        /// 读取英雄图片
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public static ImageSource GetHeroImage(int index)
        {
            if (index >= 0 && index < _heroImages.Count)
            {
                return _heroImages[index];
            }
            return null;
        }
    }
}
