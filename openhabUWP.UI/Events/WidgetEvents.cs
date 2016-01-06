using openhabUWP.Interfaces.Widgets;
using Prism.Events;

namespace openhabUWP.Events
{
    public class WidgetEvents
    {
        /// <summary>
        /// 
        /// </summary>
        /// <seealso cref="Prism.Events.PubSubEvent{openhabUWP.Interfaces.Widgets.IWidget}" />
        public class WidgetUpdateEvent : PubSubEvent<IWidget> { }

        /// <summary>
        /// 
        /// </summary>
        /// <seealso cref="Prism.Events.PubSubEvent{openhabUWP.Interfaces.Widgets.IWidget}" />
        public class WidgetTappedEvent : PubSubEvent<IWidget> { }
    }
    
}
