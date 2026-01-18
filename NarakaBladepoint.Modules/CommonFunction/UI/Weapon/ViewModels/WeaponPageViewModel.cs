using System.Collections.Generic;
using System.Linq;
using NarakaBladepoint.Modules.CommonFunction.Domain.Bases;
using NarakaBladepoint.Modules.CommonFunction.UI.Weapon.Models;

namespace NarakaBladepoint.Modules.CommonFunction.UI.Weapon.ViewModels
{
    internal class WeaponPageViewModel : CommonFunctionPageViewModelBase
    {
        private readonly IWeaponProvider weaponProvider;

        public List<MeleeWeaponModel> MeleeWeaponModels { get; }
        public List<WeaponItemModel> RangedWeaponModels { get; }

        private WeaponItemModel _selectedWeapon;
        public WeaponItemModel SelectedWeapon
        {
            get => _selectedWeapon;
            set
            {
                if (_selectedWeapon != null)
                    _selectedWeapon.IsSelected = false;
                SetProperty(ref _selectedWeapon, value);
                if (_selectedWeapon != null)
                    _selectedWeapon.IsSelected = true;
            }
        }

        public DelegateCommand<WeaponItemModel> SelectWeaponCommand { get; }

        public WeaponPageViewModel(
            IContainerProvider containerProvider,
            IWeaponProvider weaponProvider
        )
            : base(containerProvider)
        {
            this.weaponProvider = weaponProvider;

            // 远程武器
            this.RangedWeaponModels = weaponProvider
                .GetRangedWeaponDatasAsync()
                .Result
                .Select(x => new WeaponItemModel(x))
                .ToList();

            // 近战武器
            List<WeaponData> meleeWeaponDatas = weaponProvider
                .GetMeleeWeaponDatasAsync()
                .Result;
            this.MeleeWeaponModels = meleeWeaponDatas
                .GroupBy(x => x.Group)
                .Select(g => new MeleeWeaponModel(g.Select(x => new WeaponItemModel(x)).ToList()))
                .ToList();

            // 默认选中第一个远程武器
            if (RangedWeaponModels.Count > 0)
            {
                SelectedWeapon = RangedWeaponModels[0];
            }

            SelectWeaponCommand = new DelegateCommand<WeaponItemModel>(weapon =>
            {
                if (weapon != null)
                {
                    SelectedWeapon = weapon;
                }
            });
        }
    }
}
