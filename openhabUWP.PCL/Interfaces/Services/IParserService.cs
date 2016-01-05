using System.Xml.Linq;
using openhabUWP.Models;

namespace openhabUWP.Interfaces.Services
{
    public interface IParserService
    {
        Sitemap[] ParseSitemaps(XElement sitemapsNode);
        Sitemap ParseSitemap(XElement sitemapNode);
        Page ParsePage(XElement homepageNode);
    }
}