namespace NarakaBladepoint.Shared.Services.Implementation
{
    [Component(ComponentLifetime.Singleton)]
    internal class MapProvider : IMapProvider
    {
        public async Task<List<MapItemData>> GetMapItemDatasAsync()
        {
            return ConfigurationDataReader.GetList<MapItemData>();
        }

        public async Task<int> GetSelectedMapCountAsync()
        {
            return ConfigurationDataReader.GetList<MapItemData>().Count(x => x.IsSelected);
        }
    }
}
