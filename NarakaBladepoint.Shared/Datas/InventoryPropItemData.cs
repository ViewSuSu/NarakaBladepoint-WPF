using System.Windows.Media;
using NarakaBladepoint.Framework.Core.Extensions;
using NarakaBladepoint.Resources;

namespace NarakaBladepoint.Shared.Datas
{
    public class InventoryPropItemData
    {
        public int Index { get; set; }
        public string Name => Icon.GetFileName();
        public int Count { get; set; }
        public ImageSource Icon => ResourceImageReader.GetInventoryPropImage(Index);
    }
}
