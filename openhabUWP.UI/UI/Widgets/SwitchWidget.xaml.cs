using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Microsoft.Practices.Unity;
using openhabUWP.Annotations;
using openhabUWP.Events;
using openhabUWP.Helper;
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
        private IRestService _restService;


        public SwitchWidget()
        {
            this.InitializeComponent();
            this.DataContextChanged += OnDataContextChanged;
            _restService = App.Current.Container.Resolve<IRestService>();
        }

        private void OnDataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            if (args.NewValue == null)
            {
                _widget = null;
                DeregisterForUpdate();
            }
            else
            {
                _widget = args.NewValue as openhabUWP.Widgets.SwitchWidget;
                if (!IsRegisteredForUpdate())
                {
                    DeregisterForUpdate();
                    RegisterForUpdate();
                }
            }
        }

        private bool IsRegisteredForUpdate()
        {
            return App.Current.Container.Resolve<IEventAggregator>().GetEvent<WidgetUpdateEvent>().Contains(WidgetUpdateReceived);
        }

        public void RegisterForUpdate()
        {
            App.Current.Container.Resolve<IEventAggregator>().GetEvent<WidgetUpdateEvent>().Subscribe(WidgetUpdateReceived, ThreadOption.UIThread);
        }
        public void DeregisterForUpdate()
        {
            App.Current.Container.Resolve<IEventAggregator>().GetEvent<WidgetUpdateEvent>().Unsubscribe(WidgetUpdateReceived);
        }

        public void WidgetUpdateReceived(Interfaces.Widgets.IWidget widget)
        {
            if (Equals(widget.Id, _widget.Id))
            {
                remoteUpdate = true;
                this.DataContext = widget;
                SwitchToggled();
                remoteUpdate = false;
            }
        }

        private bool remoteUpdate = false;
        public async void SwitchToggled()
        {
            var link = (this.DataContext as ISwitchWidget)?.Item?.Link;
            var state = ((this.DataContext as ISwitchWidget)?.Item as SwitchItem)?.State;
            if (toggleSwitch.IsOn != state && remoteUpdate)
            {
                if (state != null)
                {
                    toggleSwitch.IsOn = (bool)state;
                }
            }

            if (!link.IsNullOrEmpty())
            {
                if (toggleSwitch.IsOn)
                {
                    await _restService.PostCommand(link, "ON");
                }
                else
                {
                    await _restService.PostCommand(link, "OFF");
                }
            }
        }
    }
}
