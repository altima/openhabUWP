using System;
using Windows.UI.Xaml.Data;
using openhabUWP.Remote.Models;

namespace openhabUWP.Converters
{
    public class WidgetToOnOffConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var widget = value as Widget;

            if (widget != null && widget.IsSwitchWidget())
            {
                var item = widget.Item;

                switch (item.State)
                {
                    case "ON":
                        return true;
                    case "OFF":
                        return false;
                }
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
