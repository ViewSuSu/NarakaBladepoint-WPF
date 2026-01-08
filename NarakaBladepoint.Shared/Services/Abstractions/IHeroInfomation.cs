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

        /// <summary>
        /// 获取所有英雄印Tag列表
        /// </summary>
        /// <returns></returns>
        public Task<List<HeroTagModel>> GetHeroTagModelsAsync();

        /// <summary>
        /// 获取被选中的英雄印
        /// </summary>
        /// <returns></returns>
        public Task<List<HeroTagModel>> GetSelectedHeroTagModelsAsync();

        /// <summary>
        /// 根据Index获取英雄印
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Task<HeroTagModel> GetHeroHeroTagModelModelByIdAsync(int index);

        /// <summary>
        /// 判断英雄印是否被选中
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Task<bool> GetHeroHeroTagIsSelectedAsync(int index);
    }
}
