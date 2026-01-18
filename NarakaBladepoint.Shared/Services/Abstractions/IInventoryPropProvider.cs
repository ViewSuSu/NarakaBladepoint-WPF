namespace NarakaBladepoint.Shared.Services.Abstractions
{
    public interface IInventoryPropProvider
    {
        /// <summary>
        /// 获取所有仓库物品数据
        /// </summary>
        /// <returns></returns>
        Task<List<InventoryPropItemData>> GetInventoryPropsAsync();
    }
}
