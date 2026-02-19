using System.Collections.ObjectModel;
using System.Windows.Media;
using NarakaBladepoint.Modules.CommonFunction.Domain.Bases;
using NarakaBladepoint.Resources;

namespace NarakaBladepoint.Modules.CommonFunction.UI.GuildHall.ViewModels
{
    /// <summary>
    /// 公会信息数据模型
    /// </summary>
    public class GuildInfo
    {
        /// <summary>
        /// 等级
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// 聚义厅名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 帮主
        /// </summary>
        public string Master { get; set; }

        /// <summary>
        /// 人数 (当前/最大)
        /// </summary>
        public string MemberCount { get; set; }

        /// <summary>
        /// 好友数量
        /// </summary>
        public int FriendCount { get; set; }

        /// <summary>
        /// 限制
        /// </summary>
        public string Restriction { get; set; }
    }

    internal class GuildHallPageViewModel : CommonFunctionPageViewModelBase
    {
        private ImageSource _guildMasterAvatar;
        private ObservableCollection<GuildInfo> _recommendedGuilds;
        private ObservableCollection<GuildInfo> _friendGuilds;

        public ImageSource GuildMasterAvatar
        {
            get { return _guildMasterAvatar; }
            set { SetProperty(ref _guildMasterAvatar, value); }
        }

        public ObservableCollection<GuildInfo> RecommendedGuilds
        {
            get { return _recommendedGuilds; }
            set { SetProperty(ref _recommendedGuilds, value); }
        }

        public ObservableCollection<GuildInfo> FriendGuilds
        {
            get { return _friendGuilds; }
            set { SetProperty(ref _friendGuilds, value); }
        }

        public GuildHallPageViewModel()
        {
            LoadRandomAvatar();
            InitializeGuildData();
        }

        private void LoadRandomAvatar()
        {
            var random = new Random();
            var avatarCount = ResourceImageReader.AvatarCount;
            if (avatarCount > 0)
            {
                int randomIndex = random.Next(avatarCount);
                GuildMasterAvatar = ResourceImageReader.GetSocialAvatarImage(randomIndex);
            }
        }

        private void InitializeGuildData()
        {
            RecommendedGuilds = new ObservableCollection<GuildInfo>
            {
                new GuildInfo { Level = 20, Name = "衣褶飞舞叶", Master = "臼别忧", MemberCount = "34/48", Restriction = "需申请" },
                new GuildInfo { Level = 20, Name = "水莱兰亭·Pve", Master = "冠道、", MemberCount = "42/48", Restriction = "需申请" },
                new GuildInfo { Level = 20, Name = "习惯性娱乐一下", Master = "感觉不如萌怀", MemberCount = "42/48", Restriction = "需申请" },
                new GuildInfo { Level = 19, Name = "你好棒啊", Master = "脱尤瓶", MemberCount = "7/45", Restriction = "需申请" },
                new GuildInfo { Level = 6, Name = "我菜我也难过啊", Master = "邪孽摇粒绳", MemberCount = "1/27", Restriction = "需申请" },
                new GuildInfo { Level = 1, Name = "未违的告白", Master = "川渔暴龙装棉羊", MemberCount = "1/15", Restriction = "无需申请" }
            };

            FriendGuilds = new ObservableCollection<GuildInfo>
            {
                new GuildInfo { Level = 20, Name = "项流AA", Master = "晚春", MemberCount = "46/48", FriendCount = 1, Restriction = "无需申请" },
                new GuildInfo { Level = 16, Name = "捉蓝只会硕士化", Master = "DJ\\夜色", MemberCount = "12/42", FriendCount = 1, Restriction = "无需申请" },
                new GuildInfo { Level = 16, Name = "军师营", Master = "编阳飘三哥", MemberCount = "18/42", FriendCount = 1, Restriction = "无需申请" }
            };
        }
    }
}
