using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NarakaBladepoint.Shared.Services.Abstractions
{
    public interface IWeaponProvider
    {
        Task<List<MeleeWeaponData>> GetMeleeWeaponDatasAsync();
        Task<List<RangedWeaponData>> GetRangedWeaponDatasAsync();
    }
}
