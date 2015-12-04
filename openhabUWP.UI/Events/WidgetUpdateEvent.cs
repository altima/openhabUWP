using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using openhabUWP.Interfaces.Widgets;
using Prism.Events;

namespace openhabUWP.Events
{
    public class WidgetUpdateEvent : PubSubEvent<IWidget>
    {

    }
}
