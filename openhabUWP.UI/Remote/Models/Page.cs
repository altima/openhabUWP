namespace openhabUWP.Remote.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class Page
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the icon.
        /// </summary>
        /// <value>
        /// The icon.
        /// </value>
        public string Icon { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Page"/> is leaf.
        /// </summary>
        /// <value>
        ///   <c>true</c> if leaf; otherwise, <c>false</c>.
        /// </value>
        public bool Leaf { get; set; }

        /// <summary>
        /// Gets or sets the link.
        /// </summary>
        /// <value>
        /// The link.
        /// </value>
        public string Link { get; set; }

        /// <summary>
        /// Gets or sets the widgets.
        /// </summary>
        /// <value>
        /// The widgets.
        /// </value>
        public Widget[] Widgets { get; set; }

        /// <summary>
        /// Gets or sets the parent.
        /// </summary>
        /// <value>
        /// The parent.
        /// </value>
        public Page Parent { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Page"/> class.
        /// </summary>
        public Page()
        {
            this.Widgets = new Widget[0];
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Page"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="title">The title.</param>
        /// <param name="link">The link.</param>
        /// <param name="leaf">if set to <c>true</c> [leaf].</param>
        /// <param name="icon">The icon.</param>
        public Page(string id, string title, string link, bool leaf, string icon) : this()
        {
            this.Id = id;
            this.Title = title;
            this.Link = link;
            this.Leaf = leaf;
            this.Icon = icon;
        }

        /// <summary>
        /// Sets the identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public Page SetId(string id)
        {
            this.Id = id;
            return this;
        }

        /// <summary>
        /// Sets the icon.
        /// </summary>
        /// <param name="icon">The icon.</param>
        /// <returns></returns>
        public Page SetIcon(string icon)
        {
            this.Icon = icon;
            return this;
        }

        /// <summary>
        /// Sets the title.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <returns></returns>
        public Page SetTitle(string title)
        {
            this.Title = title;
            return this;
        }

        /// <summary>
        /// Sets the leaf.
        /// </summary>
        /// <param name="leaf">if set to <c>true</c> [leaf].</param>
        /// <returns></returns>
        public Page SetLeaf(bool leaf)
        {
            this.Leaf = leaf;
            return this;
        }

        /// <summary>
        /// Sets the link.
        /// </summary>
        /// <param name="link">The link.</param>
        /// <returns></returns>
        public Page SetLink(string link)
        {
            this.Link = link;
            return this;
        }

        /// <summary>
        /// Sets the widgets.
        /// </summary>
        /// <param name="widgets">The widgets.</param>
        /// <returns></returns>
        public Page SetWidgets(params Widget[] widgets)
        {
            this.Widgets = widgets;
            return this;
        }

        /// <summary>
        /// Sets the parent.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <returns></returns>
        public Page SetParent(Page parent)
        {
            this.Parent = parent;
            return this;
        }
    }
}