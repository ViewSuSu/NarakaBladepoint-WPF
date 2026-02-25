using System;
using System.IO;
using System.Reflection;

namespace NarakaBladepoint.Modules.CommonFunction.UI.SkillPoint.Utilities
{
    /// <summary>
    /// 视频路径解析工具，用于获取程序集内的资源文件路径
    /// </summary>
    internal static class VideoPathResolver
    {
        /// <summary>
        /// 根据相对路径获取视频文件的完整 Uri
        /// </summary>
        /// <param name="relativePath">相对于资源目录的路径，如 "Image/SkillPoints/Gif/F1.Mp4"</param>
        /// <returns>视频文件的 Uri</returns>
        public static Uri GetVideoUri(string relativePath)
        {
            try
            {
                // 获取当前程序集的位置
                var assemblyLocation = Assembly.GetExecutingAssembly().Location;
                var assemblyDirectory = Path.GetDirectoryName(assemblyLocation);

                // 向上遍历目录结构，找到项目根目录
                // 典型结构: .../bin/Debug/net6.0/ -> 需要回到项目根
                var projectRoot = FindProjectRoot(assemblyDirectory);
                
                // 构建资源文件的完整路径
                // 假设资源在 NarakaBladepoint.Resources 项目中
                var resourcePath = Path.Combine(projectRoot, "NarakaBladepoint.Resources", relativePath);

                // 规范化路径（处理 / 和 \ 的混合使用）
                resourcePath = Path.GetFullPath(resourcePath);

                // 验证文件是否存在
                if (!File.Exists(resourcePath))
                {
                    throw new FileNotFoundException($"视频文件不存在: {resourcePath}");
                }

                // 返回 file:// Uri
                return new Uri(resourcePath, UriKind.Absolute);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"获取视频路径失败: {relativePath}", ex);
            }
        }

        /// <summary>
        /// 查找项目根目录
        /// 从程序集目录向上查找，直到找到包含 .sln 文件或 NarakaBladepoint.Resources 目录的目录
        /// </summary>
        private static string FindProjectRoot(string startPath)
        {
            var currentPath = startPath;
            var maxIterations = 10; // 防止无限循环

            for (int i = 0; i < maxIterations; i++)
            {
                // 检查是否存在 NarakaBladepoint.Resources 目录
                var resourcesPath = Path.Combine(currentPath, "NarakaBladepoint.Resources");
                if (Directory.Exists(resourcesPath))
                {
                    return currentPath;
                }

                // 检查是否存在 .sln 文件
                var slnFiles = Directory.GetFiles(currentPath, "*.sln");
                if (slnFiles.Length > 0)
                {
                    return currentPath;
                }

                // 向上一级目录
                var parentPath = Directory.GetParent(currentPath);
                if (parentPath == null)
                {
                    throw new DirectoryNotFoundException($"无法找到项目根目录，从 {startPath} 开始搜索");
                }

                currentPath = parentPath.FullName;
            }

            throw new DirectoryNotFoundException($"项目根目录搜索超时，从 {startPath} 开始搜索");
        }
    }
}
