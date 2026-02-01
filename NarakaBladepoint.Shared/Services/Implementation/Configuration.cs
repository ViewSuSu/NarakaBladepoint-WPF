namespace NarakaBladepoint.Shared.Services.Implementation
{
    [Component(ComponentLifetime.Singleton)]
    internal class Configuration : IConfiguration
    {
        public async Task<bool> SaveAsync<T>(T entity)
        {
            return ConfigurationDataReader.Save(entity);
        }

        public async Task<bool> SaveAllAsyn<T>(IEnumerable<T> entities)
        {
            return ConfigurationDataReader.SaveList(entities);
        }
    }
}
