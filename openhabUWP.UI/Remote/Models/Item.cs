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
        /// Sets the link.
        /// </summary>
        /// <param name="link">The link.</param>
        /// <returns></returns>
        public Item SetLink(string link)
        {
            this.Link = link;
            return this;
        }

        /// <summary>
        /// Sets the name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public Item SetName(string name)
        {
            this.Name = name;
            return this;
        }

        /// <summary>
        /// Sets the state.
        /// </summary>
        /// <param name="state">The state.</param>
        /// <returns></returns>
        public Item SetState(string state)
        {
            this.State = state;
            return this;
        }

        /// <summary>
        /// Sets the type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public Item SetType(string type)
        {
            this.Type = type;
            return this;
        }
    }
}