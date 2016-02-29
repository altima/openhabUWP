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
        /// <seealso cref="Prism.Events.PubSubEvent{openhabUWP.Events.SwitchWidgetButtonTappedArgs}" />
        public class WidgetTappedEvent : PubSubEvent<SwitchWidgetButtonTappedArgs> { }

    }

    public class SwitchWidgetButtonTappedArgs
    {
        public Widget Widget { get; private set; }
        public string Command { get; private set; }

        public SwitchWidgetButtonTappedArgs()
        {
            this.Widget = null;
            this.Command = string.Empty;
        }

        public SwitchWidgetButtonTappedArgs(Widget wiget) : this()
        {
            this.Widget = wiget;
        }

        public SwitchWidgetButtonTappedArgs(Widget widget, string command) : this(widget)
        {
            this.Command = command;
        }
    }
}
