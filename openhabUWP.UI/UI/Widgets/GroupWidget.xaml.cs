using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;
using Microsoft.Practices.Unity;
using openhabUWP.Events;
using openhabUWP.Remote.Models;
using Prism.Events;

namespace openhabUWP.UI.Widgets
{
    public sealed partial class GroupWidget : IWidgetControl
    {
        private Widget _widget;
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
                Debug.WriteLine("GroupWidget Udpate {0}", widget.WidgetId);
            }
        }


        private void OnTapped(object sender, TappedRoutedEventArgs e)
        {
            if (_widget != null) _eventAggregator.GetEvent<WidgetEvents.WidgetTappedEvent>().Publish(_widget);
        }
    }
}
