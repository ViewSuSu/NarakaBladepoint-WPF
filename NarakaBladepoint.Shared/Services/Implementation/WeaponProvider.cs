namespace NarakaBladepoint.Shared.Services.Implementation
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
