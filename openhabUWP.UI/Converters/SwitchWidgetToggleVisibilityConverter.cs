using System;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using openhabUWP.Remote.Models;

namespace openhabUWP.Converters
{
    public class SwitchWidgetToggleVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var widget = value as Widget;
            if (widget != null && widget.IsSwitchWidget())
            {
                return widget.Mappings == null || !widget.Mappings.Any() ? Visibility.Visible : Visibility.Collapsed;
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    public class SwitchWidgetButtonVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var widget = value as Widget;
            if (widget != null && widget.IsSwitchWidget())
            {
                return widget.Mappings != null && widget.Mappings.Any() ? Visibility.Visible : Visibility.Collapsed;
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
