using System;
using System.Diagnostics;
using System.Linq;
using Windows.Data.Json;
using openhabUWP.Interfaces.Common;
using openhabUWP.Interfaces.Widgets;
using openhabUWP.Items;
using openhabUWP.Models;
using openhabUWP.Widgets;

namespace openhabUWP.Helper
{
    public static class openhabFluent
    {
        public static Sitemap[] ToSitemaps(this string jsonString)
        {
            if (jsonString.IsNullOrEmpty()) return new Sitemap[0];
            return JsonArray.Parse(jsonString)
                .Where(a => a != null)
                .Select(a => a.GetObject().ToSitemap())
                .ToArray();
        }

        public static Sitemap ToSitemap(this string jsonString, Sitemap sitemap)
        {
            if (string.IsNullOrEmpty(jsonString)) return sitemap;
            return JsonObject.Parse(jsonString).ToSitemapDetail(sitemap);
        }

        public static Sitemap ToSitemap(this JsonObject jo)
        {
            var name = jo.GetNamedString("name");
            var label = jo.GetNamedString("label");
            var link = jo.GetNamedString("link");
            Page page = jo.GetNamedObject("homepage").ToPage();
            return new Sitemap(name, label, link, page);
        }

        public static Sitemap ToSitemapDetail(this JsonObject jo, Sitemap sitemap = null)
        {
            if (sitemap == null) sitemap = jo.ToSitemap();
            sitemap.Homepage = jo.GetNamedObject("homepage").ToPage();
            return sitemap;
        }

        public static Page ToPage(this JsonObject jo)
        {
            Page page = null;
            var pId = jo.GetNamedString("id", "");
            var pLink = jo.GetNamedString("link", "");
            var pTitle = jo.GetNamedString("title", "");
            var pIcon = jo.GetNamedString("icon", "");
            var pLeaf = jo.GetNamedBoolean("leaf", false);
            page = new Page(pId, pTitle, pLink, pLeaf, pIcon);

            // widgets
            var jWidgets = jo.GetNamedArray("widgets", null);
            if (jWidgets != null)
            {
                page.Widgets = jWidgets.ToWidgets();
            }
            return page;
        }

        public static IWidget[] ToWidgets(this JsonArray ja)
        {
            return ja.Select(j => j.GetObject().ToWidget()).ToArray();
        }

        public static IWidget ToWidget(this JsonObject jo)
        {
            IItem item = null;
            ILinkedPage linkedPage = null;
            IWidget widget = null;
            IWidget[] widgets = null;

            try
            {
                var widgetId = jo.GetNamedString("widgetId", "");
                var icon = jo.GetNamedString("icon", "");
                var label = jo.GetNamedString("label", "");
                var type = jo.GetNamedString("type", "");

                if (jo.ContainsKey("widgets"))
                {
                    var jWidgets = jo.GetNamedArray("widgets").Select(w => w.GetObject()).ToArray();
                    widgets = jWidgets
                        .Select(j => j.ToWidget())
                        .Where(w => w != null)
                        .ToArray();
                }

                if (jo.ContainsKey("item"))
                    item = jo.GetNamedObject("item").ToItem();
                if (jo.ContainsKey("linkedPage"))
                    linkedPage = jo.GetNamedObject("linkedPage").ToPage();

                switch (type)
                {
                    case "Frame":
                        widget = new FrameWidget(widgetId, label, icon);
                        ((FrameWidget)widget).Widgets = widgets;
                        ((FrameWidget)widget).Item = item;
                        ((FrameWidget)widget).LinkedPage = linkedPage;
                        break;
                    case "Text":
                        widget = new TextWidget(widgetId, label, icon);
                        ((TextWidget)widget).Item = item;
                        ((TextWidget)widget).LinkedPage = linkedPage;
                        break;
                    case "Switch":
                        widget = new SwitchWidget(widgetId, label, icon);
                        ((SwitchWidget)widget).Item = item;
                        break;
                    case "Group":
                        widget = new GroupWidget(widgetId, label, icon);
                        ((GroupWidget)widget).Widgets = widgets;
                        ((GroupWidget)widget).Item = item;
                        ((GroupWidget)widget).LinkedPage = linkedPage;
                        break;
                }
            }
            catch (Exception ex)
            {

            }
            return widget;
        }

        public static IItem ToItem(this JsonObject jo)
        {

            //var category = jo.GetNamedString("category", "");
            //var icon = jo.GetNamedString("icon", "");
            //var label = jo.GetNamedString("label", "");

            var type = jo.GetNamedString("type", "");
            var name = jo.GetNamedString("name", "");
            var link = jo.GetNamedString("link", "");
            var state = jo.GetNamedString("state", "");

            Debug.WriteLine(name);

            IItem item = null;
            switch (type)
            {
                case "GroupItem":
                    item = new GroupItem(name, link);
                    break;
                case "NumberItem":
                    item = new NumberItem(name, link, decimal.Parse(state));
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

    }
}