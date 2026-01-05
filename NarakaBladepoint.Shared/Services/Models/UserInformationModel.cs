namespace NarakaBladepoint.Shared.Services.Models
{
    public class UserInformationModel
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 名字
        /// </summary>
        public string Name { get; set; }

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
    }
}
