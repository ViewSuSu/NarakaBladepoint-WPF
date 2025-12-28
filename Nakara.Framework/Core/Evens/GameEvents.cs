namespace Nakara.Framework.Core.Evens
{
    public class LoadSidePanelRegionEvent : PubSubEvent<string> { }

    public class RemoveSidePanelRegionEvent : PubSubEvent { }

    public class LoadHomePageRegionEvent : PubSubEvent<string> { }

    public class RemoveHomePageRegionEvent : PubSubEvent { }

    public class LoadMainContentRegionEvent : PubSubEvent<string> { }

    public class RemoveMainContentRegionEvent : PubSubEvent { }
}
