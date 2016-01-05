using System;
using Windows.UI.Xaml.Data;
using openhabUWP.Interfaces.Widgets;

namespace openhabUWP.Converters
{
    public class WidgetClickableConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var item = value as IWidget;
            if (item !=null)
            {
                if (item.LinkedPage != null)
                {
                    return true;
                }

                if (item is ISwitchWidget)
                {
                    return true;
                }
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
