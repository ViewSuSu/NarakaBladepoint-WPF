using Nakara.Modules.Social.Domain.FriendList;
using Nakara.Modules.Social.Domain.FriendList.Interfaces;

namespace Nakara.Modules.Social.Infrastructure.FriendList
{
    internal class FriendService : IFriendService
    {
        public async Task<List<Friend>> GetFriendsAsync()
        {
            return await Task.FromResult(new List<Friend>());
        }
    }
}
