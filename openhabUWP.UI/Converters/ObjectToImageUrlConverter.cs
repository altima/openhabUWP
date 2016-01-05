using System;
using Windows.UI.Xaml.Data;
using openhabUWP.Interfaces.Common;

namespace openhabUWP.Converters
{
    public sealed class ObjectToImageUrlConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            //todo
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}