using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nakara_WPF.Modules.Social.FriendList.Models;

namespace Nakara_WPF.Modules.Social.FriendList.Services
{
    class FriendService : IFriendService
    {
        public Task<List<Friend>> GetFriendsAsync()
        {
            return Task.FromResult(new List<Friend>());
        }
    }
}
