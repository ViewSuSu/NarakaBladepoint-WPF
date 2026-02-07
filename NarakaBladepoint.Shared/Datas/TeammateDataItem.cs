using System.Windows.Media;
using NarakaBladepoint.Resources;

namespace NarakaBladepoint.Shared.Datas
{
    public class TeammateDataItem
    {
        private int _avatarIndex;

        /// <summary>
        /// 头像索引
        /// </summary>
        public int AvatarIndex
        {
            get { return _avatarIndex; }
            set
            {
                _avatarIndex = value;
                // 确保索引在有效范围内
                if (_avatarIndex < 0 || _avatarIndex >= ResourceImageReader.AvatarCount)
                {
                    _avatarIndex = 0;
                }
            }
        }

        /// <summary>
        /// 头像
        /// </summary>
        public ImageSource Avatar => ResourceImageReader.GetSocialAvatarImage(AvatarIndex);

        /// <summary>
        /// 名字
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 描述/职业
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 标签列表
        /// </summary>
        public List<string> Tags { get; set; } = new();
    }
}
