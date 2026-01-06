namespace NarakaBladepoint.Shared.Services.Abstractions
{
    public interface ICurrentUserFriendInfo
    {
        Task<List<FriendDataItem>> GetFriendsAsync(string nameKeyword = null);
    }
}
