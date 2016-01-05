using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using openhabUWP.Helper;

namespace openhabUWP.Converters
{
    /// <summary>
    /// Value converter that translates string to <see cref="Visibility.Visible"/> and string.empty to
    /// <see cref="Visibility.Collapsed"/>.
    /// </summary>
    public sealed class StringToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return (value is string && !((string)value).IsNullOrEmpty()) ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}