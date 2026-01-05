namespace NarakaBladepoint.Shared.Services.Abstractions
{
    public interface ICurrentUserFriendInfo
    {
        Task<List<FriendData>> GetFriendsAsync();
    }
}
