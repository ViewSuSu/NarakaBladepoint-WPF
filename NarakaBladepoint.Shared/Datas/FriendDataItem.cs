using System.Windows.Media;
using NarakaBladepoint.Resources;

namespace NarakaBladepoint.Shared.Datas
{
    public class FriendData
    {
        public List<FriendDataItem> List { get; set; }
    }

    public class FriendDataItem
    {
        /// <summary>
        /// 头像索引
        /// </summary>
        public int AvatarIndex { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public ImageSource Avatar => ResourceImageReader.GetAvatarImage(AvatarIndex);

        /// <summary>
        /// 名字
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public string Status { get; set; }
    }
}
