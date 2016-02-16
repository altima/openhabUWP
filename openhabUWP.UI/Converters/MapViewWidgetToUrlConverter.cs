using System;
using Windows.UI.Xaml.Data;
using openhabUWP.Models;
using openhabUWP.Remote.Models;

namespace openhabUWP.Converters
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Windows.UI.Xaml.Data.IValueConverter" />
    public class MapViewWidgetToUrlConverter : IValueConverter
    {
        /// <summary>
        /// Converts the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="targetType">Type of the target.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="language">The language.</param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            double size = (parameter is double) ? (double)parameter : 800;
            var mapView = value as Widget;
            if (mapView != null)
            {
                return string.Format("http://dev.virtualearth.net/REST/V1/Imagery/Map/Road/{0}?mapSize={1},{1}&key=Apo40xJZv08NT-pX9i_LE7PNGfuBnUMungCpaDYLuwh-nZiiH9dapequtuIhY-5d", mapView.Item.State, size);
            }
            return null;
        }

        /// <summary>
        /// Converts the back.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="targetType">Type of the target.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="language">The language.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}