using System;
using Windows.ApplicationModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Imaging;
using Microsoft.Practices.Unity;
using openhabUWP.Database;
using openhabUWP.Helper;
using openhabUWP.Remote.Models;

namespace openhabUWP.UI.Widgets
{
    public sealed partial class ChartWidget
    {
        private readonly Random _random = new Random();
        private IOpenhabDatabase _database;
        private string urlFormat = "{0}/chart?groups={1}&period={2}&random={3}";

        public string Url1 { get; set; }
        public string Url2 { get; set; }

        public ChartWidget()
        {
            this.InitializeComponent();
            if (DesignMode.DesignModeEnabled) return;
            _database = App.Current.Container.Resolve<IOpenhabDatabase>();
            this.DataContextChanged += OnDataContextChanged;
            this.theImage.ImageFailed += TheImageOnImageFailed;
        }

        private void TheImageOnImageFailed(object sender, ExceptionRoutedEventArgs exceptionRoutedEventArgs)
        {
            var source = this.theImage.Source;
            if (source is BitmapImage)
            {
                var bmp = source as BitmapImage;
                if (bmp.UriSource == null) return;
                var tmpUrl = bmp.UriSource.ToString();

                if (Url2.IsNullOrEmpty()) return;
                if (Equals(Url1, Url2)) return;
                if (Equals(Url2, tmpUrl)) return;
                if (Equals(Url1, tmpUrl))
                    this.theImage.Source = new BitmapImage(new Uri(Url2));
            }
        }

        private void OnDataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            var widget = (args.NewValue as Widget);
            if (widget != null && widget.IsChartWidget())
            {
                var setup = _database.GetSetup();
                Url1 = string.Format(urlFormat, setup.Url, widget.Label, widget.Period, _random.Next(int.MinValue, int.MaxValue));
                if (!setup.RemoteUrl.IsNullOrEmpty())
                {
                    Url2 = string.Format(urlFormat, setup.RemoteUrl, widget.Label, widget.Period, _random.Next(int.MinValue, int.MaxValue));
                }
                this.theImage.Source = new BitmapImage(new Uri(Url1));
            }
        }
    }
}
