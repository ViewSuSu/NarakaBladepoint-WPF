namespace NarakaBladepoint.Shared.Services.Abstractions
{
    public interface IHeroInfomation
    {
        /// <summary>
        /// 获取所有英雄头像
        /// </summary>
        /// <returns></returns>
        public Task<List<HeroAvatarModel>> GetHeroAvatarModelsAsync();

        /// <summary>
        /// 根据Index获取英雄头像信息
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Task<HeroAvatarModel> GetHeroAvatarModelByIdAsync(int index);
    }
}
