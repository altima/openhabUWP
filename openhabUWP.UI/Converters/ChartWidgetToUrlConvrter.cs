using System;
using Windows.ApplicationModel;
using Windows.UI.Xaml.Data;
using Microsoft.Practices.Unity;
using openhabUWP.Database;
using openhabUWP.Models;
using openhabUWP.Remote.Models;

namespace openhabUWP.Converters
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Windows.UI.Xaml.Data.IValueConverter" />
    public class ChartWidgetToUrlConvrter : IValueConverter
    {
        private IOpenhabDatabase _database;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChartWidgetToUrlConvrter"/> class.
        /// </summary>
        public ChartWidgetToUrlConvrter()
        {
            if (DesignMode.DesignModeEnabled) return;
            _database = App.Current.Container.Resolve<IOpenhabDatabase>();
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
            //var setup = _database.GetSetup();
            var setup = new Setup();

            setup.Url = "http://192.168.178.107:8080/";

            var chartWidget = value as Widget;
            if (chartWidget != null)
            {
                //return string.Format("{0}/chart?groups={0}&period={1}&random={2}", chartWidget.Label, chartWidget.Period, 1);
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