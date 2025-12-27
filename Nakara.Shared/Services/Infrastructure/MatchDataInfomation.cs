using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nakara.Shared.Jsons;

namespace Nakara.Shared.Services.Infrastructure
{
    [Component]
    internal class MatchDataInfomation : IMatchDataInfomation
    {
        public async Task<List<MatchDataItem>> GetMatchDataItemsAsync()
        {
            return ConfigurationDataReader.Get<MatchData>().List;
        }
    }
}
