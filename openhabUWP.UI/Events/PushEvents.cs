using openhabUWP.Remote.Models;
using Prism.Events;

namespace openhabUWP.Events
{
    public class PushEvents
    {
        /// <summary>
        /// 
        /// </summary>
        /// <seealso cref="Prism.Events.PubSubEvent{openhabUWP.Remote.Models.EventData}" />
        public class PushEvent : PubSubEvent<EventData> { }
    }
}