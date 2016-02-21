using System;
using System.Linq;
using Windows.Data.Json;
using openhabUWP.Helper;
using openhabUWP.Remote.Models;

namespace openhabUWP.Remote
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

                        if (sitemap.ContainsKey("sitemap"))
                        {
                            if (sitemap["sitemap"].ValueType == JsonValueType.Array)
                            {
                                array = sitemap.GetNamedArray("sitemap");
                            }
                            else if (sitemap["sitemap"].ValueType == JsonValueType.Object)
                            {
                                var obj = sitemap.GetNamedObject("sitemap");
                                array = new JsonArray() { obj };
                            }
                        }
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

        public static Sitemap ToSitemap(this string jsonString)
        {
            if (string.IsNullOrEmpty(jsonString)) return null;
            return JsonObject.Parse(jsonString).ToSitemapDetail();
        }

        public static Sitemap ToSitemap(this JsonObject jo)
        {
            var name = jo.GetNamedString("name");
            var label = jo.GetNamedString("label");
            var link = jo.GetNamedString("link");
            var page = jo.GetNamedObject("homepage").ToPage();
            return new Sitemap()
                .SetName(name)
                .SetLabel(label)
                .SetLink(link)
                .SetHomepage(page);
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
            var id = jo.GetNamedString("id", "");
            var link = jo.GetNamedString("link", "");
            var title = jo.GetNamedString("title", "");
            var icon = jo.GetNamedString("icon", "");
            var leaf = jo.ToBooleanSafe("key");
            Page parent = null;

            if (jo.ContainsKey("parent"))
            {
                parent = jo.GetNamedObject("parent").ToPage();
            }

            var page = new Page()
                .SetId(id)
                .SetLink(link)
                .SetTitle(title)
                .SetIcon(icon)
                .SetLeaf(leaf)
                .SetParent(parent);

            var jWidgets = jo.ToWidgetsSafe();
            if (jWidgets != null)
                return page.SetWidgets(jWidgets.ToWidgets());
            return page;
        }

        public static Page ToPage(this string jsonString)
        {
            if (string.IsNullOrEmpty(jsonString)) return null;
            return JsonObject.Parse(jsonString).ToPage();
        }

        public static Widget[] ToWidgets(this JsonArray ja)
        {
            return ja.Select(j => j.GetObject().ToWidget()).ToArray();
        }

        public static Widget ToWidget(this JsonObject jo)
        {
            var widgetId = jo.GetNamedString("widgetId", "");
            var icon = jo.GetNamedString("icon", "");
            var label = jo.GetNamedString("label", "");
            var type = jo.GetNamedString("type", "");

            var height = jo.GetNamedNumber("height", 0);
            var refresh = jo.GetNamedNumber("refresh", 0);
            var period = jo.GetNamedString("period", "");

            Widget widget = new Widget(widgetId, label, type, icon);
            switch (type)
            {
                case "Switch":
                case "Frame":
                case "Group":
                case "Image":
                case "Text":
                    break;
                case "Mapview":
                case "WebView":
                    widget = widget.SetHeight(height);
                    break;
                case "Chart":
                    widget = widget.SetChartProperties(height, refresh, period);
                    break;
                default:
                    return null;
            }

            try
            {
                var jWidgets = jo.ToWidgetsSafe();
                if (jWidgets != null)
                {
                    widget.SetWidgets(jWidgets.ToWidgets());
                }
                if (jo.ContainsKey("item"))
                {
                    widget.SetItem(jo.GetNamedObject("item").ToItem());
                }
                if (jo.ContainsKey("linkedPage"))
                {
                    widget.SetLinkedPage(jo.GetNamedObject("linkedPage").ToPage());
                }

                if (jo.ContainsKey("mappings"))
                {
                    widget.SetMappings(jo.GetNamedArray("mappings").ToMappings());
                }

            }
            catch (Exception)
            {

            }
            return widget;
        }

        public static Item ToItem(this JsonObject jo)
        {
            var category = jo.GetNamedString("category", "");
            var icon = jo.GetNamedString("icon", "");
            var label = jo.GetNamedString("label", "");

            var type = jo.GetNamedString("type", "");
            var name = jo.GetNamedString("name", "");
            var link = jo.GetNamedString("link", "");
            var state = jo.GetNamedString("state", "");
            var stateDescription = jo.GetNamedObject("stateDescription", null).ToStateDescription();


            return new Item(link, name, state, type)
                .SetStateDescription(stateDescription)
                .SetCategory(category)
                .SetIcon(icon)
                .SetLabel(label);
        }

        public static StateDescription ToStateDescription(this JsonObject jo)
        {
            if (jo == null) return null;
            var pattern = jo.GetNamedString("pattern", "");
            var readOnly = jo.GetNamedBoolean("readOnly", false);
            return new StateDescription(pattern, readOnly);
        }

        public static Mapping[] ToMappings(this JsonArray ja)
        {
            if (ja != null && ja.Any())
            {
                return ja.Select(a => new Mapping(
                    a.GetObject().GetNamedString("command"),
                    a.GetObject().GetNamedString("label", ""))).ToArray();
            }

            return new Mapping[0];
        }

        public static bool ToBoolean(this string input)
        {
            var b = false;
            bool.TryParse(input, out b);
            return b;
        }
    }
}