using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using openhabUWP.Remote.Models;

namespace openhabUWP.Helper
{
    public class
        WidgetTemplateSelector : DataTemplateSelector
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
                if (widget.IsFrameWidget())
                    return FrameWidgeTemplate;

                if (widget.IsSwitchWidget())
                    return SwitchWidgeTemplate;

                if (widget.IsTextWidget())
                    return TextWidgeTemplate;

                if (widget.IsGroupWidget())
                    return GroupWidgeTemplate;

                if (widget.IsMapViewWidget())
                    return MapViewWidgeTemplate;

                if (widget.IsChartWidget())
                    return ChartWidgeTemplate;

                if (widget.IsColorPickerWidget())
                    return null;

                if (widget.IsImageWidget())
                    return null;

                if (widget.IsSelectionWidget())
                    return null;

                if (widget.IsSetPointWidget())
                    return null;

                if (widget.IsSliderWidget())
                    return null;

                if (widget.IsVideoWidget())
                    return null;
            }
            return null;
        }
    }
}
