namespace openhabUWP.Remote.Models
{
    /// <summary>
    /// 
    /// </summary>
    public static class SitemapFluent
    {
        /// <summary>
        /// Sets the homepage.
        /// </summary>
        /// <param name="sitemap">The sitemap.</param>
        /// <param name="homepage">The homepage.</param>
        /// <returns></returns>
        public static Sitemap SetHomepage(this Sitemap sitemap, Page homepage)
        {
            sitemap.Homepage = homepage;
            return sitemap;
        }

        /// <summary>
        /// Sets the label.
        /// </summary>
        /// <param name="sitemap">The sitemap.</param>
        /// <param name="label">The label.</param>
        /// <returns></returns>
        public static Sitemap SetLabel(this Sitemap sitemap, string label)
        {
            sitemap.Label = label;
            return sitemap;
        }

        /// <summary>
        /// Sets the link.
        /// </summary>
        /// <param name="sitemap">The sitemap.</param>
        /// <param name="link">The link.</param>
        /// <returns></returns>
        public static Sitemap SetLink(this Sitemap sitemap, string link)
        {
            sitemap.Link = link;
            return sitemap;
        }

        /// <summary>
        /// Sets the name.
        /// </summary>
        /// <param name="sitemap">The sitemap.</param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public static Sitemap SetName(this Sitemap sitemap, string name)
        {
            sitemap.Name = name;
            return sitemap;
        }
    }
}