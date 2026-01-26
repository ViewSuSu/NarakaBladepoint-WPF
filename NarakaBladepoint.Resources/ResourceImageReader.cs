using System.Collections;
using System.Resources;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using NarakaBladepoint.Framework.Core.Extensions;

namespace NarakaBladepoint.Resources
{
    public static class ResourceImageReader
    {
        private static readonly List<ImageSource> _heroImages = new();
        private static readonly List<ImageSource> _heroShowImages = new();
        private static readonly List<ImageSource> _avatarImages = new();
        private static readonly List<ImageSource> _heroTagImages = new();

        // Weapon 相关：image/weapon/*.png
        private static readonly List<ImageSource> _weaponImages = new();

        // InventoryProps 相关：image/inventoryprops/*.png
        private static readonly List<ImageSource> _inventoryPropImages = new();

        // Weapon 子文件夹：Key = 武器名称, Value = (技能图片列表, 背景图)
        private static readonly Dictionary<
            string,
            (List<ImageSource> Skills, ImageSource Background)
        > _weaponFolderImages = new();

        // Map 相关：Key = 静态图，Value = Gif（可为空）
        private static readonly Dictionary<ImageSource, ImageSource> _mapImagePairs = new();

        // HisitoryMatchRecord 相关：image/hisitorymatchrecord/*.png (按加载顺序分配索引，从0开始)
        private static readonly Dictionary<int, ImageSource> _historyMatchRecordImages = new();

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

