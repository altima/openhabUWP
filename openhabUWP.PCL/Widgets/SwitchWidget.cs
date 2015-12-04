using Windows.ApplicationModel;
using openhabUWP.Interfaces;
using openhabUWP.Interfaces.Common;
using openhabUWP.Interfaces.Widgets;

namespace openhabUWP.Widgets
{
    public class SwitchWidget : ISwitchWidget
    {
        public SwitchWidget()
        {
            if (DesignMode.DesignModeEnabled)
            {
                Id = "ID10000";
                Label = "My Switch";
                Icon = "";
            }
        }

        public SwitchWidget(string widgetId, string label, string icon)
        {
            this.Id = widgetId;
            this.Label = label;
            this.Icon = icon;
        }

        public string Id { get; set; }
        public string Label { get; set; }
        public string Icon { get; set; }
        public IItem Item { get; set; }
    }
}