using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using NarakaBladepoint.Resources;

namespace NarakaBladepoint.Shared.Datas
{
    public class MeleeWeaponData
    {
        /// <summary>
        /// 武器名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 武器等级
        /// </summary>
        public int Level { get; set; }

        public ImageSource Icon => ResourceImageReader.GetWeaponImage(Name);

        /// <summary>
        /// 总伤害
        /// </summary>
        public double TotalDamage { get; set; }

        /// <summary>
        /// 总击败
        /// </summary>
        public double TotalEliminations { get; set; }

        /// <summary>
        /// 总助攻
        /// </summary>
        public double TotalAssists { get; set; }

        /// <summary>
        /// 单场最高伤害
        /// </summary>
        public double MaxDamagePerGame { get; set; }

        /// <summary>
        /// 单场最高击败
        /// </summary>
        public double MaxEliminationsPerGame { get; set; }

        /// <summary>
        /// 总振刀
        /// </summary>
        public double TotalParries { get; set; }

        /// <summary>
        /// 组
        /// </summary>
        public string Group { get; set; }
    }
}
