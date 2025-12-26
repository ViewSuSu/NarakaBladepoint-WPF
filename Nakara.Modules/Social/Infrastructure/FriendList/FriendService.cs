using Nakara.Modules.Social.Domain.FriendList;
using Nakara.Modules.Social.Domain.FriendList.Interfaces;

namespace Nakara.Modules.Social.Infrastructure.FriendList
{
    internal class FriendService : IFriendService
    {
        public Task<List<Friend>> GetFriendsAsync()
        {
            return Task.FromResult(new List<Friend>());
        }
    }
}
