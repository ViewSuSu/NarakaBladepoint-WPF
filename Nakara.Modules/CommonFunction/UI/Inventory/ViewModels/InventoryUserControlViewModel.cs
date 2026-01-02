using System.ComponentModel;
using Nakara.Modules.CommonFunction.UI.Inventory.Models;

namespace Nakara.Modules.CommonFunction.UI.Inventory.ViewModels
{
    internal class InventoryUserControlViewModel : CanRemoveMainContentRegionViewModelBase
    {
        public BindingList<InventoryItemModel> InventoryItemModels { get; set; } = [];

        public InventoryUserControlViewModel(IContainerExtension containerExtension)
            : base(containerExtension)
        {
            ClickItemComamnd = new DelegateCommand(() => { });
        }

        public DelegateCommand ClickItemComamnd { get; set; }
    }
}
