using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using openhabUWP.Remote.Models;

namespace openhabUWP.Helper
{
    public class WidgetTemplateSelector : DataTemplateSelector
    {
        public DataTemplate FrameWidgeTemplate { get; set; }
        public DataTemplate SwitchWidgeTemplate { get; set; }
        public DataTemplate TextWidgeTemplate { get; set; }
        public DataTemplate GroupWidgeTemplate { get; set; }
        public DataTemplate MapViewWidgeTemplate { get; set; }
        public DataTemplate ChartWidgeTemplate { get; set; }


        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            var widget = item as Widget;
            if (widget != null)
            {
                switch (widget.Type)
                {
                    case "Frame":
                        return FrameWidgeTemplate;
                    case "Text":
                        return TextWidgeTemplate;
                    case "Switch":
                        return SwitchWidgeTemplate;
                    case "Group":
                        return GroupWidgeTemplate;
                    case "MapView":
                        return MapViewWidgeTemplate;
                    case "Chart":
                        return ChartWidgeTemplate;
                }
            }
            return null;
        }
    }
}
