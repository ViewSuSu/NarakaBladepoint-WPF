using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NarakaBladepoint.Resources;

namespace NarakaBladepoint.Shared.Services.Infrastructure
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
