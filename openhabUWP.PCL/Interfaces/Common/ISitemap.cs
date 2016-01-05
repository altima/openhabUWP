using openhabUWP.Models;

namespace openhabUWP.Interfaces.Common
{
    public interface ISitemap
    {
        /// <summary>
        /// Gets or sets the homepage.
        /// </summary>
        /// <value>
        /// The homepage.
        /// </value>
        Page Homepage { get; set; }

        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        /// <value>
        /// The label.
        /// </value>
        string Label { get; set; }

        /// <summary>
        /// Gets or sets the link.
        /// </summary>
        /// <value>
        /// The link.
        /// </value>
        string Link { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        string Name { get; set; }
    }
}