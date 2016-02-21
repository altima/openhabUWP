using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
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
using Prism.Commands;
using Prism.Events;
using Prism.Windows.AppModel;
using Prism.Windows.Mvvm;
using Prism.Windows.Navigation;
using Page = openhabUWP.Remote.Models.Page;

namespace openhabUWP.ViewModels
{
    public interface IHomePageViewModel
    {
        Page CurrentPage { get; set; }
        Setup Setup { get; set; }

        ICommand ShowSitemapsCommand { get; set; }
        ICommand ShowSetupCommand { get; set; }
        ICommand ShowInfoCommand { get; set; }
    }

    public class HomePageViewModel : ViewModelBase, IHomePageViewModel
    {
        private readonly IRestService _restService;
        private readonly IEventAggregator _eventAggregator;
        private readonly INavigationService _navigationService;
        private readonly IPushClientService _pushClientService;
        private readonly IOpenhabDatabase _database;

        private Page _currentPage;
        private Setup _setup;

        public Page CurrentPage
        {
            get { return _currentPage; }
            set
            {
                SetProperty(ref _currentPage, value);
            }
        }
        public Setup Setup
        {
            get { return _setup; }
            set { SetProperty(ref _setup, value); }
        }

        public ICommand ShowSitemapsCommand { get; set; }
        public ICommand ShowSetupCommand { get; set; }
        public ICommand ShowInfoCommand { get; set; }

        public HomePageViewModel(IEventAggregator eventAggregator, IRestService restService, INavigationService navigationService,
            IPushClientService pushClientService, IOpenhabDatabase database)
        {
            _eventAggregator = eventAggregator;
            _restService = restService;
            _navigationService = navigationService;
            _pushClientService = pushClientService;
            _database = database;

            ShowSitemapsCommand = new DelegateCommand(ShowSitemaps);
            ShowSetupCommand = new DelegateCommand(ShowSetup);
            ShowInfoCommand = new DelegateCommand(ShowInfo);
        }

        private void ShowSitemaps()
        {
            _navigationService.Navigate(PageTokens.SitemapsPage, null);
        }
        private void ShowSetup()
        {
            _navigationService.Navigate(PageTokens.SetupPage, null);
        }
        private void ShowInfo()
        {
            _navigationService.Navigate(PageTokens.InfoPage, null);
        }

