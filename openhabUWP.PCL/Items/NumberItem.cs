using openhabUWP.Enums;
using openhabUWP.Interfaces.Items;

namespace openhabUWP.Items
{
    public class NumberItem : INumberItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NumberItem"/> class.
        /// </summary>
        public NumberItem()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NumberItem"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="link">The link.</param>
        /// <param name="value">The value.</param>
        public NumberItem(string name, string link, decimal value) : this()
        {
            this.Name = name;
            this.Link = link;
            this.State = value;
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

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>
        /// The state.
        /// </value>
        public decimal State { get; set; }
    }
}