                // ===================== HeroShow =====================
                if (key.StartsWith("image/hero/show/") && key.EndsWith(".png"))
                {
                    var relative = key["image/hero/show/".Length..];
                    if (!relative.Contains("/"))
                    {
                        try
                        {
                            _heroShowImages.Add(LoadBitmapFromResource(assembly, key));
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

                // ===================== InventoryProps =====================
                if (key.StartsWith("image/inventoryprops/") && key.EndsWith(".png"))
                {
                    var relative = key["image/inventoryprops/".Length..];
                    if (!relative.Contains("/"))
                    {
                        // 只保留纯数字命名的文件（如 1.png, 2.png 等）
                        var fileNameWithoutExtension = relative[..^4]; // 去掉 .png
                        if (int.TryParse(fileNameWithoutExtension, out _))
                        {
                            try
                            {
                                _inventoryPropImages.Add(LoadBitmapFromResource(assembly, key));
                            }
                            catch { }
                        }
                    }
                }

                // ===================== Weapon =====================
                // image/Weapon 下表示武器图片资源（统一按小写匹配后为 image/weapon/）
                if (key.StartsWith("image/weapon/") && key.EndsWith(".png"))
                {
                    var relative = key["image/weapon/".Length..];
                    if (!relative.Contains("/"))
                    {
                        // 根目录武器图标
                        try
                        {
                            _weaponImages.Add(LoadBitmapFromResource(assembly, key));
                        }
                        catch { }
                    }
                    else
                    {
                        // 子文件夹中的武器技能和背景图
                        var parts = relative.Split('/');
                        if (parts.Length == 2)
                        {
                            try
                            {
                                var image = LoadBitmapFromResource(assembly, key);
                                // 使用扩展方法获取正确的文件名
                                var fileName = image.GetFileNameWithExtension();

                                // 从路径中获取武器文件夹名（需要从原始 URI 中获取正确大小写）
                                var uri = ((BitmapImage)image).UriSource;
                                var pathParts = uri.LocalPath.Split('/');
                                var weaponName = pathParts.Length >= 2 ? pathParts[^2] : parts[0];

                                if (!_weaponFolderImages.ContainsKey(weaponName))
                                {
                                    _weaponFolderImages[weaponName] = (
                                        new List<ImageSource>(),
                                        null
                                    );
                                }

                                if (
                                    fileName.Equals(
                                        "background.png",
                                        StringComparison.OrdinalIgnoreCase
                                    )
                                )
                                {
                                    var current = _weaponFolderImages[weaponName];
                                    _weaponFolderImages[weaponName] = (current.Skills, image);
                                }
                                else
                                {
                                    _weaponFolderImages[weaponName].Skills.Add(image);
                                }
                            }
                            catch { }
                        }
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

                // ===================== HisitoryMatchRecord =====================
                if (key.StartsWith("image/hisitorymatchrecord/") && key.EndsWith(".png"))
                {
                    var relative = key["image/hisitorymatchrecord/".Length..];
                    if (!relative.Contains("/"))
                    {
                        try
                        {
                            var index = _historyMatchRecordImages.Count;
                            _historyMatchRecordImages[index] = LoadBitmapFromResource(
                                assembly,
                                key
                            );
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

        // ===================== Map Key =====================

        private static string GetMapKeyStatic(string fileName)
        {
            if (fileName.EndsWith(".png", StringComparison.OrdinalIgnoreCase))
                fileName = fileName[..^4];
            return fileName;
        }

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
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.EndInit();
            bitmap.Freeze();

            return bitmap;
        }

        // ===================== Hero / HeroShow / Avatar / HeroTag =====================

        public static ImageSource GetHeroAvatarImage(string name)
        {
            return _heroImages.Find(img =>
            {
                var fileName = img.GetFileName();
                return string.Equals(fileName, name, StringComparison.OrdinalIgnoreCase);
            });
        }

        public static ImageSource GetHeroAvatarImage(int index)
        {
            var name = _heroShowImages[index].GetFileName();
            var heroAvatarImage = _heroImages.FirstOrDefault(x => x.GetFileName() == name);
            return heroAvatarImage;
        }

        public static ImageSource GetHeroShowImage(string name)
        {
            return _heroShowImages.Find(img =>
            {
                var fileName = img.GetFileName();
                return string.Equals(fileName, name, StringComparison.OrdinalIgnoreCase);
            });
        }

        public static ImageSource GetSocialAvatarImage(int index) =>
            index >= 0 && index < _avatarImages.Count ? _avatarImages[index] : null;

        public static ImageSource GetHeroTagImage(int index) =>
            index >= 0 && index < _heroTagImages.Count ? _heroTagImages[index] : null;

        public static IReadOnlyList<ImageSource> GetAllHeroAvatarImageSouces() =>
            _heroImages.AsReadOnly();

        public static IReadOnlyList<ImageSource> GetAllHeroShowImages() =>
            _heroShowImages.AsReadOnly();

        public static IReadOnlyList<ImageSource> GetAllAvatarImages() => _avatarImages.AsReadOnly();

        public static IReadOnlyList<ImageSource> GetAllHeroTagImages() =>
            _heroTagImages.AsReadOnly();

        public static int HeroCount => _heroImages.Count;
        public static int HeroShowCount => _heroShowImages.Count;
        public static int AvatarCount => _avatarImages.Count;
        public static int HeroTagCount => _heroTagImages.Count;

        // ===================== InventoryProps API =====================

        public static ImageSource GetInventoryPropImage(int index) =>
            index >= 0 && index < _inventoryPropImages.Count ? _inventoryPropImages[index] : null;

        public static IReadOnlyList<ImageSource> GetAllInventoryPropImages() =>
            _inventoryPropImages.AsReadOnly();

        public static int InventoryPropCount => _inventoryPropImages.Count;

        // ===================== Weapon API =====================

        public static ImageSource GetWeaponImage(string name)
        {
            return _weaponImages.Find(img =>
            {
                var fileName = img.GetFileName();
                return string.Equals(fileName, name, StringComparison.OrdinalIgnoreCase);
            });
        }

        public static IReadOnlyList<ImageSource> GetAllWeaponImages() => _weaponImages.AsReadOnly();

        public static int WeaponCount => _weaponImages.Count;

        /// <summary>
        /// 获取武器背景图
        /// </summary>
        public static ImageSource GetWeaponBackground(string weaponName)
        {
            var key = _weaponFolderImages.Keys.FirstOrDefault(k =>
                string.Equals(k, weaponName, StringComparison.OrdinalIgnoreCase)
            );
            if (key != null && _weaponFolderImages.TryGetValue(key, out var data))
            {
                return data.Background;
            }
            return null;
        }

        /// <summary>
        /// 获取武器技能图片列表
        /// </summary>
        public static List<ImageSource> GetWeaponSkillImages(string weaponName)
        {
            var key = _weaponFolderImages.Keys.FirstOrDefault(k =>
                string.Equals(k, weaponName, StringComparison.OrdinalIgnoreCase)
            );
            if (key != null && _weaponFolderImages.TryGetValue(key, out var data))
            {
                return data.Skills;
            }
            return new List<ImageSource>();
        }

        // ===================== Map API =====================

        public static IReadOnlyDictionary<ImageSource, ImageSource> GetAllMapImagePairs() =>
            _mapImagePairs;

        public static ImageSource GetMapGif(ImageSource mapImage) =>
            mapImage != null && _mapImagePairs.TryGetValue(mapImage, out var gif) ? gif : null;

        public static int MapCount => _mapImagePairs.Count;

        // ===================== HisitoryMatchRecord API =====================

        public static ImageSource GetHistoryMatchRecordImage(int index) =>
            _historyMatchRecordImages.TryGetValue(index, out var image) ? image : null;

        public static IReadOnlyDictionary<int, ImageSource> GetAllHistoryMatchRecordImages() =>
            _historyMatchRecordImages;

        public static int HistoryMatchRecordCount => _historyMatchRecordImages.Count;
    }
}
