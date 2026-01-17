using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NarakaBladepoint.Modules.CommonFunction.Domain.Bases;
using NarakaBladepoint.Modules.CommonFunction.UI.Weapon.Models;

namespace NarakaBladepoint.Modules.CommonFunction.UI.Weapon.ViewModels
{
    internal class WeaponPageViewModel : CommonFunctionPageViewModelBase
    {
        private readonly IWeaponProvider weaponProvider;

        public List<MeleeWeaponModel> MeleeWeaponModels { get; }
        public List<RangedWeaponData> RangedWeaponModels { get; }

        public WeaponPageViewModel(
            IContainerProvider containerProvider,
            IWeaponProvider weaponProvider
        )
            : base(containerProvider)
        {
            this.weaponProvider = weaponProvider;
            this.RangedWeaponModels = weaponProvider.GetRangedWeaponDatasAsync().Result;
            List<MeleeWeaponData> meleeWeaponDatas = weaponProvider
                .GetMeleeWeaponDatasAsync()
                .Result;
            this.MeleeWeaponModels = meleeWeaponDatas
                .GroupBy(x => x.Group)
                .Select(g => new MeleeWeaponModel(g.ToList()))
                .ToList();
        }
    }
}
