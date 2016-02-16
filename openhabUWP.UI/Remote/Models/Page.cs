using System.Collections.Generic;
using Windows.ApplicationModel;

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
        public List<Widget> Widgets { get; set; }
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
            if (!DesignMode.DesignModeEnabled) Widgets = new List<Widget>();
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
    }
}