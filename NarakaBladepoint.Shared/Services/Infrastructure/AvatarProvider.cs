using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NarakaBladepoint.Resources;

namespace NarakaBladepoint.Shared.Services.Infrastructure
{
    [Component(ComponentLifetime.Singleton)]
    internal class AvatarProvider : IAvatarProvider
    {
        public async Task<List<AvatarData>> GetAvatarsAsync()
        {
            List<AvatarData> datas = [];
            var avatars = ResourceImageReader.GetAllAvatarImages();
            for (int i = 0; i < avatars.Count; i++)
            {
                var data = new AvatarData() { Index = i };
                datas.Add(data);
            }
            return datas;
        }
    }
}
