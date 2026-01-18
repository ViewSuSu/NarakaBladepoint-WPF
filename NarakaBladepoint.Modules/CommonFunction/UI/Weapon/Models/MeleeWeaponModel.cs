using System.Collections.Generic;
using System.Linq;

namespace NarakaBladepoint.Modules.CommonFunction.UI.Weapon.Models
{
    internal class MeleeWeaponModel : BindableBase
    {
        public string Group { get; set; }

        public List<WeaponItemModel> MeleeWeaponDatas { get; set; }

        public MeleeWeaponModel(List<WeaponItemModel> meleeWeaponDatas)
        {
            MeleeWeaponDatas = meleeWeaponDatas;
            Group = meleeWeaponDatas.FirstOrDefault()?.Group;
        }
    }
}
