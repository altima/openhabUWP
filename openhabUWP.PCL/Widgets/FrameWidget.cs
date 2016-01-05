using Windows.ApplicationModel;
using openhabUWP.Interfaces;
using openhabUWP.Interfaces.Common;
using openhabUWP.Interfaces.Widgets;
using openhabUWP.Models;

namespace openhabUWP.Widgets
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="openhabUWP.Interfaces.Widgets.IFrameWidget" />
    public class FrameWidget : IFrameWidget
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FrameWidget"/> class.
        /// </summary>
        public FrameWidget()
        {
            if (DesignMode.DesignModeEnabled)
            {
                WidgetId = "ID10";
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

        /// <summary>
        /// Initializes a new instance of the <see cref="FrameWidget"/> class.
        /// </summary>
        /// <param name="widgetId">The widget identifier.</param>
        /// <param name="label">The label.</param>
        /// <param name="icon">The icon.</param>
        public FrameWidget(string widgetId, string label, string icon) : this()
        {
            this.WidgetId = widgetId;
            this.Label = label;
            this.Icon = icon;
        }

        /// <summary>
        /// Gets or sets the widget identifier.
        /// </summary>
        /// <value>
        /// The widget identifier.
        /// </value>
        public string WidgetId { get; set; }

        /// <summary>
        /// Gets or sets the icon.
        /// </summary>
        /// <value>
        /// The icon.
        /// </value>
        public string Icon { get; set; }

        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        /// <value>
        /// The label.
        /// </value>
        public string Label { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the widgets.
        /// </summary>
        /// <value>
        /// The widgets.
        /// </value>
        public IWidget[] Widgets { get; set; }

        /// <summary>
        /// Gets or sets the item.
        /// </summary>
        /// <value>
        /// The item.
        /// </value>
        public IItem Item { get; set; }

        /// <summary>
        /// Gets or sets the linked page.
        /// </summary>
        /// <value>
        /// The linked page.
        /// </value>
        public IPage LinkedPage { get; set; }
    }
}
