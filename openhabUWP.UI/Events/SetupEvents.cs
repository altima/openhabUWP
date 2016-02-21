using Prism.Events;

namespace openhabUWP.Events
{
    public class SetupEvents
    {
        public class ShowServerListEvent : PubSubEvent<bool> { }
        public class HideServerListEvent : PubSubEvent<bool> { }
        public class ServerItemTappedEvent : PubSubEvent<string> { }
    }
}