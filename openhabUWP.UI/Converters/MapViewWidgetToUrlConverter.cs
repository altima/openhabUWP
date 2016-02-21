using System;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml.Data;
using Microsoft.Practices.Unity;
using openhabUWP.Remote.Models;
using Prism.Windows.AppModel;

namespace openhabUWP.Converters
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Windows.UI.Xaml.Data.IValueConverter" />
    public class MapViewWidgetToUrlConverter : IValueConverter
    {
        private IResourceLoader _resourceLoader;

        public MapViewWidgetToUrlConverter()
        {
            if (DesignMode.DesignModeEnabled)
            {
                _resourceLoader = new ResourceLoaderAdapter(ResourceLoader.GetForCurrentView());
                return;
            }
            _resourceLoader = App.Current.Container.Resolve<IResourceLoader>();
        }


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
                var mapUrl = _resourceLoader.GetString("MapViewWidget_staticUrl");
                var key = _resourceLoader.GetString("BingMapsKey");
                var location = mapView.Item.State;
                var label = mapView.Item.Label;
                if (label.Length > 3)
                {
                    label = "";
                }

                var url = string.Format(mapUrl, location, size, key, label);
                return url;
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