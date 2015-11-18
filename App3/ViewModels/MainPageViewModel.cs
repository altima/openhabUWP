using System.Collections.Generic;
using System.Linq;
using openhabUWP.Interfaces.Services;
using Prism.Windows.Mvvm;
using Prism.Windows.Navigation;

namespace openhabUWP.UI.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private IRestService _restService;

        public MainPageViewModel(IRestService restService)
        {
            _restService = restService;
        }

        public override async void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState)
        {
            base.OnNavigatedTo(e, viewModelState);

            var servers = await _restService.FindLocalServersAsync();
            var openhab = await _restService.LoadOpenhabLinksAsync(servers.First());

            var items = await _restService.LoadItemsAsync(openhab);

            var sitemaps = await _restService.LoadSitemapsAsync(openhab);
            foreach (var map in sitemaps.Maps)
            {
                var sitemap = await _restService.LoadSitemapDetailsAsync(map);
            }
        }

    }
}
