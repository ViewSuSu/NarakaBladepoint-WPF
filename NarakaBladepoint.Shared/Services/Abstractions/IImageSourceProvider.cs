using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace NarakaBladepoint.Shared.Services.Abstractions
{
    public interface IImageSourceProvider
    {
        /// <summary>
        /// 获取所有英雄头像ImageSource
        /// </summary>
        /// <returns></returns>
        Task<IReadOnlyList<ImageSource>> GetAllHeroAvatarImageSources();

        /// <summary>
        /// 获取当前用户所有头像
        /// </summary>
        /// <returns></returns>
        Task<IReadOnlyList<ImageSource>> GetCurrenUserAllAvatarImageSources();
    }
}
