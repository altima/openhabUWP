using Windows.ApplicationModel;
using openhabUWP.Interfaces.Widgets;
using openhabUWP.Models;

namespace openhabUWP.Widgets
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="openhabUWP.Interfaces.Widgets.IWebViewWidget" />
    public class WebViewWidget : IWebViewWidget
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WebViewWidget"/> class.
        /// </summary>
        public WebViewWidget()
        {
            if (DesignMode.DesignModeEnabled)
            {
                WidgetId = "ID10";
                Label = "WebViewWidget";
                Url = "http://soapbubbles.de";

            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WebViewWidget"/> class.
        /// </summary>
        /// <param name="widgetId">The widget identifier.</param>
        /// <param name="icon">The icon.</param>
        /// <param name="url">The URL.</param>
        public WebViewWidget(string widgetId, string icon, string url) : this()
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
}