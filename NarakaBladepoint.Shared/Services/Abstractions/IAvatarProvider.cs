using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NarakaBladepoint.Shared.Services.Abstractions
{
    public interface IAvatarProvider
    {
        /// <summary>
        /// 获取所有英雄头像
        /// </summary>
        /// <returns></returns>
        Task<List<AvatarData>> GetAvatarsAsync();
    }
}
