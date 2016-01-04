using Windows.ApplicationModel;
using openhabUWP.Interfaces.Common;
using openhabUWP.Interfaces.Widgets;
using openhabUWP.Models;

namespace openhabUWP.Widgets
{
    public class GroupWidget : IGroupWidget
    {
        public GroupWidget()
        {
            if (DesignMode.DesignModeEnabled)
            {
                Id = "ID10000";
                Label = "My Group";
                Icon = "";
            }
        }

        public GroupWidget(string widgetId, string label, string icon)
        {
            this.Id = widgetId;
            this.Label = label;
            this.Icon = icon;
        }

        public string Id { get; set; }
        public string Label { get; set; }
        public string Icon { get; set; }
        public IItem Item { get; set; }
        public IWidget[] Widgets { get; set; }
        public Page LinkedPage { get; set; }
    }
}