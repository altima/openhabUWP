using System;
using openhabUWP.Enums;
using openhabUWP.Interfaces;
using openhabUWP.Interfaces.Items;

namespace openhabUWP.Items
{
    public class DateTimeItem : IDateTimeItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DateTimeItem"/> class.
        /// </summary>
        public DateTimeItem()
        {
            this.State = DateTime.Parse("01-01-1970 00:00");
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DateTimeItem"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="link">The link.</param>
        /// <param name="value">The value.</param>
        public DateTimeItem(string name, string link, DateTime value)
        {
            this.State = value;
            this.Name = name;
            this.Link = link;
        }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>
        /// The state.
        /// </value>
        public DateTime State { get; set; }

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