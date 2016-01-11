using Windows.ApplicationModel;
using Windows.Storage.Search;
using openhabUWP.Interfaces.Widgets;
using openhabUWP.Models;

namespace openhabUWP.Widgets
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="openhabUWP.Interfaces.Widgets.IVideoWidget" />
    public class VideoWidget : IVideoWidget
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VideoWidget"/> class.
        /// </summary>
        public VideoWidget()
        {
            if (DesignMode.DesignModeEnabled)
            {
                WidgetId = "ID10";
                Label = "VideoWidget";
                Url = "http://192.168.178.107:8080/proxy?sitemap=demo.sitemap&widgetId=02030101";

            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VideoWidget"/> class.
        /// </summary>
        /// <param name="widgetId">The widget identifier.</param>
        /// <param name="icon">The icon.</param>
        /// <param name="url">The URL.</param>
        public VideoWidget(string widgetId, string icon, string url) : this()
        {
            this.WidgetId = widgetId;
            this.Icon = icon;

            this.Url = url;
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
        /// Gets or sets the URL.
        /// </summary>
        /// <value>
        /// The URL.
        /// </value>
        public string Url { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="openhabUWP.Interfaces.Widgets.ISelectionWidget" />
    public class SelectionWidget : ISelectionWidget
    {
        public string WidgetId { get; set; }
        public string Icon { get; set; }
        public string Label { get; set; }
        public string Type { get; set; }
        public Mapping[] Mappings { get; set; }
        public Page LinkedPage { get; set; }
        public IWidget[] Widgets { get; set; }
    }

    public class SetPointWidget
    {

    }

    public class SliderWidget
    {

    }
}