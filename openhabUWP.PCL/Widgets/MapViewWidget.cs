using openhabUWP.Interfaces.Items;
using openhabUWP.Interfaces.Widgets;
using openhabUWP.Models;

namespace openhabUWP.Widgets
{
    public class MapViewWidget : IMapViewWidget
    {
        //http://dev.virtualearth.net/REST/V1/Imagery/Map/Road/Brandenburger%20Gate%20Berlin?mapSize=800,800&key=Apo40xJZv08NT-pX9i_LE7PNGfuBnUMungCpaDYLuwh-nZiiH9dapequtuIhY-5d


        /// <summary>
        /// Initializes a new instance of the <see cref="MapViewWidget"/> class.
        /// </summary>
        public MapViewWidget() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="MapViewWidget"/> class.
        /// </summary>
        /// <param name="widgetId">The widget identifier.</param>
        /// <param name="label">The label.</param>
        /// <param name="icon">The icon.</param>
        /// <param name="height">The height. (maybe zoom)</param>
        public MapViewWidget(string widgetId, string label, string icon, double height) : this()
        {
            this.WidgetId = widgetId;
            this.Label = label;
            this.Icon = icon;
            this.Height = height;
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
        /// Gets or sets the mappings.
        /// </summary>
        /// <value>
        /// The mappings.
        /// </value>
        public Mapping[] Mappings { get; set; }

        /// <summary>
        /// Gets or sets the linked page.
        /// </summary>
        /// <value>
        /// The linked page.
        /// </value>
        public Page LinkedPage { get; set; }

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
        /// Gets or sets the height.
        /// </summary>
        /// <value>
        /// The height.
        /// </value>
        public double Height { get; set; }
    }
}