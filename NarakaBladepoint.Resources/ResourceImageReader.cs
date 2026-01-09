using System.Collections;
using System.Resources;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace NarakaBladepoint.Resources
{
    public static class ResourceImageReader
    {
        private static readonly List<ImageSource> _heroImages = new();
        private static readonly List<ImageSource> _avatarImages = new();
        private static readonly List<ImageSource> _heroTagImages = new();

        // Map 相关：Key = 静态图，Value = Gif（可为空）
        private static readonly Dictionary<ImageSource, ImageSource> _mapImagePairs = new();

        static ResourceImageReader()
        {
            var assembly = typeof(ResourceImageReader).Assembly;
            var resourceName = assembly.GetName().Name + ".g.resources";

            using var stream = assembly.GetManifestResourceStream(resourceName);
            if (stream == null)
                return;

            using var reader = new ResourceReader(stream);

            // 临时缓存 Map / MapGif（按文件名 key）
            var mapStaticTemp = new Dictionary<string, ImageSource>();
            var mapGifTemp = new Dictionary<string, ImageSource>();

            foreach (DictionaryEntry entry in reader)
            {
                if (entry.Key is not string key)
                    continue;

                key = key.ToLowerInvariant();

                // ===================== Hero =====================
                if (key.StartsWith("image/hero/") && key.EndsWith(".png"))
                {
                    var relative = key["image/hero/".Length..];
                    if (!relative.Contains("/"))
                    {
                        try
                        {
                            _heroImages.Add(LoadBitmapFromResource(assembly, key));
                        }
                        catch { }
                    }
                }

                // ===================== Avatar =====================
                if (key.StartsWith("image/avatar/") && key.EndsWith(".png"))
                {
                    var relative = key["image/avatar/".Length..];
                    if (!relative.Contains("/"))
                    {
                        try
                        {
                            _avatarImages.Add(LoadBitmapFromResource(assembly, key));
                        }
                        catch { }
                    }
                }

                // ===================== HeroTag =====================
                if (key.StartsWith("image/personalinfodetails/herotag/") && key.EndsWith(".png"))
                {
                    var relative = key["image/personalinfodetails/herotag/".Length..];
                    if (!relative.Contains("/"))
                    {
                        try
                        {
                            _heroTagImages.Add(LoadBitmapFromResource(assembly, key));
                        }
                        catch { }
                    }
                }

                // ===================== Map 静态图 =====================
                if (key.StartsWith("image/region/startgame/map/") && key.EndsWith(".png"))
                {
                    var relative = key["image/region/startgame/map/".Length..];
                    if (!relative.Contains("/"))
                    {
                        var mapKey = GetMapKeyStatic(relative);
                        try
                        {
                            mapStaticTemp[mapKey] = LoadBitmapFromResource(assembly, key);
                        }
                        catch { }
                    }
                }

                // ===================== Map Gif =====================
                if (key.StartsWith("image/region/startgame/map/gif/") && key.EndsWith(".png"))
                {
                    var relative = key["image/region/startgame/map/gif/".Length..];
                    if (!relative.Contains("/"))
                    {
                        var mapKey = GetMapKeyGif(relative);
                        try
                        {
                            mapGifTemp[mapKey] = LoadBitmapFromResource(assembly, key);
                        }
                        catch { }
                    }
                }
            }

            // ===================== Map 最终配对 =====================
            foreach (var (mapKey, mapImage) in mapStaticTemp)
            {
                mapGifTemp.TryGetValue(mapKey, out var gifImage);
                _mapImagePairs[mapImage] = gifImage; // Gif 可以为 null
            }
        }

        // Map 静态图 key = 文件名去掉 .png
        private static string GetMapKeyStatic(string fileName)
        {
            if (fileName.EndsWith(".png", StringComparison.OrdinalIgnoreCase))
                fileName = fileName[..^4];
            return fileName;
        }

        // Map Gif key = 文件名去掉 Gif.png
        private static string GetMapKeyGif(string fileName)
        {
            if (fileName.EndsWith("gif.png", StringComparison.OrdinalIgnoreCase))
                fileName = fileName[..^7];
            return fileName;
        }

        private static BitmapImage LoadBitmapFromResource(
            System.Reflection.Assembly assembly,
            string key
        )
        {
            var uri = new Uri(
                $"pack://application:,,,/{assembly.GetName().Name};component/{key}",
                UriKind.Absolute
            );

            var bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = uri;
            bitmap.CacheOption = BitmapCacheOption.OnLoad; // 立即加载
            bitmap.EndInit();
            bitmap.Freeze(); // 冻结，提高性能

            return bitmap;
        }

        // ===================== Hero / Avatar / HeroTag =====================

        public static ImageSource GetHeroImage(int index) =>
            index >= 0 && index < _heroImages.Count ? _heroImages[index] : null;

        public static ImageSource GetAvatarImage(int index) =>
            index >= 0 && index < _avatarImages.Count ? _avatarImages[index] : null;

        public static ImageSource GetHeroTagImage(int index) =>
            index >= 0 && index < _heroTagImages.Count ? _heroTagImages[index] : null;

        public static IReadOnlyList<ImageSource> GetAllHeroAvatarImageSouces() =>
            _heroImages.AsReadOnly();

        public static IReadOnlyList<ImageSource> GetAllAvatarImages() => _avatarImages.AsReadOnly();

        public static IReadOnlyList<ImageSource> GetAllHeroTagImages() =>
            _heroTagImages.AsReadOnly();

        public static int HeroCount => _heroImages.Count;
        public static int AvatarCount => _avatarImages.Count;
        public static int HeroTagCount => _heroTagImages.Count;

        // ===================== Map API =====================

        /// <summary>
        /// 获取 Map → MapGif 配对字典
        /// </summary>
        public static IReadOnlyDictionary<ImageSource, ImageSource> GetAllMapImagePairs() =>
            _mapImagePairs;

        /// <summary>
        /// 根据 Map 静态图获取对应的 Gif（可能为 null）
        /// </summary>
        public static ImageSource GetMapGif(ImageSource mapImage) =>
            mapImage != null && _mapImagePairs.TryGetValue(mapImage, out var gif) ? gif : null;

        /// <summary>
        /// 地图总数
        /// </summary>
        public static int MapCount => _mapImagePairs.Count;
    }
}
