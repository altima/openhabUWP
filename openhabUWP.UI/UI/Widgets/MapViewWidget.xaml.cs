using System;
using Windows.ApplicationModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Imaging;
using openhabUWP.Remote.Models;

namespace openhabUWP.UI.Widgets
{
    public sealed partial class MapViewWidget
    {
        private string urlFormat =
            "http://dev.virtualearth.net/REST/V1/Imagery/Map/Road/{1}/{2}?mapSize={3},{3}&pp={1};68;{4}&key={0}";

        private string bingMapsKey = "Apo40xJZv08NT-pX9i_LE7PNGfuBnUMungCpaDYLuwh-nZiiH9dapequtuIhY-5d";

        public MapViewWidget()
        {
            this.InitializeComponent();
            if (DesignMode.DesignModeEnabled) return;
            this.DataContextChanged += OnDataContextChanged;
        }

        private void OnDataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            var widget = this.DataContext as Widget;
            if (widget != null && widget.IsMapViewWidget())
            {
                var location = widget.Item.State;
                var zoom = widget.Height;
                var size = 800;
                var label = widget.Item.Label;
                if (label.Length > 3)
                {
                    label = "";
                }

                var url = string.Format(urlFormat,
                    bingMapsKey,
                    location,
                    zoom,
                    size,
                    label);

                this.theImage.Source = new BitmapImage(new Uri(url));
            }
        }
    }
}
