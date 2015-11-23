using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Windows.Networking.Sockets;
using Windows.UI.Xaml.Controls;
using openhabUWP.Events;
using openhabUWP.Interfaces.Services;
using Prism.Events;
using Prism.Windows.Mvvm;
using Prism.Windows.Navigation;

namespace openhabUWP.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private MessageWebSocket _socket;
        private readonly IRestService _restService;
        private readonly IEventAggregator _eventAggregator;

        private bool _isSpacerVisible;

        public bool IsSpacerVisible
        {
            get { return _isSpacerVisible; }
            set { SetProperty(ref _isSpacerVisible, value); }
        }

        public MainPageViewModel(IRestService restService, IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _restService = restService;

            //Basic example of how we can use NavigationDisplayModeChangedEvent
            //to adjust things based on the navigation display mode.
            //A page header control that can be included on every page can also
            //be made, and the same technique can be used on that control.
            _eventAggregator.GetEvent<PubSubNames.NavigationDisplayModeChangedEvent>().Subscribe(OnNavigationDisplayModeChangedEvent, true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="displayMode"></param>
        private void OnNavigationDisplayModeChangedEvent(SplitViewDisplayMode displayMode)
        {
            Debug.WriteLine(string.Format("MainPageViewModel.OnNavigationDisplayModeChangedEvent() - DisplayMode: {0}", displayMode));

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
                        _socket = await _restService.RegisterWebSocketAsync(sitemap.Homepage, CallbackAction);
                    }
                }
            }
            base.OnNavigatedTo(e, viewModelState);
        }

        private void CallbackAction(MessageWebSocketMessageReceivedEventArgs messageWebSocketMessageReceivedEventArgs)
        {

        }

        public override void OnNavigatingFrom(NavigatingFromEventArgs e, Dictionary<string, object> viewModelState, bool suspending)
        {
            _eventAggregator.GetEvent<PubSubNames.NavigationDisplayModeChangedEvent>().Unsubscribe(OnNavigationDisplayModeChangedEvent);
            _socket?.Close(1000, "navigation from page");
            base.OnNavigatingFrom(e, viewModelState, suspending);
        }
    }
}
