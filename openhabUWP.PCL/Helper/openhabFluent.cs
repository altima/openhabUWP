using System;
using System.Diagnostics;
using System.Linq;
using Windows.Data.Json;
using Newtonsoft.Json;
using openhabUWP.Interfaces;
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
            if (!jsonString.IsNullOrEmpty())
            {
                JsonArray array = null;
                if (!JsonArray.TryParse(jsonString, out array))
                {
                    //maybe openhab1?
                    JsonObject sitemap = null;
                    if (JsonObject.TryParse(jsonString, out sitemap))
                    {
                        array = sitemap.GetNamedArray("sitemap");
                    }
                }

                if (array != null)
                {
                    return array
                        .Where(a => a != null)
                        .Select(a => a.GetObject().ToSitemap())
                        .ToArray();
                }
            }
            return new Sitemap[0];
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

        private static JsonArray ToWidgetsSafe(this JsonObject jo)
        {
            JsonArray jWidgets = null;
            if (jo.ContainsKey("widgets"))
            {
                jWidgets = jo.GetNamedArray("widgets", null);
            }

            if (jo.ContainsKey("widget"))
            {
                switch (jo["widget"].ValueType)
                {
                    case JsonValueType.Object:
                        jWidgets = new JsonArray()
                        {
                            jo["widget"].GetObject()
                        };
                        break;
                    case JsonValueType.Array:
                        jWidgets = jo.GetNamedArray("widget", null);
                        break;
                }
            }
            return jWidgets;
        }
        private static bool ToBooleanSafe(this JsonObject jo, string key)
        {
            IJsonValue leafValue = null;
            if (jo.TryGetValue(key, out leafValue))
            {
                switch (leafValue.ValueType)
                {
                    case JsonValueType.String:
                        return leafValue.GetString().ToBoolean();
                    case JsonValueType.Boolean:
                        return leafValue.GetBoolean();
                }
            }
            return false;
        }

        public static Page ToPage(this JsonObject jo)
        {
            Page page = null;
            var pId = jo.GetNamedString("id", "");
            var pLink = jo.GetNamedString("link", "");
            var pTitle = jo.GetNamedString("title", "");
            var pIcon = jo.GetNamedString("icon", "");
            var pLeaf = jo.ToBooleanSafe("key");

            page = new Page(pId, pTitle, pLink, pLeaf, pIcon);

            var jWidgets = jo.ToWidgetsSafe();
            if (jWidgets != null)
                page.Widgets = jWidgets.ToWidgets();

            return page;
        }

        public static Page ToPage(this string jsonString, Page page)
        {
            if (string.IsNullOrEmpty(jsonString)) return page;
            return JsonObject.Parse(jsonString).ToPage();
        }

        public static IWidget[] ToWidgets(this JsonArray ja)
        {
            return ja.Select(j => j.GetObject().ToWidget()).ToArray();
        }

        public static IWidget ToWidget(this JsonObject jo)
        {
            IItem item = null;
            Page linkedPage = null;
            IWidget widget = null;
            IWidget[] widgets = null;

            try
            {
                var widgetId = jo.GetNamedString("widgetId", "");
                var icon = jo.GetNamedString("icon", "");
                var label = jo.GetNamedString("label", "");
                var type = jo.GetNamedString("type", "");

                var jWidgets = jo.ToWidgetsSafe();

                if (jWidgets != null)
                    widgets = jWidgets.ToWidgets();

                if (jo.ContainsKey("item"))
                    item = jo.GetNamedObject("item").ToItem();
                if (jo.ContainsKey("linkedPage"))
                    linkedPage = jo.GetNamedObject("linkedPage").ToPage();

                switch (type)
                {
                    case "Frame":
                        widget = new FrameWidget(widgetId, label, icon);
                        ((FrameWidget)widget).Item = item;
                        widget.Widgets = widgets;
                        widget.LinkedPage = linkedPage;
                        break;
                    case "Text":
                        widget = new TextWidget(widgetId, label, icon);
                        ((TextWidget)widget).Item = item;
                        widget.LinkedPage = linkedPage;
                        break;
                    case "Switch":
                        widget = new SwitchWidget(widgetId, label, icon);
                        ((SwitchWidget)widget).Item = item;
                        break;
                    case "Group":
                        widget = new GroupWidget(widgetId, label, icon);
                        ((GroupWidget)widget).Item = item;
                        widget.Widgets = widgets;
                        widget.LinkedPage = linkedPage;
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

            switch (type)
            {
                case "GroupItem":
                    return new GroupItem(name, link);
                case "NumberItem":
                    if (state == "NULL") state = "0";
                    return new NumberItem(name, link, decimal.Parse(state));
                case "DateTimeItem":
                    if (state == "NULL") state = DateTime.Parse("1970-01-01 01:00").ToString("s");
                    return new DateTimeItem(name, link, DateTime.Parse(state));
                case "SwitchItem":
                    return new SwitchItem(name, link, state);
            }
            return null;
        }


        public static bool ToBoolean(this string input)
        {
            var b = false;
            bool.TryParse(input, out b);
            return b;
        }
    }
}