using System;
using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Microsoft.Practices.Unity;
using openhabUWP.Events;
using Prism.Events;

namespace openhabUWP.UI.Widgets
{
    public sealed partial class TextWidget : UserControl, IWidget
    {
        private openhabUWP.Widgets.TextWidget _widget;

        public TextWidget()
        {
            this.InitializeComponent();
            this.DataContextChanged += OnDataContextChanged;
        }

        private void OnDataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            if (args.NewValue is openhabUWP.Widgets.TextWidget)
                RegisterForUpdate();
        }

        public void RegisterForUpdate()
        {
            _widget = this.DataContext as openhabUWP.Widgets.TextWidget;
            App.Current.Container.Resolve<IEventAggregator>().GetEvent<WidgetUpdateEvent>().Unsubscribe(WidgetUpdateReceived);
            App.Current.Container.Resolve<IEventAggregator>().GetEvent<WidgetUpdateEvent>().Subscribe(WidgetUpdateReceived);
        }

        public void WidgetUpdateReceived(Interfaces.Widgets.IWidget widget)
        {
            if (Equals(_widget.Id, widget.Id))
            {
                Debug.WriteLine("TextWidget Udpate {0}", widget.Id);
            }
        }
    }
}
