using System;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace NarakaBladepoint.Resources
{
    public static class ResourceImageReader
    {
        private static readonly List<ImageSource> _heroImages = new();
        private static readonly List<ImageSource> _avatarImages = new();

        static ResourceImageReader()
        {
            var assembly = typeof(ResourceImageReader).Assembly;
            var resourceName = assembly.GetName().Name + ".g.resources";

            // 确保流和 ResourceReader 都能被正确释放
            using var stream = assembly.GetManifestResourceStream(resourceName);
            if (stream == null)
                return;

            using var reader = new ResourceReader(stream);

            foreach (DictionaryEntry entry in reader)
            {
                if (entry.Key is not string key)
                    continue;

                key = key.ToLowerInvariant();

                // Hero 图片，只取一级目录
                if (key.StartsWith("image/hero/") && key.EndsWith(".png"))
                {
                    var relative = key["image/hero/".Length..];
                    if (!relative.Contains("/"))
                    {
                        try
                        {
                            _heroImages.Add(LoadBitmapFromResource(assembly, key));
                        }
                        catch
                        {
                            // 加载失败可以忽略或记录日志
                        }
                    }
                }

                // Avatar 图片，只取一级目录
                if (key.StartsWith("image/avatar/") && key.EndsWith(".png"))
                {
                    var relative = key["image/avatar/".Length..];
                    if (!relative.Contains("/"))
                    {
                        try
                        {
                            _avatarImages.Add(LoadBitmapFromResource(assembly, key));
                        }
                        catch
                        {
                            // 加载失败可以忽略或记录日志
                        }
                    }
                }
            }
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
            bitmap.CacheOption = BitmapCacheOption.OnLoad; // 立即加载，避免流占用
            bitmap.EndInit();
            bitmap.Freeze(); // 冻结，提高性能并允许跨线程访问

            return bitmap;
        }

        /// <summary>
        /// 获取指定索引的英雄图片
        /// </summary>
        public static ImageSource GetHeroImage(int index) =>
            index >= 0 && index < _heroImages.Count ? _heroImages[index] : null;

        /// <summary>
        /// 获取指定索引的头像图片
        /// </summary>
        public static ImageSource GetAvatarImage(int index) =>
            index >= 0 && index < _avatarImages.Count ? _avatarImages[index] : null;

        /// <summary>
        /// 获取所有英雄图片的副本
        /// </summary>
        /// <returns>英雄图片列表的只读副本</returns>
        public static IReadOnlyList<ImageSource> GetAllHeroAvatarImageSouces()
        {
            // 返回只读列表以防止外部修改
            return _heroImages.AsReadOnly();
        }

        /// <summary>
        /// 获取所有头像图片的副本
        /// </summary>
        /// <returns>头像图片列表的只读副本</returns>
        public static IReadOnlyList<ImageSource> GetAllAvatarImages()
        {
            // 返回只读列表以防止外部修改
            return _avatarImages.AsReadOnly();
        }

        /// <summary>
        /// 英雄图片总数
        /// </summary>
        public static int HeroCount => _heroImages.Count;

        /// <summary>
        /// 头像图片总数
        /// </summary>
        public static int AvatarCount => _avatarImages.Count;
    }
}
