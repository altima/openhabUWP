using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using openhabUWP.Widgets;

namespace openhabUWP.Helper
{
    public class WidgetTemplateSelector : DataTemplateSelector
    {
        public DataTemplate FrameWidgeTemplate { get; set; }
        public DataTemplate SwitchWidgeTemplate { get; set; }
        public DataTemplate TextWidgeTemplate { get; set; }
        public DataTemplate GroupWidgeTemplate { get; set; }
        public DataTemplate MapViewWidgeTemplate { get; set; }


        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            if (item is FrameWidget) return FrameWidgeTemplate;
            if (item is SwitchWidget) return SwitchWidgeTemplate;
            if (item is TextWidget) return TextWidgeTemplate;
            if (item is GroupWidget) return GroupWidgeTemplate;
            if (item is MapViewWidget) return MapViewWidgeTemplate;

            return null;
        }
    }
}
