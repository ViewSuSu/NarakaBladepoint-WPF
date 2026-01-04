namespace NarakaBladepoint.Shared.Services.Infrastructure
{
    [Component]
    internal class FriendService : IFriendService
    {
        public async Task<List<FriendData>> GetFriendsAsync()
        {
            return await Task.FromResult(new List<FriendData>());
        }
    }
}
