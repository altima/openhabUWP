using System.Collections.Generic;

namespace openhabUWP.Remote.Models
{
    public static class WidgetFluent
    {
        public static Widget SetWidgets(this Widget input, Widget[] widgets)
        {
            input.Widgets = new List<Widget>(widgets);
            return input;
        }

        public static Widget SetWidgetId(this Widget input, string widgetId)
        {
            input.WidgetId = widgetId;
            return input;
        }

        public static Widget SetIcon(this Widget input, string icon)
        {
            input.Icon = icon;
            return input;
        }

        public static Widget SetLabel(this Widget input, string label)
        {
            input.Label = label;
            return input;
        }

        public static Widget SetType(this Widget input, string type)
        {
            input.Type = type;
            return input;
        }

        public static Widget SetItem(this Widget input, Item item)
        {
            input.Item = item;
            return input;
        }

        public static Widget SetLinkedPage(this Widget input, Page linkedPage)
        {
            input.LinkedPage = linkedPage;
            return input;
        }

        public static Widget SetMappings(this Widget input, Mapping[] mappings)
        {
            input.Mappings = mappings;
            return input;
        }

        public static Widget SetChartProperties(this Widget chart, double height, double refresh, string period)
        {
            chart.Height = height;
            chart.Refresh = refresh;
            chart.Period = period;
            return chart;
        }

        private static bool IsWidget(this Widget widget, string ofType)
        {
            return Equals(widget?.Type, ofType);
        }

        public static bool IsChartWidget(this Widget widget)
        {
            return widget.IsWidget("Chart");
        }

        public static bool IsSwitchWidget(this Widget widget)
        {
            return widget.IsWidget("Switch");
        }

        public static bool IsFrameWidget(this Widget widget)
        {
            return widget.IsWidget("Frame");
        }

        public static bool IsSelectionWidget(this Widget widget)
        {
            return widget.IsWidget("Selection");
        }

        public static bool IsSetPointWidget(this Widget widget)
        {
            return widget.IsWidget("Setpoint");
        }

        public static bool IsSliderWidget(this Widget widget)
        {
            return widget.IsWidget("Slider");
        }

        public static bool IsColorPickerWidget(this Widget widget)
        {
            return widget.IsWidget("Colorpicker");
        }

        public static bool IsMapViewWidget(this Widget widget)
        {
            return widget.IsWidget("Mapview");
        }

        public static bool IsImageWidget(this Widget widget)
        {
            return widget.IsWidget("Image");
        }

        public static bool IsVideoWidget(this Widget widget)
        {
            return widget.IsWidget("Video");
        }

        public static bool IsWebViewWidget(this Widget widget)
        {
            return widget.IsWidget("Webview");
        }
    }
}