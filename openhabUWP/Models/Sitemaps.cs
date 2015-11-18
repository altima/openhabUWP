using openhabUWP.Interfaces.Common;

namespace openhabUWP.Models
{
    public class Sitemaps : ISitemaps
    {
        public Sitemaps()
        {

        }

        /// <summary>
        /// Gets or sets the maps.
        /// </summary>
        /// <value>
        /// The maps.
        /// </value>
        public Sitemap[] Maps { get; set; }
    }
}