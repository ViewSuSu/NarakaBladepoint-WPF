namespace NarakaBladepoint.Shared.Services.Abstractions
{
    /// <summary>
    /// 天赋技能点数据提供器接口
    /// </summary>
    public interface ITianfuSkillPointProvider
    {
        /// <summary>
        /// 根据天赋名称获取技能点配置
        /// </summary>
        /// <param name="tianfuName">天赋名称（tianfu1, tianfu2, tianfu3）</param>
        /// <returns>天赋技能点配置数据</returns>
        Task<TianfuSkillPointData> GetTianfuSkillPointAsync(string tianfuName);

        /// <summary>
        /// 保存天赋技能点配置
        /// </summary>
        /// <param name="tianfuData">天赋技能点配置数据</param>
        /// <returns>是否保存成功</returns>
        Task<bool> SaveTianfuSkillPointAsync(TianfuSkillPointData tianfuData);
    }
}
