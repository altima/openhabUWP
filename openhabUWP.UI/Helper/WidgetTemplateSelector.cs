using System.Diagnostics;
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
        public DataTemplate ImageWidgetTemplate { get; set; }


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
                    Debug.WriteLine(widget.Type);

                if (widget.IsImageWidget())
                    return ImageWidgetTemplate;

                if (widget.IsSelectionWidget())
                    Debug.WriteLine(widget.Type);

                if (widget.IsSetPointWidget())
                    Debug.WriteLine(widget.Type);

                if (widget.IsSliderWidget())
                    Debug.WriteLine(widget.Type);

                if (widget.IsVideoWidget())
                    Debug.WriteLine(widget.Type);

                if (widget.IsSelectionWidget())
                    Debug.WriteLine(widget.Type);

                return TextWidgeTemplate;
            }
            return null;
        }
    }
}