        public override void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState)
        {
            Setup = _database.GetSetup();
            string pageName = "";
            _eventAggregator.GetEvent<WidgetEvents.WidgetTappedEvent>().Subscribe(WidgetTapped);
            if (e.Parameter != null && e.Parameter is string)
            {
                pageName = e.Parameter as string;
            }
            Load(pageName);
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
                        await _restService.PostCommand(widget.Item.Link, currentState ? "OFF" : "ON");
                    }
                    break;
                case "Text":
                case "Group":
                    if (widget.LinkedPage != null &&
                        !widget.LinkedPage.Link.IsNullOrEmpty())
                    {
                        _navigationService.Navigate(PageTokens.HomePage, widget.LinkedPage.Id);
                    }
                    break;
            }

        }

        public override void OnNavigatingFrom(NavigatingFromEventArgs e, Dictionary<string, object> viewModelState, bool suspending)
        {
            if (suspending) return;
            _eventAggregator.GetEvent<WidgetEvents.WidgetTappedEvent>().Unsubscribe(WidgetTapped);
        }

        private async void Load(string pageName)
        {
            //var url = Setup.RemoteUrl.IsNullOrEmpty() ? Setup.Url : Setup.RemoteUrl;
            var url = Setup.Url;
            var sitemapName = Setup.Sitemap;

            if (pageName.IsNullOrEmpty())
            {
                //homepage
                var sitemap = await _restService.LoadSitemapAsync(url, sitemapName);
                pageName = sitemap.Homepage.Id;
            }
            CurrentPage = await _restService.LoadPageAsync(url, sitemapName, pageName);
        }
    }

    namespace Design
    {
        public class MainPageViewModel : ViewModelBase, IHomePageViewModel
        {
            public MainPageViewModel()
            {
                CurrentPage =
                    JsonObject.Parse(
                        "{\"id\": \"demo\",\"title\": \"Main Menu\",\"link\": \"http://demo.openhab.org:9080/rest/sitemaps/demo/demo\",\"leaf\": true,\"widgets\": [{\"widgetId\": \"demo_0\",\"type\": \"Frame\",\"label\": \"\",\"icon\": \"frame\",\"mappings\": [],\"widgets\": [{\"widgetId\": \"demo_0_0\",\"type\": \"Group\",\"label\": \"First Floor\",\"icon\": \"firstfloor\",\"mappings\": [],\"item\": {\"members\": [],\"link\": \"http://demo.openhab.org:9080/rest/items/gFF\",\"state\": \"UNDEF\",\"type\": \"GroupItem\",\"name\": \"gFF\",\"label\": \"First Floor\",\"category\": \"firstfloor\",\"tags\": [\"home-group\"],\"groupNames\": []},\"linkedPage\": {\"id\": \"0000\",\"title\": \"First Floor\",\"icon\": \"firstfloor\",\"link\": \"http://demo.openhab.org:9080/rest/sitemaps/demo/0000\",\"leaf\": false},\"widgets\": []},{\"widgetId\": \"demo_0_0_1\",\"type\": \"Group\",\"label\": \"Ground Floor\",\"icon\": \"groundfloor\",\"mappings\": [],\"item\": {\"members\": [],\"link\": \"http://demo.openhab.org:9080/rest/items/gGF\",\"state\": \"UNDEF\",\"type\": \"GroupItem\",\"name\": \"gGF\",\"label\": \"Ground Floor\",\"category\": \"groundfloor\",\"tags\": [\"home-group\"],\"groupNames\": []},\"linkedPage\": {\"id\": \"0001\",\"title\": \"Ground Floor\",\"icon\": \"groundfloor\",\"link\": \"http://demo.openhab.org:9080/rest/sitemaps/demo/0001\",\"leaf\": false},\"widgets\": []},{\"widgetId\": \"demo_0_0_1_2\",\"type\": \"Group\",\"label\": \"Cellar\",\"icon\": \"cellar\",\"mappings\": [],\"item\": {\"members\": [],\"link\": \"http://demo.openhab.org:9080/rest/items/gC\",\"state\": \"UNDEF\",\"type\": \"GroupItem\",\"name\": \"gC\",\"label\": \"Cellar\",\"category\": \"cellar\",\"tags\": [\"home-group\"],\"groupNames\": []},\"linkedPage\": {\"id\": \"0002\",\"title\": \"Cellar\",\"icon\": \"cellar\",\"link\": \"http://demo.openhab.org:9080/rest/sitemaps/demo/0002\",\"leaf\": false},\"widgets\": []},{\"widgetId\": \"demo_0_0_1_2_3\",\"type\": \"Group\",\"label\": \"Garden\",\"icon\": \"garden\",\"mappings\": [],\"item\": {\"members\": [],\"link\": \"http://demo.openhab.org:9080/rest/items/Garden\",\"state\": \"UNDEF\",\"type\": \"GroupItem\",\"name\": \"Garden\",\"label\": \"Garden\",\"category\": \"garden\",\"tags\": [\"home-group\"],\"groupNames\": []},\"linkedPage\": {\"id\": \"0003\",\"title\": \"Garden\",\"icon\": \"garden\",\"link\": \"http://demo.openhab.org:9080/rest/sitemaps/demo/0003\",\"leaf\": false},\"widgets\": []}]},{\"widgetId\": \"demo_1\",\"type\": \"Frame\",\"label\": \"Weather\",\"icon\": \"frame\",\"mappings\": [],\"widgets\": [{\"widgetId\": \"demo_1_0\",\"type\": \"Text\",\"label\": \"Sun Elevation [75.00 Â°]\",\"icon\": \"sun\",\"mappings\": [],\"item\": {\"link\": \"http://demo.openhab.org:9080/rest/items/Sun_Elevation\",\"state\": \"75\",\"stateDescription\": {\"pattern\": \"%.2f Â°\",\"readOnly\": true,\"options\": []},\"type\": \"NumberItem\",\"name\": \"Sun_Elevation\",\"label\": \"Sun Elevation\",\"category\": \"sun\",\"tags\": [],\"groupNames\": [\"Weather\"]},\"widgets\": []},{\"widgetId\": \"demo_1_0_1\",\"type\": \"Text\",\"label\": \"Outside Temperature [11.0 Â°C]\",\"icon\": \"temperature\",\"valuecolor\": \"lightgray\",\"mappings\": [],\"item\": {\"link\": \"http://demo.openhab.org:9080/rest/items/Weather_Temperature\",\"state\": \"11\",\"stateDescription\": {\"pattern\": \"%.1f Â°C\",\"readOnly\": true,\"options\": []},\"type\": \"NumberItem\",\"name\": \"Weather_Temperature\",\"label\": \"Outside Temperature\",\"category\": \"temperature\",\"tags\": [],\"groupNames\": [\"Weather\",\"Weather_Chart\"]},\"linkedPage\": {\"id\": \"0101\",\"title\": \"Outside Temperature [11.0 Â°C]\",\"icon\": \"temperature\",\"link\": \"http://demo.openhab.org:9080/rest/sitemaps/demo/0101\",\"leaf\": false},\"widgets\": []}]},{\"widgetId\": \"demo_2\",\"type\": \"Frame\",\"label\": \"Demo\",\"icon\": \"frame\",\"mappings\": [],\"widgets\": [{\"widgetId\": \"demo_2_0\",\"type\": \"Text\",\"label\": \"Date [Tuesday, 02.02.2016]\",\"icon\": \"calendar\",\"mappings\": [],\"item\": {\"link\": \"http://demo.openhab.org:9080/rest/items/CurrentDate\",\"state\": \"2016-02-02T13:00:22\",\"stateDescription\": {\"pattern\": \"%1$tA, %1$td.%1$tm.%1$tY\",\"readOnly\": false,\"options\": []},\"type\": \"DateTimeItem\",\"name\": \"CurrentDate\",\"label\": \"Date\",\"category\": \"calendar\",\"tags\": [],\"groupNames\": []},\"widgets\": []},{\"widgetId\": \"demo_2_0_1\",\"type\": \"Text\",\"label\": \"Group Demo\",\"icon\": \"firstfloor\",\"mappings\": [],\"linkedPage\": {\"id\": \"0201\",\"title\": \"Group Demo\",\"icon\": \"firstfloor\",\"link\": \"http://demo.openhab.org:9080/rest/sitemaps/demo/0201\",\"leaf\": false},\"widgets\": []},{\"widgetId\": \"demo_2_0_1_2\",\"type\": \"Text\",\"label\": \"Widget Overview\",\"icon\": \"chart\",\"mappings\": [],\"linkedPage\": {\"id\": \"0202\",\"title\": \"Widget Overview\",\"icon\": \"chart\",\"link\": \"http://demo.openhab.org:9080/rest/sitemaps/demo/0202\",\"leaf\": false},\"widgets\": []},{\"widgetId\": \"demo_2_0_1_2_3\",\"type\": \"Text\",\"label\": \"Multimedia\",\"icon\": \"video\",\"mappings\": [],\"linkedPage\": {\"id\": \"0203\",\"title\": \"Multimedia\",\"icon\": \"video\",\"link\": \"http://demo.openhab.org:9080/rest/sitemaps/demo/0203\",\"leaf\": false},\"widgets\": []}]}]}")
                        .ToPage();
            }

            public Page CurrentPage { get; set; }
            public Setup Setup { get; set; }

            public ICommand ShowSitemapsCommand { get; set; }
            public ICommand ShowSetupCommand { get; set; }
            public ICommand ShowInfoCommand { get; set; }
        }
    }
}
