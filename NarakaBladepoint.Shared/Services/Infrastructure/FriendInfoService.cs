namespace NarakaBladepoint.Shared.Services.Infrastructure
{
    [Component(ComponentLifetime.Singleton)]
    internal class FriendInfoService : ICurrentUserFriendInfo
    {
        public async Task<List<FriendData>> GetFriendsAsync()
        {
            return await Task.FromResult(new List<FriendData>());
        }
    }
}
