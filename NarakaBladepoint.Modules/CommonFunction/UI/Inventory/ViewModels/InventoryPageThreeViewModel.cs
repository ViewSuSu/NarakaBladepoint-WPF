using System.ComponentModel;
using NarakaBladepoint.Modules.CommonFunction.Domain.Bases;
using NarakaBladepoint.Modules.CommonFunction.UI.Inventory.Models;

namespace NarakaBladepoint.Modules.CommonFunction.UI.Inventory.ViewModels
{
    internal class InventoryPageThreeViewModel : CommonFunctionPageViewModelBase
    {
        private static readonly Random _random = new();

        public BindingList<InventoryItemModel> TradableItems { get; set; } = [];

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

        public InventoryPageThreeViewModel()
        {
            LoadInventoryItems();
        }

        private void LoadInventoryItems()
        {
            // 生成50个假数据
            for (int i = 1; i <= 50; i++)
            {
                TradableItems.Add(new InventoryItemModel
                {
                    Name = $"可交易道具 {i}",
                    Count = _random.Next(1, 10),
                    Icon = null, // ImageSource 为 null
                });
            }

            // 默认选中第一个
            if (TradableItems.Count > 0)
                SelectedItem = TradableItems[0];
        }
    }
}

