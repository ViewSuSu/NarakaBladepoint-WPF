using System.Windows.Media;

namespace Nakara.Modules.CommonFunction.UI.Inventory.Models
{
    internal class InventoryItemModel : BindableBase
    {
        private int count;

        public int Count
        {
            get { return count; }
            set
            {
                count = value;
                SetProperty(ref count, value);
            }
        }

        private string name;

        public string Name
        {
            get { return name; }
            set
            {
                name = value;

                SetProperty(ref name, value);
            }
        }

        private ImageSource icon;

        public ImageSource Icon
        {
            get { return icon; }
            set { icon = value; }
        }

        public string Description { get; set; }
    }
}
