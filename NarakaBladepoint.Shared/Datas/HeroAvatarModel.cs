using System.Windows.Media;
using NarakaBladepoint.Resources;
using Newtonsoft.Json;

namespace NarakaBladepoint.Shared.Datas
{
    public class HeroAvatarModel
    {
        /// <summary>
        /// 索引
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// 英雄名字
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 英雄头像
        /// </summary>
        [JsonIgnore]
        public ImageSource Avatar => ResourceImageReader.GetHeroAvatarImage(Name);

        /// <summary>
        /// 英雄展示
        /// </summary>
        [JsonIgnore]
        public ImageSource ShowImage => ResourceImageReader.GetHeroShowImage(Name);

        /// <summary>
        /// 配音
        /// </summary>
        public string VoiceActor { get; set; }

        /// <summary>
        /// 短描述
        /// </summary>
        public string ShortDescription { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 英雄难度
        /// </summary>
        public double HeroAccessibilityDifficulty { get; set; }

        /// <summary>
        /// 生存
        /// </summary>
        public int Survival { get; set; }

        /// <summary>
        /// 控制
        /// </summary>
        public int Control { get; set; }

        /// <summary>
        ///  机动
        /// </summary>
        public int Mobility { get; set; }

        /// <summary>
        /// 伤害
        /// </summary>
        public int Damage { get; set; }

        /// <summary>
        /// 支援
        /// </summary>
        public int Support { get; set; }
    }
}
