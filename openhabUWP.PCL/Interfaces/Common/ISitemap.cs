using openhabUWP.Interfaces.Common;

namespace openhabUWP.Interfaces
{
    public interface ISitemap : ILinkItem, ILabelItem, INameItem
    {
        /// <summary>
        /// Gets or sets the homepage.
        /// </summary>
        /// <value>
        /// The homepage.
        /// </value>
        IPage Homepage { get; set; }
    }
}