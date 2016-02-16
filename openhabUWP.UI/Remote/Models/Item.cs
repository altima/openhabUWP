namespace openhabUWP.Remote.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class Item
    {
        /// <summary>
        /// Gets or sets the link.
        /// </summary>
        /// <value>
        /// The link.
        /// </value>
        public string Link { get; set; }
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>
        /// The state.
        /// </value>
        public string State { get; set; }
        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public string Type { get; set; }
        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        /// <value>
        /// The label.
        /// </value>
        public string Label { get; set; }
        /// <summary>
        /// Gets or sets the icon.
        /// </summary>
        /// <value>
        /// The icon.
        /// </value>
        public string Icon { get; set; }
        /// <summary>
        /// Gets or sets the category.
        /// </summary>
        /// <value>
        /// The category.
        /// </value>
        public string Category { get; set; }
        /// <summary>
        /// Gets or sets the tags.
        /// </summary>
        /// <value>
        /// The tags.
        /// </value>
        public string[] Tags { get; set; }
        /// <summary>
        /// Gets or sets the group names.
        /// </summary>
        /// <value>
        /// The group names.
        /// </value>
        public string[] GroupNames { get; set; }
        /// <summary>
        /// Gets or sets the state description.
        /// </summary>
        /// <value>
        /// The state description.
        /// </value>
        public StateDescription StateDescription { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="Item"/> class.
        /// </summary>
        public Item() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="Item"/> class.
        /// </summary>
        /// <param name="link">The link.</param>
        /// <param name="name">The name.</param>
        /// <param name="state">The state.</param>
        /// <param name="type">The type.</param>
        public Item(string link, string name, string state, string type) : this()
        {
            this.Link = link;
            this.Name = name;
            this.State = state;
            this.Type = type;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Item"/> class.
        /// </summary>
        /// <param name="link">The link.</param>
        /// <param name="name">The name.</param>
        /// <param name="state">The state.</param>
        /// <param name="type">The type.</param>
        /// <param name="stateDescription">The state description.</param>
        public Item(string link, string name, string state, string type, StateDescription stateDescription)
            : this(link, name, state, type)
        {
            this.StateDescription = stateDescription;
        }
    }
}