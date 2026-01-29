using System.Windows.Media;
using NarakaBladepoint.Resources;

namespace NarakaBladepoint.Shared.Datas
{
    /// <summary>
    /// 武器数据（近战和远程通用）
    /// </summary>
    public class WeaponData
    {
        /// <summary>
        /// 武器名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 武器等级
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// 武器图标
        /// </summary>
        public ImageSource Icon => ResourceImageReader.GetWeaponImage(Name);

        /// <summary>
        /// 武器背景图
        /// </summary>
        public ImageSource Background => ResourceImageReader.GetWeaponBackground(Name);

        /// <summary>
        /// 武器技能图片列表
        /// </summary>
        public List<ImageSource> SkillImages => ResourceImageReader.GetWeaponSkillImages(Name);

        /// <summary>
        /// 总伤害
        /// </summary>
        public int TotalDamage { get; set; }

        /// <summary>
        /// 总击败
        /// </summary>
        public int TotalEliminations { get; set; }

        /// <summary>
        /// 总助攻
        /// </summary>
        public int TotalAssists { get; set; }

        /// <summary>
        /// 单场最高伤害
        /// </summary>
        public int MaxDamagePerGame { get; set; }

        /// <summary>
        /// 单场最高击败
        /// </summary>
        public int MaxEliminationsPerGame { get; set; }

        /// <summary>
        /// 组（近战武器分组用）
        /// </summary>
        public string Group { get; set; }

        /// <summary>
        /// 总振刀（近战武器专用）
        /// </summary>
        public int TotalParries { get; set; }

        /// <summary>
        /// 命中头部次数（远程武器专用）
        /// </summary>
        public int Headshots { get; set; }

        /// <summary>
        /// 最远击败距离（远程武器专用）
        /// </summary>
        public int FarthestEliminationDistance { get; set; }
    }
}
