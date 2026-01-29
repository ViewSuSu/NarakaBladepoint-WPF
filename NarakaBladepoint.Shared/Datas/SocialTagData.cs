using System.ComponentModel;

namespace NarakaBladepoint.Shared.Datas
{
    public enum SocialTagType
    {
        /// <summary>
        /// 战斗
        /// </summary>
        [Description("战斗")]
        Fight,

        /// <summary>
        /// 社交
        /// </summary>
        [Description("社交")]
        Social,

        /// <summary>
        /// 个性
        /// </summary>
        [Description("个性")]
        Personality,

        /// <summary>
        /// 模式
        /// </summary>
        [Description("模式")]
        Mode,

        /// <summary>
        /// 语言
        /// </summary>
        [Description("语言")]
        Language,
    }

    public class SocialTagData
    {
        public int Index { get; set; }

        public string Text { get; set; }

        public SocialTagType TagType { get; set; }
    }
}
