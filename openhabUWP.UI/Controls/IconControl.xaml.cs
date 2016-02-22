using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.ApplicationModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Imaging;
using Microsoft.Practices.Unity;
using openhabUWP.Database;
using openhabUWP.Helper;

namespace openhabUWP.Controls
{
    public sealed partial class IconControl
    {
        private static DependencyProperty IconProperty = DependencyProperty.Register("Icon", typeof(string), typeof(IconControl), new PropertyMetadata(string.Empty, OnIconChanged));

        private static void OnIconChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as IconControl;
            if (control != null && e.NewValue != null && e.NewValue is string)
            {
                control.CheckImage();
            }
        }

        private IOpenhabDatabase _database;
        public string Icon
        {
            get { return (string)this.GetValue(IconProperty); }
            set { this.SetValue(IconProperty, value); }
        }

        public IconControl()
        {
            this.InitializeComponent();
            if (DesignMode.DesignModeEnabled) return;
            _database = App.Current.Container.Resolve<IOpenhabDatabase>();
        }

        private void CheckImage()
        {
            var setup = _database.GetSetup();
            var url1 = string.Concat(setup.Url, "/images/", Icon, ".png");
            var url2 = !setup.RemoteUrl.IsNullOrEmpty() ? string.Concat(setup.RemoteUrl, "/images/", Icon, ".png") : string.Empty;
            theImage.ImageFailed += (sender, args) =>
            {
                var bmp = theImage.Source as BitmapImage;
                if (bmp == null) return;
                var tmpUrl = bmp.UriSource.ToString();

                if (tmpUrl.IsNullOrEmpty()) return;
                if (url2.IsNullOrEmpty()) return;
                if (Equals(url1, url2)) return;
                if (Equals(tmpUrl, url1))
                    theImage.Source = new BitmapImage(new Uri(url2, UriKind.Absolute));
            };
            theImage.Source = new BitmapImage(new Uri(url1, UriKind.Absolute));
        }
    }
}
