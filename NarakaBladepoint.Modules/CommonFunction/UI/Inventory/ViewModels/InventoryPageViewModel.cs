using System.ComponentModel;
using NarakaBladepoint.Modules.CommonFunction.Domain.Bases;
using NarakaBladepoint.Modules.CommonFunction.UI.Inventory.Models;

namespace NarakaBladepoint.Modules.CommonFunction.UI.Inventory.ViewModels
{
    internal class InventoryPageViewModel : CommonFunctionPageViewModelBase
    {
        private readonly IInventoryPropProvider _inventoryPropProvider;
        private static readonly Random _random = new();

        public BindingList<InventoryItemModel> AllItems { get; set; } = [];
        public BindingList<InventoryItemModel> GiftPackItems { get; set; } = [];
        public BindingList<InventoryItemModel> CouponItems { get; set; } = [];
        public BindingList<InventoryItemModel> ConquestItems { get; set; } = [];
        public BindingList<InventoryItemModel> BountyItems { get; set; } = [];

        private InventoryItemModel _selectedItem;
        public InventoryItemModel SelectedItem
        {
            get => _selectedItem;
            set
            {
                if (_selectedItem != null)
                    _selectedItem.IsSelected = false;
                SetProperty(ref _selectedItem, value);
                if (_selectedItem != null)
                    _selectedItem.IsSelected = true;
            }
        }

        private DelegateCommand<InventoryItemModel> _selectItemCommand;
        public DelegateCommand<InventoryItemModel> SelectItemCommand =>
            _selectItemCommand ??= new DelegateCommand<InventoryItemModel>(item =>
            {
                SelectedItem = item;
            });

        public InventoryPageViewModel(IInventoryPropProvider inventoryPropProvider)
        {
            _inventoryPropProvider = inventoryPropProvider;
            LoadInventoryItems();
        }

        private void LoadInventoryItems()
        {
            var items = _inventoryPropProvider.GetInventoryPropsAsync().Result;
            var baseList = items
                .Select(item => new InventoryItemModel
                {
                    Name = item.Name,
                    Count = item.Count,
                    Icon = item.Icon,
                })
                .ToList();

            // 全部 Tab 显示所有数据
            FillShuffledList(AllItems, baseList, baseList.Count);
            // 其他 Tab 显示较少的数据（随机取 60%-80% 的数量）
            FillShuffledList(GiftPackItems, baseList, (int)(baseList.Count * 0.7));
            FillShuffledList(CouponItems, baseList, (int)(baseList.Count * 0.6));
            FillShuffledList(ConquestItems, baseList, (int)(baseList.Count * 0.75));
            FillShuffledList(BountyItems, baseList, (int)(baseList.Count * 0.65));

            // 默认选中第一个
            if (AllItems.Count > 0)
                SelectedItem = AllItems[0];
        }

        private static void FillShuffledList(
            BindingList<InventoryItemModel> target,
            List<InventoryItemModel> source,
            int count
        )
        {
            var shuffled = source.OrderBy(_ => _random.Next()).Take(count).ToList();
            foreach (var item in shuffled)
            {
                target.Add(
                    new InventoryItemModel
                    {
                        Name = item.Name,
                        Count = item.Count,
                        Icon = item.Icon,
                    }
                );
            }
        }
    }
}
