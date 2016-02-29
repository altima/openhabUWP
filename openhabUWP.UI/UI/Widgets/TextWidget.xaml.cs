using System.Diagnostics;
using Windows.ApplicationModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;
using Microsoft.Practices.Unity;
using openhabUWP.Events;
using openhabUWP.Models;
using openhabUWP.Remote.Models;
using Prism.Events;

namespace openhabUWP.UI.Widgets
{
    public sealed partial class TextWidget : IWidgetControl
    {
        private IEventAggregator _eventAggregator;
        
        private Widget _widget;

        public TextWidget()
        {
            this.InitializeComponent();
            if(DesignMode.DesignModeEnabled) return;
            _eventAggregator = App.Current.Container.Resolve<IEventAggregator>();
            this.DataContextChanged += OnDataContextChanged;
            this.Tapped += OnTapped;
        }

        private void OnTapped(object sender, TappedRoutedEventArgs tappedRoutedEventArgs)
        {
            if (_widget != null) _eventAggregator.GetEvent<WidgetEvents.WidgetTappedEvent>().Publish(new SwitchWidgetButtonTappedArgs(_widget));
        }

        private void OnDataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            if (args.NewValue is Widget)
                RegisterForUpdate();
        }

        public void RegisterForUpdate()
        {
            _widget = this.DataContext as Widget;
            _eventAggregator.GetEvent<WidgetEvents.WidgetUpdateEvent>().Unsubscribe(WidgetUpdateReceived);
            _eventAggregator.GetEvent<WidgetEvents.WidgetUpdateEvent>().Subscribe(WidgetUpdateReceived);
        }

        public void WidgetUpdateReceived(Widget widget)
        {
            if (Equals(_widget.WidgetId, widget.WidgetId))
            {
                this.DataContext = widget;
                Debug.WriteLine("TextWidget Udpate {0}", widget.WidgetId);
            }
        }
    }
}
