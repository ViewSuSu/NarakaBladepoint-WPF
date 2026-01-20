using NarakaBladepoint.Resources;

namespace NarakaBladepoint.Shared.Services.Implementation
{
    [Component(ComponentLifetime.Singleton)]
    internal class InventoryPropProvider : IInventoryPropProvider
    {
        private static readonly Random _random = new();

        public Task<List<InventoryPropItemData>> GetInventoryPropsAsync()
        {
            List<InventoryPropItemData> datas = [];
            var propImages = ResourceImageReader.GetAllInventoryPropImages();
            for (int i = 0; i < propImages.Count; i++)
            {
                var data = new InventoryPropItemData { Index = i, Count = _random.Next(1, 100) };
                datas.Add(data);
            }
            return Task.FromResult(datas);
        }
    }
}
