namespace NarakaBladepoint.Shared.Services.Abstractions
{
    public interface IConfiguration
    {
        /// <summary>
        /// 保存配置类对象到本地
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> SaveAsync<T>(T entity);

        /// <summary>
        /// 保存配置类集合对象到本地
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities"></param>
        /// <returns></returns>
        Task<bool> SaveAllAsyn<T>(IEnumerable<T> entities);
    }
}
