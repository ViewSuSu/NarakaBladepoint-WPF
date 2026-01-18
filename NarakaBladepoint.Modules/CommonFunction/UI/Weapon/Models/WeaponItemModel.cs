using System.Collections.Generic;
using System.Windows.Media;

namespace NarakaBladepoint.Modules.CommonFunction.UI.Weapon.Models
{
    internal class WeaponItemModel : BindableBase
    {
        public string Name { get; set; }

        public int Level { get; set; }

        public ImageSource Icon { get; set; }

        public ImageSource Background { get; set; }

        public List<ImageSource> SkillImages { get; set; }

        public int TotalDamage { get; set; }

        public int TotalEliminations { get; set; }

        public int TotalAssists { get; set; }

        public int MaxDamagePerGame { get; set; }

        public int MaxEliminationsPerGame { get; set; }

        public string Group { get; set; }

        public int TotalParries { get; set; }

        public int Headshots { get; set; }

        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set => SetProperty(ref _isSelected, value);
        }

        public WeaponItemModel() { }

        public WeaponItemModel(WeaponData weaponData)
        {
            Name = weaponData.Name;
            Level = weaponData.Level;
            Icon = weaponData.Icon;
            Background = weaponData.Background;
            SkillImages = weaponData.SkillImages;
            TotalDamage = weaponData.TotalDamage;
            TotalEliminations = weaponData.TotalEliminations;
            TotalAssists = weaponData.TotalAssists;
            MaxDamagePerGame = weaponData.MaxDamagePerGame;
            MaxEliminationsPerGame = weaponData.MaxEliminationsPerGame;
            Group = weaponData.Group;
            TotalParries = weaponData.TotalParries;
            Headshots = weaponData.Headshots;
        }
    }
}
