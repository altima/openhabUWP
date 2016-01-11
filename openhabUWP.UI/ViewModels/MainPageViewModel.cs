using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Data.Json;
using Windows.Networking.Sockets;
using Windows.Storage.Streams;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Microsoft.Practices.ObjectBuilder2;
using openhabUWP.Enums;
using openhabUWP.Events;
using openhabUWP.Helper;
using openhabUWP.Interfaces;
using openhabUWP.Interfaces.Common;
using openhabUWP.Interfaces.Items;
using openhabUWP.Interfaces.Services;
using openhabUWP.Interfaces.Widgets;
using openhabUWP.Items;
using openhabUWP.Models;
using openhabUWP.Services;
using openhabUWP.Widgets;
using Prism.Commands;
using Prism.Events;
using Prism.Windows.AppModel;
using Prism.Windows.Mvvm;
using Prism.Windows.Navigation;
using Page = openhabUWP.Models.Page;

namespace openhabUWP.ViewModels
{
    public interface IMainPageViewModel
    {
        Page CurrentPage { get; set; }
        Server CurrentServer { get; set; }

        Task LoadPage(Page page);
        void GoUp();
        bool CanGoUp { get; set; }
    }

    public class MainPageViewModel : ViewModelBase, IMainPageViewModel
    {
        private readonly IRestService _restService;
        private readonly IEventAggregator _eventAggregator;
        private readonly INavigationService _navigationService;
        private readonly IDeviceGestureService _gestureService;
        private IPushClientService _pushClientService;

        private bool _canGetUp;
        private Page _currentPage;
        private Server _currentServer;

        public bool CanGoUp
        {
            get { return _canGetUp; }
            set
            {
                SetProperty(ref _canGetUp, value);
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = value ?
                    AppViewBackButtonVisibility.Visible :
                    AppViewBackButtonVisibility.Collapsed;
            }
        }
        public Page CurrentPage
        {
            get { return _currentPage; }
            set
            {
                SetProperty(ref _currentPage, value);
                CanGoUp = CurrentPage?.Parent != null;
            }
        }
        public Server CurrentServer
        {
            get { return _currentServer; }
            set { SetProperty(ref _currentServer, value); }
        }

        public MainPageViewModel(IEventAggregator eventAggregator, IRestService restService, IDeviceGestureService gestureService, INavigationService navigationService, IPushClientService pushClientService)
        {
            _eventAggregator = eventAggregator;
            _restService = restService;
            _gestureService = gestureService;
            _navigationService = navigationService;
            _pushClientService = pushClientService;

            _gestureService.GoBackRequested += GestureServiceOnGoBackRequested;
        }

        private void GestureServiceOnGoBackRequested(object sender, DeviceGestureEventArgs args)
        {
            if (CanGoUp)
            {
                args.Cancel = args.Handled = true;
                GoUp();
            }
        }

        public override void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState)
        {
            _eventAggregator.GetEvent<WidgetEvents.WidgetTappedEvent>().Subscribe(WidgetTapped);
            CheckServer();
        }

        private async void WidgetTapped(IWidget widget)
        {
            switch (widget.Type)
            {
                case "Switch":
                    var switchWidget = (SwitchWidget)widget;
                    if (switchWidget.Item is NumberItem)
                    {
                        var numberItem = switchWidget.Item as NumberItem;
                        var mappings = switchWidget.Mappings;
                    }
                    else
                    {
                        var switchItem = (SwitchItem)switchWidget.Item;
                        var currentState = switchItem.State;
                        await _restService.PostCommand(switchItem, currentState ? "OFF" : "ON");
                    }
                    break;
                case "Text":
                case "Group":
                    if (widget.LinkedPage != null &&
                        !widget.LinkedPage.Link.IsNullOrEmpty())
                    {
                        await LoadPage(widget.LinkedPage);
                    }
                    break;
            }

        }

        private async void OnDataReceived(string s)
        {
            if (_needPagePush)
            {
                //openhab1, response is page, resart pooling
                if (!s.IsNullOrEmpty())
                {
                    var page = JsonObject.Parse(s).ToPage();
                    if (page != null)
                    {
                        if (Equals(CurrentPage.Id, page.Id))
                        {
                            CurrentPage = page;
                        }
                        _pushClientService.PoolForEvent(page.Link, OnDataReceived);
                    }
                }
                else
                {
                    _pushClientService.PoolForEvent(CurrentPage.Link, OnDataReceived);
                }
            }
            else
            {
                //openhab2
            }
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
                        _eventAggregator.GetEvent<WidgetEvents.WidgetUpdateEvent>().Publish(widget);
                        break;
                    case "Text":
                        widget = new TextWidget(widgetId, label, icon);
                        _eventAggregator.GetEvent<WidgetEvents.WidgetUpdateEvent>().Publish(widget);
                        break;
                    case "Frame":
                        widget = new FrameWidget(widgetId, label, icon);
                        _eventAggregator.GetEvent<WidgetEvents.WidgetUpdateEvent>().Publish(widget);
                        break;
                }
            }
        }

        public override void OnNavigatingFrom(NavigatingFromEventArgs e, Dictionary<string, object> viewModelState, bool suspending)
        {
            _eventAggregator.GetEvent<WidgetEvents.WidgetTappedEvent>().Unsubscribe(WidgetTapped);
        }

        private async void CheckServer()
        {
            //var server = new Server(host: "192.168.178.107"); //alpha mode, set server ip here
            CurrentServer = new Server(host: "192.168.178.107"); //alpha mode, set server ip here

            CheckPush(CurrentServer);

            var sitemaps = await _restService.LoadSitemapsAsync(CurrentServer);
            if (sitemaps.Any())
            {
                var firstSitemap = sitemaps.FirstOrDefault();
                if (firstSitemap != null)
                {
                    firstSitemap = await _restService.LoadSitemapAsync(firstSitemap);
                    await LoadPage(firstSitemap.Homepage);
                }
            }

        }

        public async Task LoadPage(Page page)
        {
            CurrentPage = await _restService.LoadPageAsync(page);
            if (_needPagePush)
            {
                _pushClientService.PoolForEvent(CurrentPage.Link, OnDataReceived);
            }
        }

        public async void GoUp()
        {
            if (CanGoUp)
            {
                await LoadPage(CurrentPage.Parent);
            }
        }

        private bool _needPagePush = false;
        private async void CheckPush(Server server)
        {
            _needPagePush = !(await _restService.IsOpenhab2(server));
            if (!_needPagePush && !_pushClientService.PushChannelAttached)
            {
                _pushClientService.AttachToEvents(server.Link + "/events", onDataReceived: OnDataReceived);
            }
        }
    }

    namespace Design
    {
        public class MainPageViewModel : ViewModelBase, IMainPageViewModel
        {
            public MainPageViewModel()
            {
                CurrentPage = new Page("", "Default", "http://link.to", false, "firstfloor")
                {
                    Widgets = new IWidget[]
                    {
                        new FrameWidget("","Frame 1", ""),
                        new FrameWidget("","Frame 2", ""),
                        new FrameWidget("","Frame 3", ""),
                    }
                };
            }

            public Server CurrentServer { get; set; }

            public Task LoadPage(Page page)
            {
                throw new NotImplementedException();
            }

            public void GoUp()
            {
                throw new NotImplementedException();
            }

            public bool CanGoUp { get; set; }

            public Page CurrentPage { get; set; }
        }
    }
}
