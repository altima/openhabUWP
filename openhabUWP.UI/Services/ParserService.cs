using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using openhabUWP.Models;
using openhabUWP.Remote.Models;

namespace openhabUWP.Services
{
    public interface IParserService
    {
        Sitemap[] ParseSitemaps(XElement sitemapsNode);
        Sitemap ParseSitemap(XElement sitemapNode);
        Page ParsePage(XElement homepageNode);
    }

    public class ParserService : IParserService
    {
        private ILogService _logService;

        public ParserService(ILogService logService)
        {
            _logService = logService;
        }

        public Sitemap[] ParseSitemaps(XElement sitemapsNode)
        {
            throw new NotImplementedException();
        }

        public Sitemap ParseSitemap(XElement sitemapNode)
        {
            var map = new Sitemap();
            map.Link = GetValue(sitemapNode, "link");
            map.Name = GetValue(sitemapNode, "name");
            map.Label = GetValue(sitemapNode, "label");
            map.Homepage = ParsePage(sitemapNode.Element("homepage"));
            return map;
        }

        public Page ParsePage(XElement homepageNode)
        {
            if (homepageNode == null) return null;

            var page = new Page();
            page.Id = GetValue(homepageNode, "id");
            page.Title = GetValue(homepageNode, "title");
            page.Link = GetValue(homepageNode, "link");
            page.Leaf = bool.Parse(GetValue(homepageNode, "leaf"));

            //widgets
            page.Widgets = new List<Widget>(ParseWidgets(homepageNode.Elements("widget")));

            return page;
        }

        public Item ParseItem(XElement itemNode)
        {
            Item item = new Item();
            item.Type = GetValue(itemNode, "type");
            item.Name = GetValue(itemNode, "name");
            item.State = GetValue(itemNode, "state");
            item.Link = GetValue(itemNode, "link");
            return item;
        }

        public IEnumerable<Widget> ParseWidgets(IEnumerable<XElement> widgetNodes)
        {
            //return widgetNodes.AsParallel().Select(ParseWidget);
            return widgetNodes.Select(ParseWidget);
        }

        public Widget ParseWidget(XElement widgetNode)
        {
            Widget widget = new Widget();
            widget.WidgetId = GetValue(widgetNode, "widgetId");
            widget.Type = GetValue(widgetNode, "type");
            widget.Label = GetValue(widgetNode, "label");
            widget.Icon = GetValue(widgetNode, "icon");
            widget.Item = ParseItem(widgetNode.Element("item"));
            widget.LinkedPage = ParsePage(widgetNode.Element("linkedPage"));
            return widget;
        }

        //helpers
        private string GetValue(XElement node, string elementName)
        {
            if (node != null && node.Element(elementName) != null)
            {
                return node.Element(elementName).Value;
            }
            return string.Empty;
        }

        private string GetAttribute(XElement node, string attributeName)
        {
            if (node != null && node.Attribute(attributeName) != null)
            {
                return node.Attribute(attributeName).Value;
            }
            return string.Empty;
        }
    }
}
