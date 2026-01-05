using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace NarakaBladepoint.Framework.Core.Extensions
{
    public static class ImageSourceExtensions
    {
        /// <summary>
        /// 从 ImageSource 中提取文件名（不带扩展名）
        /// </summary>
        /// <param name="imageSource">图像源</param>
        /// <returns>文件名（不带扩展名），如果无法获取则返回 null</returns>
        public static string GetFileName(this ImageSource imageSource)
        {
            if (imageSource == null)
                return null;

            var uri = GetUriFromImageSource(imageSource);
            if (uri != null)
            {
                return Path.GetFileNameWithoutExtension(uri.LocalPath);
            }

            return null;
        }

        /// <summary>
        /// 从 ImageSource 中提取带扩展名的完整文件名
        /// </summary>
        /// <param name="imageSource">图像源</param>
        /// <returns>带扩展名的文件名，如果无法获取则返回 null</returns>
        public static string GetFileNameWithExtension(this ImageSource imageSource)
        {
            if (imageSource == null)
                return null;

            var uri = GetUriFromImageSource(imageSource);
            if (uri != null)
            {
                return Path.GetFileName(uri.LocalPath);
            }

            return null;
        }

        /// <summary>
        /// 从 ImageSource 中提取文件扩展名
        /// </summary>
        /// <param name="imageSource">图像源</param>
        /// <returns>文件扩展名（不含点号），如果无法获取则返回 null</returns>
        public static string GetFileExtension(this ImageSource imageSource)
        {
            if (imageSource == null)
                return null;

            var uri = GetUriFromImageSource(imageSource);
            if (uri != null)
            {
                return Path.GetExtension(uri.LocalPath)?.TrimStart('.');
            }

            return null;
        }

        /// <summary>
        /// 获取 ImageSource 的完整 URI 路径
        /// </summary>
        /// <param name="imageSource">图像源</param>
        /// <returns>URI 字符串，如果无法获取则返回 null</returns>
        public static string GetUriPath(this ImageSource imageSource)
        {
            if (imageSource == null)
                return null;

            var uri = GetUriFromImageSource(imageSource);
            if (uri != null)
            {
                return uri.ToString();
            }

            // 返回 ToString() 结果（某些情况下就是 URI）
            return imageSource.ToString();
        }

        /// <summary>
        /// 从 ImageSource 中提取 URI
        /// </summary>
        private static Uri GetUriFromImageSource(ImageSource imageSource)
        {
            if (imageSource == null)
                return null;

            // 处理 BitmapImage
            if (imageSource is BitmapImage bitmapImage)
            {
                return bitmapImage.UriSource;
            }

            // 处理 BitmapFrame - 正确获取方式
            if (imageSource is BitmapFrame bitmapFrame)
            {
                // BitmapFrame 的 Uri 属性获取方式
                return bitmapFrame.BaseUri;
            }

            // 对于其他类型的 ImageSource，尝试从 ToString() 解析
            var uriString = imageSource.ToString();
            if (Uri.TryCreate(uriString, UriKind.Absolute, out Uri uri))
            {
                return uri;
            }

            // 尝试移除可能的 "System.Windows.Media." 前缀
            if (uriString.StartsWith("System.Windows.Media."))
            {
                var cleaned = uriString.Replace("System.Windows.Media.", "");
                if (Uri.TryCreate(cleaned, UriKind.Absolute, out Uri cleanedUri))
                {
                    return cleanedUri;
                }
            }

            return null;
        }

        /// <summary>
        /// 检查图片是否来自指定的资源路径
        /// </summary>
        /// <param name="imageSource">图像源</param>
        /// <param name="resourcePath">资源路径，如 "image/hero/"</param>
        /// <returns>如果图片来自指定路径则返回 true</returns>
        public static bool IsFromResourcePath(this ImageSource imageSource, string resourcePath)
        {
            var uriPath = imageSource.GetUriPath();
            if (uriPath == null)
                return false;

            return uriPath.Contains(resourcePath, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// 获取资源的相对路径
        /// </summary>
        /// <param name="imageSource">图像源</param>
        /// <returns>资源的相对路径，如 "image/hero/hero1.png"</returns>
        public static string GetResourceRelativePath(this ImageSource imageSource)
        {
            var uri = GetUriFromImageSource(imageSource);
            if (uri == null)
                return null;

            // 处理 pack:// URI
            if (uri.IsAbsoluteUri && uri.Scheme == "pack")
            {
                // pack://application:,,,/AssemblyName;component/image/hero/hero1.png
                var uriString = uri.ToString();

                // 查找 "component/" 之后的部分
                var componentIndex = uriString.IndexOf(
                    "component/",
                    StringComparison.OrdinalIgnoreCase
                );
                if (componentIndex > 0)
                {
                    return uriString.Substring(componentIndex + "component/".Length);
                }

                // 如果没找到 component/，尝试其他格式
                var segments = uri.Segments;
                if (segments.Length > 0)
                {
                    // 取最后一个 segment 作为文件名
                    return Path.GetFileName(uri.LocalPath);
                }
            }

            // 对于本地路径
            if (uri.IsFile)
            {
                return Path.GetFileName(uri.LocalPath);
            }

            return null;
        }
    }
}
