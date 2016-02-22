using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;
using Microsoft.Practices.Unity;
using openhabUWP.Events;
using openhabUWP.Models;
using openhabUWP.Remote.Models;
using Prism.Events;

namespace openhabUWP.UI.Widgets
{
    public sealed partial class SwitchWidget : IWidgetControl
    {
        private Widget _widget;
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
            var widget = args.NewValue as Widget;
            if (widget != null)
            {
                RegisterForUpdate();
                if (widget.Item != null)
                {
                    this.toggleSwitch.IsOn = widget.Item.State == "ON";
                }
            }

        }

        public void SwitchToggled()
        {
            if (_widget != null) _eventAggregator.GetEvent<WidgetEvents.WidgetTappedEvent>().Publish(_widget);
        }

        public void RegisterForUpdate()
        {
            _widget = this.DataContext as Widget;
            _eventAggregator.GetEvent<WidgetEvents.WidgetUpdateEvent>().Unsubscribe(WidgetUpdateReceived);
            _eventAggregator.GetEvent<WidgetEvents.WidgetUpdateEvent>().Subscribe(WidgetUpdateReceived);
        }

        public void WidgetUpdateReceived(Widget widget)
        {
            if (Equals(widget.WidgetId, _widget.WidgetId))
            {
                this.DataContext = widget;
            }
        }

        private void ToggleSwitch_OnToggled(object sender, RoutedEventArgs e)
        {
            SwitchToggled();
        }
    }
}
