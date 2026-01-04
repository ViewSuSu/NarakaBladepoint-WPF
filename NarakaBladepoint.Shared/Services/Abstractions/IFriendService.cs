namespace NarakaBladepoint.Shared.Services.Abstractions
{
    public interface IFriendService
    {
        Task<List<FriendData>> GetFriendsAsync();
    }
}
