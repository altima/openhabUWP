using System;
using Windows.ApplicationModel;
using Windows.UI.Xaml.Input;
using Microsoft.Practices.Unity;
using openhabUWP.Events;
using openhabUWP.Remote.Models;
using Prism.Events;

namespace openhabUWP.Controls
{
    public sealed partial class SitemapListItem
    {
        private IEventAggregator _eventAggregator;

        public SitemapListItem()
        {
            this.InitializeComponent();
            if (DesignMode.DesignModeEnabled) return;
            _eventAggregator = App.Current.Container.Resolve<IEventAggregator>();
            this.Tapped += OnTapped;
        }

        private void OnTapped(object sender, TappedRoutedEventArgs tappedRoutedEventArgs)
        {
            var sitemap = this.DataContext as Sitemap;
            if (sitemap == null) return;
            _eventAggregator.GetEvent<SitemapEvents.TappedEvent>().Publish(sitemap);
        }
    }
}
