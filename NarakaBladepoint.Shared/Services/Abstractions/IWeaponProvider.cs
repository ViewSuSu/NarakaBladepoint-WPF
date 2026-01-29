namespace NarakaBladepoint.Shared.Services.Abstractions
{
    public interface IWeaponProvider
    {
        Task<List<WeaponData>> GetMeleeWeaponDatasAsync();

        Task<List<WeaponData>> GetRangedWeaponDatasAsync();
    }
}
