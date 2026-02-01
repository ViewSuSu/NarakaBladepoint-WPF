using System.Windows.Media;
using NarakaBladepoint.Resources;
using Newtonsoft.Json;

namespace NarakaBladepoint.Shared.Datas
{
    public class UserInformationData
    {
        /// <summary>
        /// 通行证名字
        /// </summary>
        public string BattlePassName { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 头像索引
        /// </summary>
        public int AvatarIndex { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        [JsonIgnore]
        public ImageSource Avatar => ResourceImageReader.GetSocialAvatarImage(AvatarIndex);

        /// <summary>
        /// 名字
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 金砖
        /// </summary>
        public double GoldBrick { get; set; }

        /// <summary>
        /// 古币
        /// </summary>
        public double AncientCoins { get; set; }

        /// <summary>
        /// 幻丝
        /// </summary>
        public double HuanSi { get; set; }

        /// <summary>
        /// 等级
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// 经验值
        /// </summary>
        public int Exp { get; set; }

        /// <summary>
        /// 信誉分
        /// </summary>

        public int Credits { get; set; }

        ///<summary>
        ///活跃值
        ///</summary>
        public int ActiveValue { get; set; }

        ///<summary>
        ///总收藏值
        ///</summary>
        public int TotalFavorites { get; set; }

        /// <summary>
        /// 登录天数
        /// </summary>
        public int LoginDays { get; set; }

        /// <summary>
        /// 武器外观数量
        /// </summary>
        public int WeaponSkins { get; set; }

        /// <summary>
        /// 英雄外观数量
        /// </summary>
        public int HeroSkins { get; set; }

        /// <summary>
        /// 通行证等级
        /// </summary>
        public int BattlePassLevel { get; set; }

        /// <summary>
        /// 预选首选英雄
        /// </summary>
        public int FirstPickHeroIndex { get; set; }

        /// <summary>
        /// 预选次选英雄1
        /// </summary>
        public int SecondPickHeroIndex { get; set; }

        /// <summary>
        /// 预选次选英雄2
        /// </summary>
        public int ThridPickHeroIndex { get; set; }

        /// <summary>
        /// 超时默认全选地图
        /// </summary>
        public bool TimeoutDefaultAllMap { get; set; }

        /// <summary>
        /// 选择的英雄印列表
        /// </summary>
        public int[] SelectedHeroTags { get; set; }

        /// <summary>
        /// 选择的社交标签
        /// </summary>
        public int[] SelectedSocialTags { get; set; }

        /// <summary>
        /// 麦克风社交标签
        /// </summary>
        public int? SelectedSocialTagMic { get; set; }

        /// <summary>
        /// 在线时间标签
        /// </summary>
        public int? SelectedSocialTagOnline { get; set; }

        /// <summary>
        /// 是否有任何的标签
        /// </summary>
        public bool IsExsitAnyValidSocialTag =>
            SelectedSocialTags.Any()
            || SelectedSocialTagMic.HasValue
            || SelectedSocialTagOnline.HasValue;

        /// <summary>
        /// 性别 0:未设置 1:男 2:女
        /// </summary>
        public int Gender { get; set; }

        /// <summary>
        /// 是否隐藏性别
        /// </summary>
        public bool IsHideGender { get; set; }
    }
}
