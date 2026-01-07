using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using NarakaBladepoint.Framework.Core.Extensions;
using NarakaBladepoint.Resources;
using Newtonsoft.Json;

namespace NarakaBladepoint.Shared.Datas
{
    public class MapItemData
    {
        public int Index { get; set; }

        [JsonIgnore]
        public ImageSource MapImage =>
            ResourceImageReader
                .GetAllMapImagePairs()
                .Keys.FirstOrDefault(x => x.GetFileName().Contains(Name));

        [JsonIgnore]
        public ImageSource MapGif =>
            ResourceImageReader
                .GetAllMapImagePairs()
                .Values.FirstOrDefault(x => x.GetFileName().Contains(Name));

        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsSelected { get; set; }
    }
}
