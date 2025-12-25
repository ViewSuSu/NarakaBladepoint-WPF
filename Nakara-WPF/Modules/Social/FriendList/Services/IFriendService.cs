using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nakara_WPF.Modules.Social.FriendList.Models;

namespace Nakara_WPF.Modules.Social.FriendList.Services
{
    interface IFriendService
    {
        Task<List<Friend>> GetFriendsAsync();
    }
}
