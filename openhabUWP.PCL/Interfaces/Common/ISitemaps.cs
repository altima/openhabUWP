using openhabUWP.Models;

namespace openhabUWP.Interfaces.Common
{
    public interface ISitemaps
    {
        /// <summary>
        /// Gets or sets the maps.
        /// </summary>
        /// <value>
        /// The maps.
        /// </value>
        Sitemap[] Maps { get; set; }
    }
}