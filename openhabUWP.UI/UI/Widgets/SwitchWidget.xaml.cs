using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Microsoft.Practices.Unity;
using openhabUWP.Annotations;
using openhabUWP.Events;
using openhabUWP.Helper;
using openhabUWP.Interfaces;
using openhabUWP.Interfaces.Common;
using openhabUWP.Interfaces.Services;
using openhabUWP.Interfaces.Widgets;
using openhabUWP.Items;
using Prism.Events;

namespace openhabUWP.UI.Widgets
{
    public sealed partial class SwitchWidget : UserControl, IWidget
    {
        private openhabUWP.Widgets.SwitchWidget _widget;
        private IEventAggregator _eventAggregator;

        public SwitchWidget()
        {
            this.InitializeComponent();
            this.DataContextChanged += OnDataContextChanged;
            this.Tapped += OnTapped;
            this.toggleSwitch.Tapped += OnTapped;
            _eventAggregator = App.Current.Container.Resolve<IEventAggregator>();
        }

        private void OnTapped(object sender, TappedRoutedEventArgs tappedRoutedEventArgs)
        {
            SwitchToggled();
        }

        private void OnDataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            if (args.NewValue is openhabUWP.Widgets.SwitchWidget)
                RegisterForUpdate();
        }

        public void SwitchToggled()
        {
            if (_widget != null) _eventAggregator.GetEvent<WidgetEvents.WidgetTappedEvent>().Publish(_widget);
        }

        public void RegisterForUpdate()
        {
            _widget = this.DataContext as openhabUWP.Widgets.SwitchWidget;
            _eventAggregator.GetEvent<WidgetEvents.WidgetUpdateEvent>().Unsubscribe(WidgetUpdateReceived);
            _eventAggregator.GetEvent<WidgetEvents.WidgetUpdateEvent>().Subscribe(WidgetUpdateReceived);
        }

        public void WidgetUpdateReceived(Interfaces.Widgets.IWidget widget)
        {
            if (Equals(widget.WidgetId, _widget.WidgetId))
            {
                this.DataContext = widget;
            }
        }
    }
}
