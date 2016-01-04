using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using Windows.Data.Json;
using Windows.Networking.Sockets;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Microsoft.Practices.ObjectBuilder2;
using openhabUWP.Events;
using openhabUWP.Interfaces;
using openhabUWP.Interfaces.Common;
using openhabUWP.Interfaces.Widgets;
using openhabUWP.Items;
using openhabUWP.Models;
using openhabUWP.Services;
using openhabUWP.Widgets;
using Prism.Events;
using Prism.Windows.Mvvm;
using Prism.Windows.Navigation;

namespace openhabUWP.ViewModels
{
    public interface IMainPageViewModel : IViewModel
    {
        string PageTitle { get; set; }
        ObservableCollection<IWidget> Widgets { get; set; }
    }

    public class MainPageViewModel : ViewModelBase, IMainPageViewModel
    {
        private readonly IRestService20 _restService;
        private readonly IEventAggregator _eventAggregator;

        private bool _isSpacerVisible;
        private string _pageTitle;
        private ObservableCollection<IWidget> _widgets;

        public bool IsSpacerVisible
        {
            get { return _isSpacerVisible; }
            set { SetProperty(ref _isSpacerVisible, value); }
        }

        public ObservableCollection<IWidget> Widgets
        {
            get { return _widgets; }
            set { SetProperty(ref _widgets, value); }
        }

        public string PageTitle
        {
            get { return _pageTitle; }
            set { SetProperty(ref _pageTitle, value); }
        }
        public MainPageViewModel(IEventAggregator eventAggregator, IRestService20 restService)
        {
            _eventAggregator = eventAggregator;
            _restService = restService;

            //Basic example of how we can use NavigationDisplayModeChangedEvent
            //to adjust things based on the navigation display mode.
            //A page header control that can be included on every page can also
            //be made, and the same technique can be used on that control.
            _eventAggregator.GetEvent<PubSubNames.NavigationDisplayModeChangedEvent>()
                .Subscribe(OnNavigationDisplayModeChangedEvent, true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="displayMode"></param>
        private void OnNavigationDisplayModeChangedEvent(SplitViewDisplayMode displayMode)
        {
            Debug.WriteLine(string.Format("MainPageViewModel.OnNavigationDisplayModeChangedEvent() - DisplayMode: {0}",
                displayMode));

            IsSpacerVisible = displayMode == SplitViewDisplayMode.Overlay;
        }


        public override async void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState)
        {
            _eventAggregator.GetEvent<PubSubNames.NavigatedToPageEvent>().Publish(e);

            if (e.NavigationMode == NavigationMode.New)
            {
                // load
            }
            else
            {
                // refresh
            }

            if (_restService != null)
            {
                var servers = await _restService.FindLocalServersAsync();
                if (servers.Length > 0)
                {
                    var server = servers.First();
                    if (await _restService.IsOpenhab2(server))
                    {
                        var sitemaps = await _restService.LoadSitemapsAsync(server);
                        if (sitemaps.Any())
                        {
                            var sitemap = await _restService.LoadSitemapDetailAsync(sitemaps.First());
                            CreatePage(sitemap.Homepage);
                        }
                    }
                    else
                    {

                    }
                }
            }
            base.OnNavigatedTo(e, viewModelState);
        }

        private void CreatePage(IPage page)
        {
            PageTitle = page.Title;
            Widgets = new ObservableCollection<IWidget>(page.Widgets);
        }

        private void OnDataReceived(string s)
        {
            Debug.WriteLine(s);
        }

        private void CallbackAction(MessageWebSocketMessageReceivedEventArgs args)
        {
            DataReader messageReader = args.GetDataReader();
            messageReader.UnicodeEncoding = UnicodeEncoding.Utf8;
            string messageString = messageReader.ReadString(messageReader.UnconsumedBufferLength);

            //{"widget":{"widgetId":"default_2_0_1","type":"Switch","label":"Apple TV","icon":"video","item":{"type":"SwitchItem","name":"AppleTV","state":"ON","link":"http://192.168.178.3:8080/rest/items/AppleTV"}}}

            var o = JsonObject.Parse(messageString);
            var widgetObject = o.GetNamedObject("widget");
            if (widgetObject != null)
            {
                IWidget widget = null;
                IItem item = null;
                var widgetId = widgetObject.GetNamedString("widgetId");
                var type = widgetObject.GetNamedString("type");
                var label = widgetObject.GetNamedString("label");
                var icon = widgetObject.GetNamedString("icon");
                var itemObject = widgetObject.GetNamedObject("item");
                string itemType = "", itemName = "", itemState = "", itemLink = "";


                if (itemObject != null)
                {
                    itemType = itemObject.GetNamedString("type");
                    itemName = itemObject.GetNamedString("name");
                    itemState = itemObject.GetNamedString("state");
                    itemLink = itemObject.GetNamedString("link");
                }

                switch (itemType)
                {
                    case "SwitchItem":
                        item = new SwitchItem(itemName, itemLink, itemState);
                        break;
                    default:
                        Debug.WriteLine("todo: {0}", itemType);
                        break;
                }

                switch (type)
                {
                    case "Switch":
                        widget = new SwitchWidget(widgetId, label, icon);
                        break;
                    case "Text":
                        widget = new TextWidget(widgetId, label, icon);
                        break;
                    case "Frame":
                        widget = new FrameWidget(widgetId, label, icon);
                        break;
                }


                if (widget != null)
                {
                    if (item != null)
                        widget.Item = item;
                    _eventAggregator.GetEvent<WidgetUpdateEvent>().Publish(widget);
                }

            }
        }

        public override void OnNavigatingFrom(NavigatingFromEventArgs e, Dictionary<string, object> viewModelState, bool suspending)
        {
            _eventAggregator.GetEvent<PubSubNames.NavigationDisplayModeChangedEvent>().Unsubscribe(OnNavigationDisplayModeChangedEvent);
            base.OnNavigatingFrom(e, viewModelState, suspending);
        }


    }

    namespace Design
    {
        public class MainPageViewModel : ViewModelBase, IMainPageViewModel
        {
            public string PageTitle { get; set; }
            public ObservableCollection<IWidget> Widgets { get; set; }

            public MainPageViewModel()
            {
                PageTitle = "UI Page 1";

                Widgets = new ObservableCollection<IWidget>()
                {
                    new FrameWidget("1", "Frame 1",""),
                    new FrameWidget("2", "Frame 2",""),

                    new SwitchWidget("3","Switch 1",""),
                    new SwitchWidget("4","Switch 2",""),

                    new TextWidget("5", "Text 1", ""),
                    new TextWidget("6", "Text 2", ""),
                };
            }

            public bool IsSpacerVisible { get; set; }
        }
    }
}
