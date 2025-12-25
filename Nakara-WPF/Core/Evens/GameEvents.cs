using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nakara_WPF.Core.Evens
{
    /// <summary>
    /// 切换好友面板事件
    /// null: 切换, true: 强制打开, false: 强制关闭
    /// </summary>
    public class ToggleFriendPanelEvent : PubSubEvent<bool?> { }

    /// <summary>
    /// 好友面板状态变化事件
    /// </summary>
    public class FriendPanelStateChangedEvent : PubSubEvent<bool> { }

    /// <summary>
    /// 请求打开好友面板事件
    /// </summary>
    public class OpenFriendPanelEvent : PubSubEvent { }

    /// <summary>
    /// 请求关闭好友面板事件
    /// </summary>
    public class CloseFriendPanelEvent : PubSubEvent { }
}
