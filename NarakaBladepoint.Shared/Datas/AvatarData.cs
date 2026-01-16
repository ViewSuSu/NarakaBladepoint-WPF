using System.Windows.Media;
using NarakaBladepoint.Framework.Core.Extensions;
using NarakaBladepoint.Resources;

namespace NarakaBladepoint.Shared.Datas
{
    public class AvatarData
    {
        /// <summary>
        /// 头像索引
        /// </summary>
        public int Index { get; set; } = -1;

        public ImageSource ImageSource =>
            Index != -1 ? ResourceImageReader.GetSocialAvatarImage(Index) : null;

        /// <summary>
        /// 名字
        /// </summary>
        public string Name => ImageSource.GetFileName();

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 是否拥有
        /// </summary>
        public bool IsHave { get; set; }
    }
}
