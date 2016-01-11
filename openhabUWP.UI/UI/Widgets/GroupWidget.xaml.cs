using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Microsoft.Practices.Unity;
using openhabUWP.Events;
using Prism.Events;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace openhabUWP.UI.Widgets
{
    public sealed partial class GroupWidget : UserControl
    {
        private openhabUWP.Widgets.GroupWidget _widget;
        private IEventAggregator _eventAggregator;

        public GroupWidget()
        {
            this.InitializeComponent();
            _eventAggregator = App.Current.Container.Resolve<IEventAggregator>();
            this.Tapped += OnTapped;
            this.DataContextChanged += OnDataContextChanged;
        }

        private void OnDataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            if (args.NewValue is openhabUWP.Widgets.GroupWidget)
                RegisterForUpdate();
        }

        private void RegisterForUpdate()
        {
            _widget = this.DataContext as openhabUWP.Widgets.GroupWidget;
            _eventAggregator.GetEvent<WidgetEvents.WidgetUpdateEvent>().Unsubscribe(WidgetUpdateReceived);
            _eventAggregator.GetEvent<WidgetEvents.WidgetUpdateEvent>().Subscribe(WidgetUpdateReceived);
        }

        public void WidgetUpdateReceived(Interfaces.Widgets.IWidget widget)
        {
            if (Equals(_widget.WidgetId, widget.WidgetId))
            {
                this.DataContext = widget;
                Debug.WriteLine("GroupWidget Udpate {0}", widget.WidgetId);
            }
        }


        private void OnTapped(object sender, TappedRoutedEventArgs e)
        {
            if (_widget != null) _eventAggregator.GetEvent<WidgetEvents.WidgetTappedEvent>().Publish(_widget);
        }
    }
}
