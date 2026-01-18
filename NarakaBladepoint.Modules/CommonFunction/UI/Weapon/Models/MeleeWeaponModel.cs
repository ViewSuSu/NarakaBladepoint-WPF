using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NarakaBladepoint.Modules.CommonFunction.UI.Weapon.Models
{
    internal class MeleeWeaponModel : BindableBase
    {
        public string Group { get; set; }

        public List<WeaponData> MeleeWeaponDatas { get; set; }

        public MeleeWeaponModel(List<WeaponData> meleeWeaponDatas)
        {
            MeleeWeaponDatas = meleeWeaponDatas;
            Group = meleeWeaponDatas.FirstOrDefault()?.Group;
        }
    }
}
