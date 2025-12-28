namespace Nakara.Modules.EventCenter.Domain.Events
{
    internal class LoadEventCenterMainContentEvent : PubSubEvent<string> { }

    internal class RemoveEventCenterMainContentEvent : PubSubEvent { }
}
