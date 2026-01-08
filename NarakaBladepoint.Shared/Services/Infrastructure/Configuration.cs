using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NarakaBladepoint.Shared.Datas;

namespace NarakaBladepoint.Shared.Services.Infrastructure
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
