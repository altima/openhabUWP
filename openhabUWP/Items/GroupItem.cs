using openhabUWP.Enums;
using openhabUWP.Interfaces.Items;

namespace openhabUWP.Items
{
    public class GroupItem : IGroupItem
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="GroupItem"/> class.
        /// </summary>
        public GroupItem()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GroupItem"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="link">The link.</param>
        public GroupItem(string name, string link) : this()
        {
            this.Name = name;
            this.Link = link;
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the link.
        /// </summary>
        /// <value>
        /// The link.
        /// </value>
        public string Link { get; set; }
    }
}