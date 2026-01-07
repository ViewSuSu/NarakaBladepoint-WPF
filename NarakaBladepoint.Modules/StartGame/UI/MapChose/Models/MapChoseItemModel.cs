using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NarakaBladepoint.Shared.Datas;

namespace NarakaBladepoint.Modules.StartGame.UI.MapChose.Models
{
    class MapChoseItemModel : BindableBase
    {
        public MapChoseItemModel(MapItemData mapItemData)
        {
            MapItemData = mapItemData;
        }

        public MapItemData MapItemData { get; }

        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                RaisePropertyChanged();
            }
        }
    }
}
