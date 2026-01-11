using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NarakaBladepoint.Shared.Services.Infrastructure
{
    [Component(ComponentLifetime.Singleton)]
    internal class EmailItemDataProvider : IEmailItemDataProvider
    {
        public async Task<List<EmailItemData>> GetEmailItemDatasAsync()
        {
            return ConfigurationDataReader.GetList<EmailItemData>();
        }
    }
}
