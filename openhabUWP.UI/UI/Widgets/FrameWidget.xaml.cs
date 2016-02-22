using System.Diagnostics;
using Windows.UI.Xaml;
using Microsoft.Practices.Unity;
using openhabUWP.Events;
using openhabUWP.Remote.Models;
using Prism.Events;

namespace openhabUWP.UI.Widgets
{
    public sealed partial class FrameWidget : IWidgetControl
    {
        private Widget _widget;

        public FrameWidget()
        {
            this.InitializeComponent();
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
            App.Current.Container.Resolve<IEventAggregator>().GetEvent<WidgetEvents.WidgetUpdateEvent>().Unsubscribe(WidgetUpdateReceived);
            App.Current.Container.Resolve<IEventAggregator>().GetEvent<WidgetEvents.WidgetUpdateEvent>().Subscribe(WidgetUpdateReceived);
        }

        public void WidgetUpdateReceived(Widget widget)
        {
            if (Equals(widget.WidgetId, _widget.WidgetId))
            {
                Debug.WriteLine("FrameWidget Udpate {0}", widget.WidgetId);
            }
        }
    }
}
