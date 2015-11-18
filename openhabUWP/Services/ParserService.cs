using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;
using openhabUWP.Interfaces;
using openhabUWP.Interfaces.Common;
using openhabUWP.Interfaces.Services;
using openhabUWP.Interfaces.Widgets;
using openhabUWP.Items;
using openhabUWP.Models;
using openhabUWP.Widgets;

namespace openhabUWP.Services
{
    public class ParserService : IParserService
    {
        private ILogService _logService;

        public ParserService(ILogService logService)
        {
            _logService = logService;
        }

        public Openhab ParseOpenhab(XElement openhabNode)
        {
            var hab = new Openhab();
            hab.Links = openhabNode.Descendants("link")
                    .AsParallel()
                    .Select(n => new Link(GetAttribute(n, "type"), n.Value))
                    .ToArray();
            return hab;
        }

        public Sitemaps ParseSitempas(XElement sitemapsNode)
        {
            var maps = new Sitemaps();
            maps.Maps = sitemapsNode.Descendants("sitemap")
                .AsParallel()
                .Select(ParseSitemap)
                .ToArray();
            return maps;
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
            page.Widgets = ParseWidgets(homepageNode.Elements("widget")).ToArray();

            return page;
        }

        public Models.Items ParseItems(XElement itemsNode)
        {
            var items = new Models.Items();
            items.items = itemsNode.Descendants("item")
                .AsParallel()
                .Select(ParseItem)
                .Where(i => i != null)
                .ToArray();
            return items;
        }

        public IItem ParseItem(XElement itemNode)
        {
            IItem item = null;
            var type = GetValue(itemNode, "type");
            var name = GetValue(itemNode, "name");
            var state = GetValue(itemNode, "state");
            var link = GetValue(itemNode, "link");
            switch (type)
            {
                case "GroupItem":
                    item = new GroupItem(name, link);
                    break;
                case "NumberItem":
                    item = new NumberItem(name, link, decimal.Parse(state, CultureInfo.InvariantCulture));
                    break;
                case "DateTimeItem":
                    item = new DateTimeItem(name, link, DateTime.Parse(state));
                    break;
                case "SwitchItem":
                    item = new SwitchItem(name, link, state);
                    break;
            }
            return item;
        }

        public IEnumerable<IWidget> ParseWidgets(IEnumerable<XElement> widgetNodes)
        {
            //return widgetNodes.AsParallel().Select(ParseWidget);
            return widgetNodes.Select(ParseWidget);
        }

        public IWidget ParseWidget(XElement widgetNode)
        {
            IWidget widget = null;
            var widgetId = GetValue(widgetNode, "widgetId");
            var type = GetValue(widgetNode, "type");
            var label = GetValue(widgetNode, "label");
            var icon = GetValue(widgetNode, "icon");
            var item = ParseItem(widgetNode.Element("item"));
            var linkedPage = ParsePage(widgetNode.Element("linkedPage"));
            switch (type)
            {
                case "Frame":
                    widget = new FrameWidget(widgetId, label, icon);
                    ((FrameWidget)widget).Widgets = ParseWidgets(widgetNode.Elements("widget")).ToArray();
                    break;
                case "Text":
                    widget = new TextWidget(widgetId, label, icon);
                    widget.Item = item;
                    ((TextWidget) widget).LinkedPage = linkedPage;

                    break;
                case "Switch":
                    widget = new SwitchWidget(widgetId, label, icon);
                    widget.Item = item;
                    break;
            }




            return widget;
        }

        //helpers
        private string GetValue(XElement node, string elementName)
        {
            if (node!=null && node.Element(elementName) != null)
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
