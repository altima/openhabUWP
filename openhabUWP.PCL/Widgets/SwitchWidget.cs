using Windows.ApplicationModel;
using openhabUWP.Interfaces;
using openhabUWP.Interfaces.Common;
using openhabUWP.Interfaces.Widgets;

namespace openhabUWP.Widgets
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="openhabUWP.Interfaces.Widgets.ISwitchWidget" />
    public class SwitchWidget : ISwitchWidget
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SwitchWidget"/> class.
        /// </summary>
        public SwitchWidget()
        {
            if (DesignMode.DesignModeEnabled)
            {
                WidgetId = "ID10000";
                Label = "My Switch";
                Icon = "";
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SwitchWidget"/> class.
        /// </summary>
        /// <param name="widgetId">The widget identifier.</param>
        /// <param name="label">The label.</param>
        /// <param name="icon">The icon.</param>
        public SwitchWidget(string widgetId, string label, string icon)
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
        /// Gets or sets the linked page.
        /// </summary>
        /// <value>
        /// The linked page.
        /// </value>
        public IPage LinkedPage { get; set; }

        /// <summary>
        /// Gets or sets the widgets.
        /// </summary>
        /// <value>
        /// The widgets.
        /// </value>
        public IWidget[] Widgets { get; set; }

        /// <summary>
        /// Gets or sets the icon.
        /// </summary>
        /// <value>
        /// The icon.
        /// </value>
        public string Icon { get; set; }

        /// <summary>
        /// Gets or sets the item.
        /// </summary>
        /// <value>
        /// The item.
        /// </value>
        public IItem Item { get; set; }
    }
}