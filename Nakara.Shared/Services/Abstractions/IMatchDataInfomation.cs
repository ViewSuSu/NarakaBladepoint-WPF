using Nakara.Shared.Jsons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nakara.Shared.Services.Abstractions
{
    public interface IMatchDataInfomation
    {
        /// <summary>
        /// 获取对局记录
        /// </summary>
        /// <returns></returns>
        Task<List<MatchDataItem>> GetMatchDataItemsAsync();
    }
}
