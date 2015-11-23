using openhabUWP.Interfaces.Common;

namespace openhabUWP.Interfaces
{
    public interface IHomepage : ILinkItem, IIdItem
    {
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="IHomepage"/> is leaf.
        /// </summary>
        /// <value>
        ///   <c>true</c> if leaf; otherwise, <c>false</c>.
        /// </value>
        bool Leaf { get; set; }
    }
}