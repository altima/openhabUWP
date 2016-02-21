using System.Collections.Generic;
using System.Collections.ObjectModel;
using openhabUWP.Database;
using openhabUWP.Events;
using openhabUWP.Helper;
using openhabUWP.Remote.Models;
using openhabUWP.Remote.Services;
using Prism.Events;
using Prism.Windows.AppModel;
using Prism.Windows.Mvvm;
using Prism.Windows.Navigation;

namespace openhabUWP.ViewModels
{
    public interface ISitemapsPageViewModel
    {
        Setup CurrentSetup { get; set; }
        ObservableCollection<Sitemap> Sitemaps { get; set; }
    }

    public class SitemapsPageViewModel : ViewModelBase, ISitemapsPageViewModel
    {
        private IResourceLoader _resourceLoader;
        private INavigationService _navigationService;
        private IOpenhabDatabase _database;
        private IEventAggregator _eventAggregator;
        private IRestService _restService;

        private Setup _currentSetup;
        ObservableCollection<Sitemap> _sitemaps = new ObservableCollection<Sitemap>();

        public Setup CurrentSetup
        {
            get { return _currentSetup; }
            set { SetProperty(ref _currentSetup, value); }
        }
        public ObservableCollection<Sitemap> Sitemaps
        {
            get { return _sitemaps; }
            set { SetProperty(ref _sitemaps, value); }
        }

        public SitemapsPageViewModel(IResourceLoader resourceLoader, INavigationService navigationService, IOpenhabDatabase database, IEventAggregator eventAggregator, IRestService restService)
        {
            this._navigationService = navigationService;
            this._resourceLoader = resourceLoader;
            this._database = database;
            this._eventAggregator = eventAggregator;
            this._restService = restService;
        }

        public override void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState)
        {
            base.OnNavigatedTo(e, viewModelState);
            CurrentSetup = _database.GetSetup();

            _eventAggregator.GetEvent<SitemapEvents.TappedEvent>().Unsubscribe(SitemapTapped);
            _eventAggregator.GetEvent<SitemapEvents.TappedEvent>().Subscribe(SitemapTapped);

            LoadSitemaps();
        }

        private async void LoadSitemaps()
        {
            string url = string.Empty;
            if (!(CurrentSetup.Url.IsNullOrEmpty()) && await _restService.Ping(CurrentSetup.Url))
            {
                url = CurrentSetup.Url;
            }
            else if (!(CurrentSetup.Url.IsNullOrEmpty()) && await _restService.Ping(CurrentSetup.RemoteUrl))
            {
                url = CurrentSetup.RemoteUrl;
            }
            else
            {
                //error?! open setup page
                _navigationService.ClearHistory();
                _navigationService.Navigate(PageTokens.SetupPage, null);
                return;
            }
            var sitemaps = await _restService.LoadSitemapsAsync(url);
            Sitemaps = new ObservableCollection<Sitemap>(sitemaps);
        }

        private void SitemapTapped(Sitemap obj)
        {
            if (obj == null) return;

            CurrentSetup.Sitemap = obj.Name;
            _database.UpdateSetup(CurrentSetup);

            _navigationService.ClearHistory();
            _navigationService.Navigate(PageTokens.HomePage, null);
        }

        public override void OnNavigatingFrom(NavigatingFromEventArgs e, Dictionary<string, object> viewModelState, bool suspending)
        {
            base.OnNavigatingFrom(e, viewModelState, suspending);
            if (suspending) return;
            _eventAggregator.GetEvent<SitemapEvents.TappedEvent>().Unsubscribe(SitemapTapped);
        }
    }

    namespace Design
    {

        public class SitemapsPageViewModel : ViewModelBase, ISitemapsPageViewModel
        {
            public Setup CurrentSetup { get; set; }
            public ObservableCollection<Sitemap> Sitemaps { get; set; }
        }
    }
}
