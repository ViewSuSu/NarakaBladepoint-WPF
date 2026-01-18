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
        public List<WeaponData> RangedWeaponModels { get; }

        private WeaponData _selectedWeapon;
        public WeaponData SelectedWeapon
        {
            get => _selectedWeapon;
            set
            {
                _selectedWeapon = value;
                RaisePropertyChanged();
            }
        }

        public DelegateCommand<WeaponData> SelectWeaponCommand { get; }

        public WeaponPageViewModel(
            IContainerProvider containerProvider,
            IWeaponProvider weaponProvider
        )
            : base(containerProvider)
        {
            this.weaponProvider = weaponProvider;
            this.RangedWeaponModels = weaponProvider.GetRangedWeaponDatasAsync().Result;
            List<WeaponData> meleeWeaponDatas = weaponProvider
                .GetMeleeWeaponDatasAsync()
                .Result;
            this.MeleeWeaponModels = meleeWeaponDatas
                .GroupBy(x => x.Group)
                .Select(g => new MeleeWeaponModel(g.ToList()))
                .ToList();

            // 默认选中第一个远程武器
            if (RangedWeaponModels.Count > 0)
            {
                SelectedWeapon = RangedWeaponModels[0];
            }

            SelectWeaponCommand = new DelegateCommand<WeaponData>(weapon =>
            {
                if (weapon != null)
                {
                    SelectedWeapon = weapon;
                }
            });
        }
    }
}
