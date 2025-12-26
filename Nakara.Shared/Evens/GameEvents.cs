namespace Nakara.Shared.Evens
{
    /// <summary>
    /// 请求打开好友面板事件
    /// </summary>
    public class OpenFriendPanelEvent : PubSubEvent { }

    /// <summary>
    /// 请求关闭好友面板事件
    /// </summary>
    public class CloseFriendPanelEvent : PubSubEvent { }

    /// <summary>
    /// 打开模式选择面板事件
    /// </summary>
    public class OpenModeSelectionEvent : PubSubEvent { }

    /// <summary>
    /// 关闭模式选择面板事件
    /// </summary>
    public class CloseModeSelectionEvent : PubSubEvent { }
}
