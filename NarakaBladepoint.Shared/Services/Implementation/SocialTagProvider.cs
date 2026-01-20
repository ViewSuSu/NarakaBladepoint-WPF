using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NarakaBladepoint.Shared.Services.Implementation
{
    [Component(ComponentLifetime.Singleton)]
    internal class SocialTagProvider : ISocialTagProvider
    {
        private readonly ICurrentUserInfoProvider currentUserInfoProvider;

        public SocialTagProvider(ICurrentUserInfoProvider currentUserInfoProvider)
        {
            this.currentUserInfoProvider = currentUserInfoProvider;
        }

        public async Task<List<SocialTagData>> GetSocialTags(SocialTagType socialTagType)
        {
            var datas = ConfigurationDataReader.GetList<SocialTagData>();
            return datas.Where(x => x.TagType == socialTagType).ToList();
        }

        public async Task<SocialTagData> GetSocialTagByIndex(int index)
        {
            return ConfigurationDataReader
                .GetList<SocialTagData>()
                .FirstOrDefault(x => x.Index == index);
        }

        public async Task<List<SocialTagData>> GetSocialTags()
        {
            return ConfigurationDataReader.GetList<SocialTagData>();
        }

        public async Task<bool> GetSocialTagIsSelectedByIndex(int index)
        {
            var currentModel = await currentUserInfoProvider.GetCurrentUserInfoAsync();
            return currentModel.SelectedSocialTags.Contains(index);
        }

        public async Task<SocialTagMicData> GetSocialTagMicDataByIndex(int index)
        {
            return ConfigurationDataReader
                .GetList<SocialTagMicData>()
                .FirstOrDefault(x => x.Index == index);
        }

        public Task<SocialTagOnlineData> GetSocialTagOnlineDataByIndex(int index)
        {
            return Task.FromResult(
                ConfigurationDataReader
                    .GetList<SocialTagOnlineData>()
                    .FirstOrDefault(x => x.Index == index)
            );
        }

        public async Task<List<SocialTagMicData>> GetSocialTagMicDatas()
        {
            return ConfigurationDataReader.GetList<SocialTagMicData>();
        }

        public async Task<List<SocialTagOnlineData>> GetSocialTagOnlineDatas()
        {
            return ConfigurationDataReader.GetList<SocialTagOnlineData>();
        }

        public async Task<bool> GetSocialTagMicDataIsSelectedByIndex(int index)
        {
            var currentModel = await currentUserInfoProvider.GetCurrentUserInfoAsync();
            return currentModel.SelectedSocialTagMic == index;
        }

        public async Task<bool> GetSocialTagOnlineDataIsSelectedByIndex(int index)
        {
            var currentModel = await currentUserInfoProvider.GetCurrentUserInfoAsync();
            return currentModel.SelectedSocialTagOnline == index;
        }
    }
}
