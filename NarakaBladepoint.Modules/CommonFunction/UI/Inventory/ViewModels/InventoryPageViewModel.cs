using System.ComponentModel;
using NarakaBladepoint.Modules.CommonFunction.UI.Inventory.Models;

namespace NarakaBladepoint.Modules.CommonFunction.UI.Inventory.ViewModels
{
    internal class InventoryPageViewModel : CanRemoveMainContentRegionViewModelBase
    {
        public BindingList<InventoryItemModel> InventoryItemModels { get; set; } = [];

        private DelegateCommand _clickItemComamnd;

        public DelegateCommand ClickItemComamnd =>
            _clickItemComamnd ??= new DelegateCommand(() => { });

        public InventoryPageViewModel(IContainerProvider containerProvider)
            : base(containerProvider) { }
    }
}
