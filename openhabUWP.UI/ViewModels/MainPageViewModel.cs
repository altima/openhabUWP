using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.Data.Json;
using Windows.Networking.Sockets;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Core;
using openhabUWP.Database;
using openhabUWP.Events;
using openhabUWP.Helper;
using openhabUWP.Models;
using openhabUWP.Remote;
using openhabUWP.Remote.Models;
using openhabUWP.Remote.Services;
using openhabUWP.Services;
using Prism.Events;
using Prism.Windows.AppModel;
using Prism.Windows.Mvvm;
using Prism.Windows.Navigation;
using Page = openhabUWP.Remote.Models.Page;

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
        private readonly IPushClientService _pushClientService;
        private IOpenhabDatabase _databaseService;

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

        public MainPageViewModel(IEventAggregator eventAggregator, IRestService restService, IDeviceGestureService gestureService, INavigationService navigationService,
            IPushClientService pushClientService, IOpenhabDatabase databaseService)
        {
            _eventAggregator = eventAggregator;
            _restService = restService;
            _gestureService = gestureService;
            _navigationService = navigationService;
            _pushClientService = pushClientService;
            _databaseService = databaseService;

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

        private async void WidgetTapped(Widget widget)
        {
            switch (widget.Type)
            {
                case "Switch":
                    if (widget.Item != null && widget.Item.Type == "Number")
                    {
                        var numberItem = widget.Item;
                        var mappings = widget.Mappings;
                    }
                    else
                    {
                        var currentState = widget.Item.State == "ON";
                        await _restService.PostCommand(widget.Item, currentState ? "OFF" : "ON");
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

        private void OnDataReceived(string s)
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
                Widget widget = null;
                Item item = null;
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

                //switch (itemType)
                //{
                //    case "SwitchItem":
                //        item = new SwitchItem(itemName, itemLink, itemState);
                //        break;
                //    default:
                //        Debug.WriteLine("todo: {0}", itemType);
                //        break;
                //}

                //switch (type)
                //{
                //    case "Switch":
                //        widget = new SwitchWidgetControl(widgetId, label, icon);
                //        _eventAggregator.GetEvent<WidgetEvents.WidgetUpdateEvent>().Publish(widget);
                //        break;
                //    case "Text":
                //        widget = new TextWidgetControl(widgetId, label, icon);
                //        _eventAggregator.GetEvent<WidgetEvents.WidgetUpdateEvent>().Publish(widget);
                //        break;
                //    case "Frame":
                //        widget = new FrameWidgetControl(widgetId, label, icon);
                //        _eventAggregator.GetEvent<WidgetEvents.WidgetUpdateEvent>().Publish(widget);
                //        break;
                //}
            }
        }

        public override void OnNavigatingFrom(NavigatingFromEventArgs e, Dictionary<string, object> viewModelState, bool suspending)
        {
            _eventAggregator.GetEvent<WidgetEvents.WidgetTappedEvent>().Unsubscribe(WidgetTapped);
        }

        private async void CheckServer()
        {
            var setup = _databaseService.GetSetup();
            if (setup.Url.IsNullOrEmpty() && setup.RemoteUrl.IsNullOrEmpty()) return;

            var url = setup.RemoteUrl.IsNullOrEmpty() ? setup.Url : setup.RemoteUrl;

            CurrentServer = new Server(url);

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
                CurrentPage =
                    JsonObject.Parse(
                        "{\"id\": \"demo\",\"title\": \"Main Menu\",\"link\": \"http://demo.openhab.org:9080/rest/sitemaps/demo/demo\",\"leaf\": true,\"widgets\": [{\"widgetId\": \"demo_0\",\"type\": \"Frame\",\"label\": \"\",\"icon\": \"frame\",\"mappings\": [],\"widgets\": [{\"widgetId\": \"demo_0_0\",\"type\": \"Group\",\"label\": \"First Floor\",\"icon\": \"firstfloor\",\"mappings\": [],\"item\": {\"members\": [],\"link\": \"http://demo.openhab.org:9080/rest/items/gFF\",\"state\": \"UNDEF\",\"type\": \"GroupItem\",\"name\": \"gFF\",\"label\": \"First Floor\",\"category\": \"firstfloor\",\"tags\": [\"home-group\"],\"groupNames\": []},\"linkedPage\": {\"id\": \"0000\",\"title\": \"First Floor\",\"icon\": \"firstfloor\",\"link\": \"http://demo.openhab.org:9080/rest/sitemaps/demo/0000\",\"leaf\": false},\"widgets\": []},{\"widgetId\": \"demo_0_0_1\",\"type\": \"Group\",\"label\": \"Ground Floor\",\"icon\": \"groundfloor\",\"mappings\": [],\"item\": {\"members\": [],\"link\": \"http://demo.openhab.org:9080/rest/items/gGF\",\"state\": \"UNDEF\",\"type\": \"GroupItem\",\"name\": \"gGF\",\"label\": \"Ground Floor\",\"category\": \"groundfloor\",\"tags\": [\"home-group\"],\"groupNames\": []},\"linkedPage\": {\"id\": \"0001\",\"title\": \"Ground Floor\",\"icon\": \"groundfloor\",\"link\": \"http://demo.openhab.org:9080/rest/sitemaps/demo/0001\",\"leaf\": false},\"widgets\": []},{\"widgetId\": \"demo_0_0_1_2\",\"type\": \"Group\",\"label\": \"Cellar\",\"icon\": \"cellar\",\"mappings\": [],\"item\": {\"members\": [],\"link\": \"http://demo.openhab.org:9080/rest/items/gC\",\"state\": \"UNDEF\",\"type\": \"GroupItem\",\"name\": \"gC\",\"label\": \"Cellar\",\"category\": \"cellar\",\"tags\": [\"home-group\"],\"groupNames\": []},\"linkedPage\": {\"id\": \"0002\",\"title\": \"Cellar\",\"icon\": \"cellar\",\"link\": \"http://demo.openhab.org:9080/rest/sitemaps/demo/0002\",\"leaf\": false},\"widgets\": []},{\"widgetId\": \"demo_0_0_1_2_3\",\"type\": \"Group\",\"label\": \"Garden\",\"icon\": \"garden\",\"mappings\": [],\"item\": {\"members\": [],\"link\": \"http://demo.openhab.org:9080/rest/items/Garden\",\"state\": \"UNDEF\",\"type\": \"GroupItem\",\"name\": \"Garden\",\"label\": \"Garden\",\"category\": \"garden\",\"tags\": [\"home-group\"],\"groupNames\": []},\"linkedPage\": {\"id\": \"0003\",\"title\": \"Garden\",\"icon\": \"garden\",\"link\": \"http://demo.openhab.org:9080/rest/sitemaps/demo/0003\",\"leaf\": false},\"widgets\": []}]},{\"widgetId\": \"demo_1\",\"type\": \"Frame\",\"label\": \"Weather\",\"icon\": \"frame\",\"mappings\": [],\"widgets\": [{\"widgetId\": \"demo_1_0\",\"type\": \"Text\",\"label\": \"Sun Elevation [75.00 Â°]\",\"icon\": \"sun\",\"mappings\": [],\"item\": {\"link\": \"http://demo.openhab.org:9080/rest/items/Sun_Elevation\",\"state\": \"75\",\"stateDescription\": {\"pattern\": \"%.2f Â°\",\"readOnly\": true,\"options\": []},\"type\": \"NumberItem\",\"name\": \"Sun_Elevation\",\"label\": \"Sun Elevation\",\"category\": \"sun\",\"tags\": [],\"groupNames\": [\"Weather\"]},\"widgets\": []},{\"widgetId\": \"demo_1_0_1\",\"type\": \"Text\",\"label\": \"Outside Temperature [11.0 Â°C]\",\"icon\": \"temperature\",\"valuecolor\": \"lightgray\",\"mappings\": [],\"item\": {\"link\": \"http://demo.openhab.org:9080/rest/items/Weather_Temperature\",\"state\": \"11\",\"stateDescription\": {\"pattern\": \"%.1f Â°C\",\"readOnly\": true,\"options\": []},\"type\": \"NumberItem\",\"name\": \"Weather_Temperature\",\"label\": \"Outside Temperature\",\"category\": \"temperature\",\"tags\": [],\"groupNames\": [\"Weather\",\"Weather_Chart\"]},\"linkedPage\": {\"id\": \"0101\",\"title\": \"Outside Temperature [11.0 Â°C]\",\"icon\": \"temperature\",\"link\": \"http://demo.openhab.org:9080/rest/sitemaps/demo/0101\",\"leaf\": false},\"widgets\": []}]},{\"widgetId\": \"demo_2\",\"type\": \"Frame\",\"label\": \"Demo\",\"icon\": \"frame\",\"mappings\": [],\"widgets\": [{\"widgetId\": \"demo_2_0\",\"type\": \"Text\",\"label\": \"Date [Tuesday, 02.02.2016]\",\"icon\": \"calendar\",\"mappings\": [],\"item\": {\"link\": \"http://demo.openhab.org:9080/rest/items/CurrentDate\",\"state\": \"2016-02-02T13:00:22\",\"stateDescription\": {\"pattern\": \"%1$tA, %1$td.%1$tm.%1$tY\",\"readOnly\": false,\"options\": []},\"type\": \"DateTimeItem\",\"name\": \"CurrentDate\",\"label\": \"Date\",\"category\": \"calendar\",\"tags\": [],\"groupNames\": []},\"widgets\": []},{\"widgetId\": \"demo_2_0_1\",\"type\": \"Text\",\"label\": \"Group Demo\",\"icon\": \"firstfloor\",\"mappings\": [],\"linkedPage\": {\"id\": \"0201\",\"title\": \"Group Demo\",\"icon\": \"firstfloor\",\"link\": \"http://demo.openhab.org:9080/rest/sitemaps/demo/0201\",\"leaf\": false},\"widgets\": []},{\"widgetId\": \"demo_2_0_1_2\",\"type\": \"Text\",\"label\": \"Widget Overview\",\"icon\": \"chart\",\"mappings\": [],\"linkedPage\": {\"id\": \"0202\",\"title\": \"Widget Overview\",\"icon\": \"chart\",\"link\": \"http://demo.openhab.org:9080/rest/sitemaps/demo/0202\",\"leaf\": false},\"widgets\": []},{\"widgetId\": \"demo_2_0_1_2_3\",\"type\": \"Text\",\"label\": \"Multimedia\",\"icon\": \"video\",\"mappings\": [],\"linkedPage\": {\"id\": \"0203\",\"title\": \"Multimedia\",\"icon\": \"video\",\"link\": \"http://demo.openhab.org:9080/rest/sitemaps/demo/0203\",\"leaf\": false},\"widgets\": []}]}]}")
                        .ToPage();
            }

            public Server CurrentServer { get; set; }

            public async Task LoadPage(Page page) { }

            public void GoUp()
            {
                throw new NotImplementedException();
            }

            public bool CanGoUp { get; set; }

            public Page CurrentPage { get; set; }
        }
    }
}
