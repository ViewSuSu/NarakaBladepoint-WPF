using System.Windows.Media;

namespace NarakaBladepoint.Shared.Services.Models
{
    public class HeroAvatarModel
    {
        /// <summary>
        /// 索引
        /// </summary>
        public int Index { get; internal set; }

        /// <summary>
        /// 英雄名字
        /// </summary>
        public string Name { get; internal set; }

        /// <summary>
        /// 英雄头像
        /// </summary>
        public ImageSource Avatar { get; internal set; }
    }
}
