using openhabUWP.Interfaces.Widgets;
using openhabUWP.Models;

namespace openhabUWP.Widgets
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="openhabUWP.Interfaces.Widgets.IChartWidget" />
    public class ChartWidget : IChartWidget
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChartWidget"/> class.
        /// </summary>
        public ChartWidget() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChartWidget"/> class.
        /// </summary>
        /// <param name="widgetId">The widget identifier.</param>
        /// <param name="label">The label.</param>
        /// <param name="icon">The icon.</param>
        /// <param name="refresh">The refresh.</param>
        /// <param name="period">The period.</param>
        public ChartWidget(string widgetId, string label, string icon, int refresh, string period) : this()
        {
            this.WidgetId = widgetId;
            this.Label = label;
            this.Icon = icon;
            this.Refresh = refresh;
            this.Period = period;
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
        /// Gets or sets the refresh.
        /// </summary>
        /// <value>
        /// The refresh.
        /// </value>
        public int Refresh { get; set; }

        /// <summary>
        /// Gets or sets the period.
        /// </summary>
        /// <value>
        /// The period.
        /// </value>
        public string Period { get; set; }
    }
}