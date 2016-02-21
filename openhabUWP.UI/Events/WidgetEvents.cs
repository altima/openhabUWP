using openhabUWP.Models;
using openhabUWP.Remote.Models;
using openhabUWP.UI.Widgets;
using Prism.Events;

namespace openhabUWP.Events
{
    public class WidgetEvents
    {
        /// <summary>
        /// 
        /// </summary>
        /// <seealso cref="Prism.Events.PubSubEvent{openhabUWP.Models.Widget}" />
        public class WidgetUpdateEvent : PubSubEvent<Widget> { }

        /// <summary>
        /// 
        /// </summary>
        /// <seealso cref="Prism.Events.PubSubEvent{openhabUWP.Models.Widget}" />
        public class WidgetTappedEvent : PubSubEvent<Widget> { }
    }
}
