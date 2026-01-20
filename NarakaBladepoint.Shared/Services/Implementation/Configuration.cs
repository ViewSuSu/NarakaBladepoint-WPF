namespace NarakaBladepoint.Shared.Services.Implementation
{
    [Component(ComponentLifetime.Singleton)]
    internal class Configuration : IConfiguration
    {
        public async Task<bool> Save<T>(T entity)
        {
            return ConfigurationDataReader.Save(entity);
        }

        public async Task<bool> SaveAll<T>(IEnumerable<T> entities)
        {
            return ConfigurationDataReader.SaveList(entities);
        }
    }
}
