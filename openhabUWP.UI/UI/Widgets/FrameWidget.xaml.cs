using System.Diagnostics;
using Windows.UI.Xaml;
using Microsoft.Practices.Unity;
using openhabUWP.Events;
using Prism.Events;

namespace openhabUWP.UI.Widgets
{
    public sealed partial class FrameWidget : IWidget
    {
        private openhabUWP.Widgets.FrameWidget _widget;

        public FrameWidget()
        {
            this.InitializeComponent();
            this.DataContextChanged += OnDataContextChanged;
        }

        private void OnDataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            if (args.NewValue is openhabUWP.Widgets.FrameWidget)
                RegisterForUpdate();
        }

        public void RegisterForUpdate()
        {
            _widget = this.DataContext as openhabUWP.Widgets.FrameWidget;
            App.Current.Container.Resolve<IEventAggregator>().GetEvent<WidgetUpdateEvent>().Unsubscribe(WidgetUpdateReceived);
            App.Current.Container.Resolve<IEventAggregator>().GetEvent<WidgetUpdateEvent>().Subscribe(WidgetUpdateReceived);
        }

        public void WidgetUpdateReceived(Interfaces.Widgets.IWidget widget)
        {
            if (Equals(widget.WidgetId, _widget.WidgetId))
            {
                Debug.WriteLine("FrameWidget Udpate {0}", widget.WidgetId);
            }
        }
    }
}
