using openhabUWP.Interfaces;
using openhabUWP.Interfaces.Common;

namespace openhabUWP.Models
{
    /// <summary>
    /// <sitemap>
    ///     <name>default</name>
    ///     <label>Demo</label>
    ///     <link>http://192.168.178.3:8080/rest/sitemaps/default</link>
    ///      <homepage>
    ///         <link>http://192.168.178.3:8080/rest/sitemaps/default/default</link>
    ///         <leaf>false</leaf>
    ///      </homepage>
    /// </sitemap>
    /// </summary>
    public class Sitemap : ISitemap
    {
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
        /// <param name="name">The name.</param>
        /// <param name="label">The label.</param>
        /// <param name="link">The link.</param>
        /// <param name="homepage">The homepage.</param>
        public Sitemap(string name, string label, string link, Page homepage)
        {
            this.Name = name;
            this.Label = label;
            this.Link = link;
            this.Homepage = homepage;
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        /// <value>
        /// The label.
        /// </value>
        public string Label { get; set; }

        /// <summary>
        /// Gets or sets the homepage.
        /// </summary>
        /// <value>
        /// The homepage.
        /// </value>
        public IPage Homepage { get; set; }

        /// <summary>
        /// Gets or sets the link.
        /// </summary>
        /// <value>
        /// The link.
        /// </value>
        public string Link { get; set; }
    }
}
