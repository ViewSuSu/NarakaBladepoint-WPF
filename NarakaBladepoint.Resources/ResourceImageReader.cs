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
        
        // PersonalInfoDetails Achievements: image/personalinfodetails/achievements/images/*.png
        private static readonly List<ImageSource> _personalInfoAchievementImages = new();
        // IllustratedCollection images: root images and foldered images (e.g. 套装/图鉴子集)
        private static readonly List<ImageSource> _illustratedCollectionRootImages = new();
        private static readonly Dictionary<string, List<ImageSource>> _illustratedCollectionFolderImages = new();

        // TimeLimitedEvent Reward Images: image/region/eventcenter/timelimitedevent/images/*.png
        private static readonly List<ImageSource> _timeLimitedEventRewardImages = new();

        // TimeLimitedEvent Images2: image/region/eventcenter/timelimitedevent/images2/*.png
        private static readonly List<ImageSource> _timeLimitedEventImages2 = new();

        // TimeLimitedEvent Images3: image/region/eventcenter/timelimitedevent/images3/*.png
        private static readonly List<ImageSource> _timeLimitedEventImages3 = new();

        // Store 相关：image/store/overview/*.png
        private static readonly List<ImageSource> _storeOverviewImages = new();
        // Store Daily 道具：image/store/daily/prop/*.png
        private static readonly List<ImageSource> _storeDailyPropImages = new();
        // Store Daily 幻丝：image/store/daily/huansi/*.png
        private static readonly List<ImageSource> _storeDailyHuanSiImages = new();
        // Store Daily 赠礼：image/store/daily/gift/*.png
        private static readonly List<ImageSource> _storeDailyGiftImages = new();
        
        // Store HeroTag Images: Key = Tag Index (1-6), Value = List of images in that tag folder
        private static readonly Dictionary<int, List<ImageSource>> _storeHeroTagImages = new();

        // Tournament Champion Images: image/personalinfodetails/leaderboard/champions/*.png
        private static readonly List<ImageSource> _tournamentChampionImages = new();

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
                // ===================== PersonalInfoDetails Achievements Images =====================
                if (key.StartsWith("image/personalinfodetails/achievements/images/") && key.EndsWith(".png"))
                {
                    var relative = key["image/personalinfodetails/achievements/images/".Length..];
                    if (!relative.Contains("/"))
                    {
                        try
                        {
                            _personalInfoAchievementImages.Add(LoadBitmapFromResource(assembly, key));
                        }
                        catch { }
                    }
                }

                // ===================== PersonalInfoDetails IllustratedCollection Images =====================
                // Support both images directly under IllustratedCollection and images organized in subfolders
                if (key.StartsWith("image/personalinfodetails/illustratedcollection/images/") && key.EndsWith(".png"))
                {
                    var relative = key["image/personalinfodetails/illustratedcollection/images/".Length..];
                    try
                    {
                        var image = LoadBitmapFromResource(assembly, key);
                        if (!relative.Contains("/"))
                        {
                            _illustratedCollectionRootImages.Add(image);
                        }
                        else
                        {
                            var parts = relative.Split('/');
                            var folder = parts[0];
                            if (!_illustratedCollectionFolderImages.ContainsKey(folder))
                                _illustratedCollectionFolderImages[folder] = new List<ImageSource>();
                            _illustratedCollectionFolderImages[folder].Add(image);
                        }
                    }
                    catch { }
                }

                // ===================== Store Overview Images =====================
                if (key.StartsWith("image/store/overview/") && key.EndsWith(".png"))
                {
                    var relative = key["image/store/overview/".Length..];
                    if (!relative.Contains("/"))
                    {
                        try
                        {
                            _storeOverviewImages.Add(LoadBitmapFromResource(assembly, key));
                        }
                        catch { }
                    }
                }

                // ===================== Store Daily Prop Images =====================
                if (key.StartsWith("image/store/daily/prop/") && key.EndsWith(".png"))
                {
                    var relative = key["image/store/daily/prop/".Length..];
                    if (!relative.Contains("/"))
                    {
                        try
                        {
                            _storeDailyPropImages.Add(LoadBitmapFromResource(assembly, key));
                        }
                        catch { }
                    }
                }

                // ===================== Store Daily HuanSi Images =====================
                if (key.StartsWith("image/store/daily/huansi/") && key.EndsWith(".png"))
                {
                    var relative = key["image/store/daily/huansi/".Length..];
                    if (!relative.Contains("/"))
                    {
                        try
                        {
                            _storeDailyHuanSiImages.Add(LoadBitmapFromResource(assembly, key));
                        }
                        catch { }
                    }
                }

                // ===================== Store Daily Gift Images =====================
                if (key.StartsWith("image/store/daily/gift/") && key.EndsWith(".png"))
                {
                    var relative = key["image/store/daily/gift/".Length..];
                    if (!relative.Contains("/"))
                    {
                        try
                        {
                            _storeDailyGiftImages.Add(LoadBitmapFromResource(assembly, key));
                        }
                        catch { }
                    }
                }

                // ===================== Store HeroTag Images =====================
                if (key.StartsWith("image/store/herotag/") && key.EndsWith(".png"))
                {
                    var relative = key["image/store/herotag/".Length..];
                    if (relative.Contains("/"))
                    {
                        var parts = relative.Split('/');
                        if (parts.Length == 2 && int.TryParse(parts[0], out var tagIndex))
                        {
                            try
                            {
                                if (!_storeHeroTagImages.ContainsKey(tagIndex))
                                {
                                    _storeHeroTagImages[tagIndex] = new List<ImageSource>();
                                }
                                _storeHeroTagImages[tagIndex].Add(LoadBitmapFromResource(assembly, key));
                            }
                            catch { }
                        }
                    }
                }

                // ===================== Tournament Champion Images =====================
                if (key.StartsWith("image/personalinfodetails/leaderboard/champions/") && key.EndsWith(".png"))
                {
                    var relative = key["image/personalinfodetails/leaderboard/champions/".Length..];
                    if (!relative.Contains("/"))
                    {
                        try
                        {
                            _tournamentChampionImages.Add(LoadBitmapFromResource(assembly, key));
                        }
                        catch { }
                    }
                }

                // ===================== TimeLimitedEvent Reward Images =====================
                if (key.StartsWith("image/region/eventcenter/timelimitedevent/images/") && key.EndsWith(".png"))
                {
                    var relative = key["image/region/eventcenter/timelimitedevent/images/".Length..];
                    if (!relative.Contains("/"))
                    {
                        try
                        {
                            _timeLimitedEventRewardImages.Add(LoadBitmapFromResource(assembly, key));
                        }
                        catch { }
                    }
                }

                // ===================== TimeLimitedEvent Images2 =====================
                if (key.StartsWith("image/region/eventcenter/timelimitedevent/images2/") && key.EndsWith(".png"))
                {
                    var relative = key["image/region/eventcenter/timelimitedevent/images2/".Length..];
                    if (!relative.Contains("/"))
                    {
                        try
                        {
                            _timeLimitedEventImages2.Add(LoadBitmapFromResource(assembly, key));
                        }
                        catch { }
                    }
                }

                // ===================== TimeLimitedEvent Images3 =====================
                if (key.StartsWith("image/region/eventcenter/timelimitedevent/images3/") && key.EndsWith(".png"))
                {
                    var relative = key["image/region/eventcenter/timelimitedevent/images3/".Length..];
                    if (!relative.Contains("/"))
                    {
                        try
                        {
                            _timeLimitedEventImages3.Add(LoadBitmapFromResource(assembly, key));
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

        // ===================== PersonalInfoDetails Achievements API =====================

        public static ImageSource GetPersonalInfoAchievementImage(int index) =>
            index >= 0 && index < _personalInfoAchievementImages.Count ? _personalInfoAchievementImages[index] : null;

        public static IReadOnlyList<ImageSource> GetAllPersonalInfoAchievementImages() => _personalInfoAchievementImages.AsReadOnly();

        public static int PersonalInfoAchievementCount => _personalInfoAchievementImages.Count;

        // ===================== IllustratedCollection API =====================

        public static ImageSource GetIllustratedCollectionRootImage(int index) =>
            index >= 0 && index < _illustratedCollectionRootImages.Count ? _illustratedCollectionRootImages[index] : null;

        public static IReadOnlyList<ImageSource> GetAllIllustratedCollectionRootImages() => _illustratedCollectionRootImages.AsReadOnly();

        public static IReadOnlyDictionary<string, List<ImageSource>> GetAllIllustratedCollectionFolderImages() => _illustratedCollectionFolderImages;

        // 直接使用下面的独立资源加载属性来获取 IllustratedCollection 的 3.png/4.png/5.png

        // 直接从资源路径独立加载指定文件（位于 Image/PersonalInfoDetails/IllustratedCollection）
        private static ImageSource TryLoadImageByResourceKey(string resourceKey)
        {
            if (string.IsNullOrWhiteSpace(resourceKey))
                return null;
            try
            {
                return LoadBitmapFromResource(typeof(ResourceImageReader).Assembly, resourceKey);
            }
            catch
            {
                return null;
            }
        }

        // 独立属性：直接读取 Image/PersonalInfoDetails/IllustratedCollection 下的 3.png/4.png/5.png
        public static ImageSource IllustratedCollectionImage_3 => TryLoadImageByResourceKey("image/personalinfodetails/illustratedcollection/3.png");
        public static ImageSource IllustratedCollectionImage_4 => TryLoadImageByResourceKey("image/personalinfodetails/illustratedcollection/4.png");
        public static ImageSource IllustratedCollectionImage_5 => TryLoadImageByResourceKey("image/personalinfodetails/illustratedcollection/5.png");

        // 兼容旧属性名，保持对现有代码的引用不变
        public static ImageSource IllustratedCollectionRootImage3 => IllustratedCollectionImage_3;
        public static ImageSource IllustratedCollectionRootImage4 => IllustratedCollectionImage_4;
        public static ImageSource IllustratedCollectionRootImage5 => IllustratedCollectionImage_5;

        public static List<ImageSource> GetIllustratedCollectionImagesByFolder(string folderName)
        {
            if (string.IsNullOrWhiteSpace(folderName))
                return new List<ImageSource>();
            var key = _illustratedCollectionFolderImages.Keys.FirstOrDefault(k => string.Equals(k, folderName, StringComparison.OrdinalIgnoreCase));
            if (key != null && _illustratedCollectionFolderImages.TryGetValue(key, out var list))
                return list;
            return new List<ImageSource>();
        }

        // ===================== Store Overview API =====================

        public static ImageSource GetStoreOverviewImage(int index) =>
            index >= 0 && index < _storeOverviewImages.Count ? _storeOverviewImages[index] : null;

        public static IReadOnlyList<ImageSource> GetAllStoreOverviewImages() =>
            _storeOverviewImages.AsReadOnly();

        public static int StoreOverviewCount => _storeOverviewImages.Count;

        // ===================== Store Daily Prop API =====================

        public static ImageSource GetStoreDailyPropImage(int index) =>
            index >= 0 && index < _storeDailyPropImages.Count ? _storeDailyPropImages[index] : null;

        public static IReadOnlyList<ImageSource> GetAllStoreDailyPropImages() =>
            _storeDailyPropImages.AsReadOnly();

        public static int StoreDailyPropCount => _storeDailyPropImages.Count;

        // ===================== Store Daily HuanSi API =====================

        public static ImageSource GetStoreDailyHuanSiImage(int index) =>
            index >= 0 && index < _storeDailyHuanSiImages.Count ? _storeDailyHuanSiImages[index] : null;

        public static IReadOnlyList<ImageSource> GetAllStoreDailyHuanSiImages() =>
            _storeDailyHuanSiImages.AsReadOnly();

        public static int StoreDailyHuanSiCount => _storeDailyHuanSiImages.Count;

        // ===================== Store Daily Gift API =====================

        public static ImageSource GetStoreDailyGiftImage(int index) =>
            index >= 0 && index < _storeDailyGiftImages.Count ? _storeDailyGiftImages[index] : null;

        public static IReadOnlyList<ImageSource> GetAllStoreDailyGiftImages() =>
            _storeDailyGiftImages.AsReadOnly();

        public static int StoreDailyGiftCount => _storeDailyGiftImages.Count;

        // ===================== Store HeroTag API =====================

        /// <summary>
        /// 获取指定标签索引的所有英雄印图片（标签索引 1-6）
        /// </summary>
        public static IReadOnlyList<ImageSource> GetStoreHeroTagImages(int tagIndex) =>
            _storeHeroTagImages.TryGetValue(tagIndex, out var images) ? images.AsReadOnly() : new List<ImageSource>().AsReadOnly();

        /// <summary>
        /// 获取所有英雄印标签的图片集合
        /// </summary>
        public static IReadOnlyDictionary<int, List<ImageSource>> GetAllStoreHeroTagImages() =>
            _storeHeroTagImages;

        /// <summary>
        /// 获取英雄印标签的个数
        /// </summary>
        public static int StoreHeroTagCount => _storeHeroTagImages.Count;

        // ===================== Tournament Champion API =====================

        /// <summary>
        /// 获取所有冠军图片列表
        /// </summary>
        public static IReadOnlyList<ImageSource> GetAllTournamentChampionImages() =>
            _tournamentChampionImages.AsReadOnly();

        /// <summary>
        /// 获取冠军图片个数
        /// </summary>
        public static int TournamentChampionCount => _tournamentChampionImages.Count;

        /// <summary>
        /// 获取时间限制事件奖励图片（按文件名数字顺序 1.png, 2.png, ...）
        /// </summary>
        public static ImageSource GetTimeLimitedEventRewardImage(int index) =>
            index >= 0 && index < _timeLimitedEventRewardImages.Count ? _timeLimitedEventRewardImages[index] : null;

        /// <summary>
        /// 获取所有时间限制事件奖励图片列表
        /// </summary>
        public static IReadOnlyList<ImageSource> GetAllTimeLimitedEventRewardImages() =>
            _timeLimitedEventRewardImages.AsReadOnly();

        /// <summary>
        /// 获取时间限制事件奖励图片总数
        /// </summary>
        public static int TimeLimitedEventRewardCount => _timeLimitedEventRewardImages.Count;

        // ===================== TimeLimitedEvent Images2 API =====================

        /// <summary>
        /// 获取时间限制事件 Images2 图片（按文件名数字顺序 1.png, 2.png, ...）
        /// </summary>
        public static ImageSource GetTimeLimitedEventImages2(int index) =>
            index >= 0 && index < _timeLimitedEventImages2.Count ? _timeLimitedEventImages2[index] : null;

        /// <summary>
        /// 获取所有时间限制事件 Images2 图片列表
        /// </summary>
        public static IReadOnlyList<ImageSource> GetAllTimeLimitedEventImages2() =>
            _timeLimitedEventImages2.AsReadOnly();

        /// <summary>
        /// 获取时间限制事件 Images2 图片总数
        /// </summary>
        public static int TimeLimitedEventImages2Count => _timeLimitedEventImages2.Count;

        // ===================== TimeLimitedEvent Images3 API =====================

        /// <summary>
        /// 获取时间限制事件 Images3 图片（按文件名数字顺序 1.png, 2.png, ...）
        /// </summary>
        public static ImageSource GetTimeLimitedEventImages3(int index) =>
            index >= 0 && index < _timeLimitedEventImages3.Count ? _timeLimitedEventImages3[index] : null;

        /// <summary>
        /// 获取所有时间限制事件 Images3 图片列表
        /// </summary>
        public static IReadOnlyList<ImageSource> GetAllTimeLimitedEventImages3() =>
            _timeLimitedEventImages3.AsReadOnly();

        /// <summary>
        /// 获取时间限制事件 Images3 图片总数
        /// </summary>
        public static int TimeLimitedEventImages3Count => _timeLimitedEventImages3.Count;
    }
}


