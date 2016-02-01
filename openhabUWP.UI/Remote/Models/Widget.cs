namespace openhabUWP.Remote.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class Widget
    {
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
        public Widget[] Widgets { get; set; }

        /// <summary>
        /// Gets or sets the item.
        /// </summary>
        /// <value>
        /// The item.
        /// </value>
        public Item Item { get; set; }

        /// <summary>
        /// Gets or sets the linked page.
        /// </summary>
        /// <value>
        /// The linked page.
        /// </value>
        public Page LinkedPage { get; set; }

        /// <summary>
        /// Gets or sets the mappings.
        /// </summary>
        /// <value>
        /// The mappings.
        /// </value>
        public Mapping[] Mappings { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Widget"/> class.
        /// </summary>
        public Widget()
        {
            this.Widgets = new Widget[0];
            this.Item = new Item();
            this.LinkedPage = new Page();
            this.Mappings = new Mapping[0];
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Widget"/> class.
        /// </summary>
        /// <param name="widgetId">The widget identifier.</param>
        /// <param name="label">The label.</param>
        /// <param name="type">The type.</param>
        /// <param name="icon">The icon.</param>
        public Widget(string widgetId, string label, string type, string icon) : this()
        {
            this.WidgetId = widgetId;
            this.Icon = icon;
            this.Label = label;
            this.Type = type;
        }

        /// <summary>
        /// Sets the widget identifier.
        /// </summary>
        /// <param name="widgetId">The widget identifier.</param>
        /// <returns></returns>
        public Widget SetWidgetId(string widgetId)
        {
            this.WidgetId = widgetId;
            return this;
        }

        /// <summary>
        /// Sets the icon.
        /// </summary>
        /// <param name="icon">The icon.</param>
        /// <returns></returns>
        public Widget SetIcon(string icon)
        {
            this.Icon = icon;
            return this;
        }

        /// <summary>
        /// Sets the label.
        /// </summary>
        /// <param name="label">The label.</param>
        /// <returns></returns>
        public Widget SetLabel(string label)
        {
            this.Label = label;
            return this;
        }

        /// <summary>
        /// Sets the type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public Widget SetType(string type)
        {
            this.Type = type;
            return this;
        }

        /// <summary>
        /// Sets the widgets.
        /// </summary>
        /// <param name="widgets">The widgets.</param>
        /// <returns></returns>
        public Widget SetWidgets(Widget[] widgets)
        {
            this.Widgets = widgets;
            return this;
        }

        /// <summary>
        /// Sets the item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        public Widget SetItem(Item item)
        {
            this.Item = item;
            return this;
        }

        /// <summary>
        /// Sets the linked page.
        /// </summary>
        /// <param name="linkedPage">The linked page.</param>
        /// <returns></returns>
        public Widget SetLinkedPage(Page linkedPage)
        {
            this.LinkedPage = linkedPage;
            return this;
        }

        /// <summary>
        /// Sets the mappings.
        /// </summary>
        /// <param name="mappings">The mappings.</param>
        /// <returns></returns>
        public Widget SetMappings(Mapping[] mappings)
        {
            this.Mappings = mappings;
            return this;
        }
    }
}