using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using Windows.Data.Json;
using Windows.Networking.Sockets;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Controls;
using Newtonsoft.Json;
using openhabUWP.Events;
using openhabUWP.Interfaces.Common;
using openhabUWP.Interfaces.Services;
using openhabUWP.Interfaces.Widgets;
using openhabUWP.Items;
using openhabUWP.Widgets;
using Prism.Events;
using Prism.Windows.Mvvm;
using Prism.Windows.Navigation;

namespace openhabUWP.ViewModels
{
    public interface IMainPageViewModel : IViewModel
    {
        ObservableCollection<IWidget> Widgets { get; set; }
    }

    public class MainPageViewModel : ViewModelBase, IMainPageViewModel
    {
        private MessageWebSocket _socket;
        private readonly IRestService _restService;
        private readonly IEventAggregator _eventAggregator;

        private bool _isSpacerVisible;
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

        public MainPageViewModel(IRestService restService, IEventAggregator eventAggregator)
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
            if (_restService != null)
            {
                var server = await _restService.FindLocalServersAsync();
                if (server.Length > 0)
                {
                    var openhabLinks = await _restService.LoadOpenhabLinksAsync(server.First());
                    var sitemaps = await _restService.LoadSitemapsAsync(openhabLinks);
                    if (sitemaps != null && sitemaps.Maps != null && sitemaps.Maps.Length > 0)
                    {
                        var sitemap = await _restService.LoadSitemapDetailsAsync(sitemaps.Maps.First());
                        CreatePage(sitemap.Homepage);
                    }
                }
            }
            base.OnNavigatedTo(e, viewModelState);
        }

        private async void CreatePage(IPage page)
        {
            Widgets = new ObservableCollection<IWidget>(page.Widgets);
            _socket = await _restService.RegisterWebSocketAsync(page, CallbackAction);
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

        public override void OnNavigatingFrom(NavigatingFromEventArgs e, Dictionary<string, object> viewModelState,
            bool suspending)
        {
            _eventAggregator.GetEvent<PubSubNames.NavigationDisplayModeChangedEvent>()
                .Unsubscribe(OnNavigationDisplayModeChangedEvent);
            _socket?.Close(1000, "navigation from page");
            base.OnNavigatingFrom(e, viewModelState, suspending);
        }


    }

    namespace Design
    {
        public class MainPageViewModel : ViewModelBase, IMainPageViewModel
        {
            public ObservableCollection<IWidget> Widgets { get; set; }

            public MainPageViewModel()
            {
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
