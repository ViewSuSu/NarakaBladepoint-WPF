using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NarakaBladepoint.Shared.Services.Infrastructure
{
    [Component(ComponentLifetime.Singleton)]
    internal class WeaponProvider : IWeaponProvider
    {
        public async Task<List<WeaponData>> GetRangedWeaponDatasAsync()
        {
            return ConfigurationDataReader.GetList<WeaponData>("RangedWeaponData");
        }

        public async Task<List<WeaponData>> GetMeleeWeaponDatasAsync()
        {
            return ConfigurationDataReader.GetList<WeaponData>("MeleeWeaponData");
        }
    }
}
