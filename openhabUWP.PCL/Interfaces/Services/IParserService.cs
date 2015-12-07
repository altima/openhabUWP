﻿using System.Xml.Linq;
using openhabUWP.Models;

namespace openhabUWP.Interfaces.Services
{
    public interface IParserService
    {
        Openhab ParseOpenhab(XElement openhabNode);

        Sitemaps ParseSitempas(XElement sitemapsNode);
        Sitemap ParseSitemap(XElement sitemapNode);
        Page ParsePage(XElement homepageNode);

        Models.Items ParseItems(XElement itemsNode);
    }
}