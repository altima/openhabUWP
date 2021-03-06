namespace openhabUWP.Remote.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class Sitemap
    {
        /// <summary>
        /// Gets or sets the homepage.
        /// </summary>
        /// <value>
        /// The homepage.
        /// </value>
        public Page Homepage { get; set; }
        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        /// <value>
        /// The label.
        /// </value>
        public string Label { get; set; }
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
        /// Initializes a new instance of the <see cref="Sitemap"/> class.
        /// </summary>
        public Sitemap()
        {
            this.Homepage = new Page();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Sitemap"/> class.
        /// </summary>
        /// <param name="label">The label.</param>
        /// <param name="link">The link.</param>
        /// <param name="name">The name.</param>
        public Sitemap(string label, string link, string name) : this()
        {
            this.Label = label;
            this.Link = link;
            this.Name = name;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Sitemap"/> class.
        /// </summary>
        /// <param name="label">The label.</param>
        /// <param name="link">The link.</param>
        /// <param name="name">The name.</param>
        /// <param name="homepage">The homepage.</param>
        public Sitemap(string label, string link, string name, Page homepage) : this(label, link, name)
        {
            this.Homepage = homepage;
        }
    }
}