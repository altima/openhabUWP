using openhabUWP.Interfaces.Items;

namespace openhabUWP.Items
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="openhabUWP.Interfaces.Items.ISwitchItem" />
    public class SwitchItem : ISwitchItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SwitchItem"/> class.
        /// </summary>
        public SwitchItem()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SwitchItem"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="link">The link.</param>
        /// <param name="value">The value.</param>
        public SwitchItem(string name, string link, string value) : this()
        {
            this.Name = name;
            this.Link = link;
            this.State = Equals(value, "ON");
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public string Type { get; set; }

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
        /// Gets or sets a value indicating whether this <see cref="SwitchItem"/> is state.
        /// </summary>
        /// <value>
        ///   <c>true</c> if state; otherwise, <c>false</c>.
        /// </value>
        public bool State { get; set; }
    }
}