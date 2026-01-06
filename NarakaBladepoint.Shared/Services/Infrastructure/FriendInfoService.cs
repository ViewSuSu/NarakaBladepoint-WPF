using NarakaBladepoint.Shared.Jsons;

namespace NarakaBladepoint.Shared.Services.Infrastructure
{
    [Component(ComponentLifetime.Singleton)]
    internal class FriendInfoService : ICurrentUserFriendInfo
    {
        public async Task<List<FriendDataItem>> GetFriendsAsync(string nameKeyword = null)
        {
            return string.IsNullOrEmpty(nameKeyword)
                ? ConfigurationDataReader.Get<FriendData>().List
                : ConfigurationDataReader
                    .Get<FriendData>()
                    .List.Where(x => x.Name.Contains(nameKeyword))
                    .ToList();
        }
    }
}
