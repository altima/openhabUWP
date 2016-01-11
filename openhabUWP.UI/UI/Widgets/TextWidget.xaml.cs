using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;
using Microsoft.Practices.Unity;
using openhabUWP.Events;
using Prism.Events;

namespace openhabUWP.UI.Widgets
{
    public sealed partial class TextWidget : IWidget
    {
        private IEventAggregator _eventAggregator;
        

        private const string LabelValuePattern = @"(?<label>.+)\s(\[(?<value>.+)\])$";
        private openhabUWP.Widgets.TextWidget _widget;

        public TextWidget()
        {
            this.InitializeComponent();
            _eventAggregator = App.Current.Container.Resolve<IEventAggregator>();
            this.DataContextChanged += OnDataContextChanged;
            this.Tapped += OnTapped;
        }

        private void OnTapped(object sender, TappedRoutedEventArgs tappedRoutedEventArgs)
        {
            if (_widget != null) _eventAggregator.GetEvent<WidgetEvents.WidgetTappedEvent>().Publish(_widget);
        }

        private void OnDataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            if (args.NewValue is openhabUWP.Widgets.TextWidget)
                RegisterForUpdate();
        }

        public void RegisterForUpdate()
        {
            _widget = this.DataContext as openhabUWP.Widgets.TextWidget;
            _eventAggregator.GetEvent<WidgetEvents.WidgetUpdateEvent>().Unsubscribe(WidgetUpdateReceived);
            _eventAggregator.GetEvent<WidgetEvents.WidgetUpdateEvent>().Subscribe(WidgetUpdateReceived);
        }

        public void WidgetUpdateReceived(Interfaces.Widgets.IWidget widget)
        {
            if (Equals(_widget.WidgetId, widget.WidgetId))
            {
                this.DataContext = widget;
                Debug.WriteLine("TextWidget Udpate {0}", widget.WidgetId);
            }
        }
    }
}
