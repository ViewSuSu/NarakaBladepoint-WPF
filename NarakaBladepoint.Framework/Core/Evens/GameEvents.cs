namespace NarakaBladepoint.Framework.Core.Evens
{
    public class NavigationArgs
    {
        public NavigationArgs(string viewName, NavigationParameters navigationParameter = default)
        {
            ViewName = viewName;
            Parameter = navigationParameter;
        }

        public string ViewName { get; }
        public NavigationParameters Parameter { get; }
    }

    public class LoadHomePageRegionEvent : PubSubEvent<NavigationArgs> { }

    public class RemoveHomePageRegionEvent : PubSubEvent { }

    public class RemoveAllHomePageRegionEvent : PubSubEvent { }

    public class LoadMainContentRegionEvent : PubSubEvent<NavigationArgs> { }

    public class RemoveMainContentRegionEvent : PubSubEvent { }
}
