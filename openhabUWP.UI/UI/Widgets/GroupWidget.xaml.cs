using System;
using System.Collections.Generic;
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
        private openhabUWP.Widgets.TextWidget _widget;
        private IEventAggregator _eventAggregator;

        public GroupWidget()
        {
            this.InitializeComponent();
            _eventAggregator = App.Current.Container.Resolve<IEventAggregator>();
            this.Tapped += OnTapped;
        }

        
        private void OnTapped(object sender, TappedRoutedEventArgs e)
        {
            if (_widget != null) _eventAggregator.GetEvent<WidgetEvents.WidgetTappedEvent>().Publish(_widget);
        }
    }
}
