using Mapster;

namespace NarakaBladepoint.Framework.Core.Extensions
{
    public static class ConverterExtensions
    {
        /// <summary>
        /// 将对象转换为目标类型
        /// </summary>
        public static TDestination ConvertTo<TSource, TDestination>(this TSource source)
        {
            if (source == null)
                return default!;
            return source.Adapt<TDestination>();
        }

        /// <summary>
        /// 将列表转换为目标类型列表
        /// </summary>
        public static List<TDestination> ConvertToList<TDestination>(
            this IEnumerable<object> sourceList
        )
        {
            if (sourceList == null)
                return new List<TDestination>();
            return sourceList.Adapt<List<TDestination>>();
        }
    }
}
