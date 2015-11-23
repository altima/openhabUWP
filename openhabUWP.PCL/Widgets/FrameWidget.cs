using openhabUWP.Interfaces;
using openhabUWP.Interfaces.Common;
using openhabUWP.Interfaces.Widgets;

namespace openhabUWP.Widgets
{
    public class FrameWidget : IFrameWidget
    {
        public FrameWidget() { }

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
