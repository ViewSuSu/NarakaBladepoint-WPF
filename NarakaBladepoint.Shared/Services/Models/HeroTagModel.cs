using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using NarakaBladepoint.Framework.Core.Extensions;
using NarakaBladepoint.Resources;

namespace NarakaBladepoint.Shared.Services.Models
{
    public class HeroTagModel
    {
        /// <summary>
        /// 索引
        /// </summary>
        public int Index { get; internal set; } = -1;

        /// <summary>
        /// 英雄名字
        /// </summary>
        public string Name => Icon.GetFileName();

        /// <summary>
        /// 图片
        /// </summary>
        public ImageSource Icon => Index != -1 ? ResourceImageReader.GetHeroTagImage(Index) : null;
    }
}
