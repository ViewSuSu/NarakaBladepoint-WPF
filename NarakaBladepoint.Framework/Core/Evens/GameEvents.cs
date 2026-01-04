namespace NarakaBladepoint.Framework.Core.Evens
{
    public class LoadRightSidePanelRegionEvent : PubSubEvent<string> { }

    public class RemoveRightSidePanelRegionEvent : PubSubEvent { }

    public class LoadHomePageRegionEvent : PubSubEvent<string> { }

    public class RemoveHomePageRegionEvent : PubSubEvent { }

    public class LoadMainContentRegionEvent : PubSubEvent<string> { }

    public class RemoveMainContentRegionEvent : PubSubEvent { }
}
