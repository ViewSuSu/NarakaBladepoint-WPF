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
        public async Task<List<RangedWeaponData>> GetRangedWeaponDatasAsync()
        {
            return ConfigurationDataReader.GetList<RangedWeaponData>();
        }

        public async Task<List<MeleeWeaponData>> GetMeleeWeaponDatasAsync()
        {
            return ConfigurationDataReader.GetList<MeleeWeaponData>();
        }
    }
}
