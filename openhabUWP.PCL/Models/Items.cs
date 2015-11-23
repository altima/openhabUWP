using openhabUWP.Interfaces;
using openhabUWP.Interfaces.Common;

namespace openhabUWP.Models
{
    public class Items : IItems
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Items"/> class.
        /// </summary>
        public Items() { }

        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        /// <value>
        /// The items.
        /// </value>
        public IItem[] items { get; set; }
    }
}
