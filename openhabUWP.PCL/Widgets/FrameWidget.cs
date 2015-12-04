using Windows.ApplicationModel;
using openhabUWP.Interfaces;
using openhabUWP.Interfaces.Common;
using openhabUWP.Interfaces.Widgets;

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
                    new SwitchWidget(), 
                    new SwitchWidget(), 
                    new SwitchWidget(), 
                    new TextWidget(), 
                    new TextWidget(),
                    new SwitchWidget(),
                    new SwitchWidget(),
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
        public ILinkedPage LinkedPage { get; set; }
    }
}
