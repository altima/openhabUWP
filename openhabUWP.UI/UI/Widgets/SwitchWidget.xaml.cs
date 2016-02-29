using System;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Microsoft.Practices.Unity;
using openhabUWP.Events;
using openhabUWP.Helper;
using openhabUWP.Remote.Models;
using Prism.Events;

namespace openhabUWP.UI.Widgets
{
    public sealed partial class SwitchWidget : IWidgetControl
    {
        private bool _canToggle = false;
        private Widget _widget;
        private IEventAggregator _eventAggregator;

        public SwitchWidget()
        {
            this.InitializeComponent();
            this.DataContextChanged += OnDataContextChanged;
            this.Tapped += OnTapped;
            this.toggleSwitch.Toggled += OnToggled;
            _eventAggregator = App.Current.Container.Resolve<IEventAggregator>();
            _eventAggregator.GetEvent<PushEvents.PushEvent>().Subscribe(OnPushEvent);
        }

        private void OnPushEvent(EventData obj)
        {
            //"smarthome/items/Weather_LastUpdate/state"

            if (_widget == null) return;
            if (_widget.Item == null) return;
            if (_widget.Item.Name.IsNullOrEmpty()) return;
            if (_widget.Item.Name.IsNullOrEmpty()) return;

            var itemName = _widget.Item.Name;
            var name = string.Empty;

            var match = Regex.Match(obj.Topic, @".+\/.+\/(?<name>.+)\/.+");
            if (match.Success)
            {
                name = match.Groups["name"].Value;
            }

            if (name.IsNullOrEmpty()) return;
            if (!Equals(itemName, name))
            {
                _widget.Item.State = obj.Payload.Value;
                this.WidgetUpdateReceived(_widget);
            }
        }

        private void OnToggled(object sender, RoutedEventArgs e)
        {
            if (_canToggle)
            {
                SwitchToggled(this.toggleSwitch.IsOn ? "ON" : "OFF");
            }
        }

        private void OnTapped(object sender, TappedRoutedEventArgs e)
        {
            bool widgetHasButtons = _widget.Mappings.Any();

            string command = "";

            if (widgetHasButtons)
            {
                if (sender is Button)
                {
                    var button = sender as Button;
                    command = button.Tag as string;
                }
            }
            else
            {
                command = this.toggleSwitch.IsOn ? "OFF" : "ON";
            }
            SwitchToggled(command);
        }

        private async void OnDataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            var widget = args.NewValue as Widget;
            if (widget != null && widget.IsSwitchWidget())
            {
                RegisterForUpdate();
                if (widget.Item != null)
                {
                    var onOff = new[] { "ON", "OFF" };
                    if (onOff.Contains(widget.Item.State))
                    {
                        this.toggleSwitch.IsOn = widget.Item.State == "ON";
                    }
                }
            }
            _canToggle = true;
        }

        public void SwitchToggled(string command = "")
        {
            _canToggle = false;
            if (_widget != null && !command.IsNullOrEmpty())
            {
                _eventAggregator.GetEvent<WidgetEvents.WidgetTappedEvent>().Publish(new SwitchWidgetButtonTappedArgs(_widget, command));
            }
        }

        public void RegisterForUpdate()
        {
            _eventAggregator.GetEvent<WidgetEvents.WidgetUpdateEvent>().Unsubscribe(WidgetUpdateReceived);
            var tmpWidget = this.DataContext as Widget;
            if (!Equals(tmpWidget, _widget)) _widget = tmpWidget;
            _eventAggregator.GetEvent<WidgetEvents.WidgetUpdateEvent>().Subscribe(WidgetUpdateReceived);
        }

        public async void WidgetUpdateReceived(Widget widget)
        {
            if (this.Dispatcher.HasThreadAccess)
            {
                if (Equals(widget.WidgetId, _widget.WidgetId))
                {
                    await Task.Delay(100); // need a little delay
                    this.DataContext = widget;
                }
            }
            else
            {
                await this.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => WidgetUpdateReceived(widget));
            }
        }
    }
}
