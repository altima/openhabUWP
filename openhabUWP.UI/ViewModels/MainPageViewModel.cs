using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Windows.Data.Json;
using Windows.Networking.Sockets;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Microsoft.Practices.ObjectBuilder2;
using openhabUWP.Enums;
using openhabUWP.Events;
using openhabUWP.Helper;
using openhabUWP.Interfaces;
using openhabUWP.Interfaces.Common;
using openhabUWP.Interfaces.Services;
using openhabUWP.Interfaces.Widgets;
using openhabUWP.Items;
using openhabUWP.Models;
using openhabUWP.Services;
using openhabUWP.Widgets;
using Prism.Events;
using Prism.Windows.Mvvm;
using Prism.Windows.Navigation;
using Page = openhabUWP.Models.Page;

namespace openhabUWP.ViewModels
{
    public interface IMainPageViewModel : IViewModel
    {
        Page OpenhabPage { get; set; }

        Task LoadPage(Page page);
        void GoBack();
        bool CanGoBack();
    }

    public class MainPageViewModel : ViewModelBase, IMainPageViewModel
    {
        private readonly IRestService _restService;
        private readonly IEventAggregator _eventAggregator;

        private bool _isSpacerVisible;
        private Page _openhabPage;

        public bool IsSpacerVisible
        {
            get { return _isSpacerVisible; }
            set { SetProperty(ref _isSpacerVisible, value); }
        }

        public Page OpenhabPage
        {
            get { return _openhabPage; }
            set { SetProperty(ref _openhabPage, value); }
        }

        public MainPageViewModel(IEventAggregator eventAggregator, IRestService restService)
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

        private void OnNavigationDisplayModeChangedEvent(SplitViewDisplayMode displayMode)
        {
            Debug.WriteLine(string.Format("MainPageViewModel.OnNavigationDisplayModeChangedEvent() - DisplayMode: {0}",
                displayMode));

            IsSpacerVisible = displayMode == SplitViewDisplayMode.Overlay;
        }

        public override async void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState)
        {
            _eventAggregator.GetEvent<PubSubNames.NavigatedToPageEvent>().Publish(e);
            CheckServer();
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
                        _eventAggregator.GetEvent<WidgetUpdateEvent>().Publish(widget);
                        break;
                    case "Text":
                        widget = new TextWidget(widgetId, label, icon);
                        _eventAggregator.GetEvent<WidgetUpdateEvent>().Publish(widget);
                        break;
                    case "Frame":
                        widget = new FrameWidget(widgetId, label, icon);
                        _eventAggregator.GetEvent<WidgetUpdateEvent>().Publish(widget);
                        break;
                }
            }
        }

        public override void OnNavigatingFrom(NavigatingFromEventArgs e, Dictionary<string, object> viewModelState, bool suspending)
        {
            if (e.NavigationMode == NavigationMode.Back && CanGoBack())
            {
                e.Cancel = true;
                GoBack();
            }
            else
            {
                _eventAggregator.GetEvent<PubSubNames.NavigationDisplayModeChangedEvent>().Unsubscribe(OnNavigationDisplayModeChangedEvent);
            }
        }

        private async void CheckServer()
        {
            var server = new Server(host: "192.168.178.107"); //alpha mode, set server ip here
            var sitemaps = await _restService.LoadSitemapsAsync(server);
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
            OpenhabPage = await _restService.LoadPageAsync(page);
        }
        public async void GoBack()
        {
            if (CanGoBack())
            {
                await LoadPage(OpenhabPage.Parent);
            }
        }
        public bool CanGoBack()
        {
            if (OpenhabPage != null &&
                OpenhabPage.Parent != null &&
                !OpenhabPage.Parent.Link.IsNullOrEmpty())
            {
                return true;
            }


            return false;
        }

    }

    namespace Design
    {
        public class MainPageViewModel : ViewModelBase, IMainPageViewModel
        {
            public MainPageViewModel()
            {
                OpenhabPage = new Page("", "Default", "http://link.to", false, "firstfloor")
                {
                    Widgets = new IWidget[]
                    {
                        new FrameWidget("","Frame 1", ""),
                        new FrameWidget("","Frame 2", ""),
                        new FrameWidget("","Frame 3", ""),
                    }
                };
            }

            public bool IsSpacerVisible { get; set; }

            public Task LoadPage(Page page)
            {
                throw new NotImplementedException();
            }

            public void GoBack()
            {
                throw new NotImplementedException();
            }

            public bool CanGoBack()
            {
                throw new NotImplementedException();
            }

            public Page OpenhabPage { get; set; }
        }
    }
}
