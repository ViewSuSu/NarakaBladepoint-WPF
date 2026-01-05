namespace NarakaBladepoint.Framework.Core.Evens
{
    public class LoadHomePageRegionEvent : PubSubEvent<string> { }

    public class RemoveHomePageRegionEvent : PubSubEvent { }

    public class LoadMainContentRegionEvent : PubSubEvent<string> { }

    public class RemoveMainContentRegionEvent : PubSubEvent { }
}
