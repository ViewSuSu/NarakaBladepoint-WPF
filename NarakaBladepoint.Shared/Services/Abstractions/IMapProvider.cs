using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NarakaBladepoint.Shared.Services.Abstractions
{
    /// <summary>
    /// 地图服务接口
    /// </summary>
    public interface IMapProvider
    {
        /// <summary>
        /// 获取地图信息
        /// </summary>
        /// <returns></returns>
        Task<List<MapItemData>> GetMapItemDatasAsync();

        /// <summary>
        ///  获取选中地图数量
        /// </summary>
        /// <returns></returns>
        Task<int> GetSelectedMapCountAsync();
    }
}
