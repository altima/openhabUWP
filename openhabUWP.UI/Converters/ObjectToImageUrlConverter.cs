using System;
using Windows.ApplicationModel;
using Windows.UI.Xaml.Data;
using Microsoft.Practices.Unity;
using openhabUWP.Database;
using openhabUWP.Helper;

namespace openhabUWP.Converters
{
    public sealed class ObjectToImageUrlConverter : IValueConverter
    {
        private IOpenhabDatabase _database;

        public ObjectToImageUrlConverter()
        {
            if(DesignMode.DesignModeEnabled) return;
            _database = App.Current.Container.Resolve<IOpenhabDatabase>();
        }


        public object Convert(object value, Type targetType, object parameter, string language)
        {
            //var url = _database.GetSetup().Url;
            var url = "http://192.168.178.107:8080/";

            var icon = value as string;

            if (!url.IsNullOrEmpty() && !icon.IsNullOrEmpty())
            {
                return string.Concat(url, "/../images/", icon, ".png");
            }

            //todo
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}