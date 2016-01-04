using Windows.ApplicationModel;
using openhabUWP.Interfaces;
using openhabUWP.Interfaces.Common;
using openhabUWP.Interfaces.Widgets;
using openhabUWP.Models;

namespace openhabUWP.Widgets
{
    public class FrameWidget : IFrameWidget
    {
        public FrameWidget()
        {
            if (DesignMode.DesignModeEnabled)
            {
                Id = "ID10";
                Label = "FrameWidget";
                Widgets = new IWidget[]
                {
                    new SwitchWidget("1","Switch 1",""),
                    new SwitchWidget("2","Switch 2",""),
                    new SwitchWidget("3","Switch 3",""),
                    new TextWidget("4","Text 1",""),
                    new TextWidget("5","Text 2 [31.2 Grad]",""),
                    new SwitchWidget("6","Switch 4",""),
                    new SwitchWidget("7","Switch 5",""),
                };
            }

        }

        public FrameWidget(string widgetId, string label, string icon) : this()
        {
            this.Id = widgetId;
            this.Label = label;
            this.Icon = icon;
        }

        public string Id { get; set; }
        public string Label { get; set; }
        public string Icon { get; set; }
        public IWidget[] Widgets { get; set; }
        public IItem Item { get; set; }
        public Page LinkedPage { get; set; }
    }
}
