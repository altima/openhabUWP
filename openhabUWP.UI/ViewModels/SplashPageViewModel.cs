using System.Collections.Generic;
using System.Threading.Tasks;
using openhabUWP.Database;
using openhabUWP.Helper;
using Prism.Events;
using Prism.Windows.AppModel;
using Prism.Windows.Mvvm;
using Prism.Windows.Navigation;

namespace openhabUWP.ViewModels
{
    public interface ISplashPageViewModel
    {

    }

    public class SplashPageViewModel : ViewModelBase, ISplashPageViewModel
    {
        private INavigationService _navigationService;
        private IOpenhabDatabase _database;
        private IEventAggregator _eventAggregator;
        private IResourceLoader _resourceLoader;


        public SplashPageViewModel(INavigationService navigationService, IOpenhabDatabase database, IEventAggregator eventAggregator, IResourceLoader resourceLoader)
        {
            _navigationService = navigationService;
            _database = database;
            _eventAggregator = eventAggregator;
            _resourceLoader = resourceLoader;
        }

        public override void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState)
        {
            base.OnNavigatedTo(e, viewModelState);
            Check();
        }

        private async void Check()
        {
            await Task.Delay(1500);
            var setup = _database.GetSetup();
            if (setup == null || setup.Url.IsNullOrEmpty())
            {
                StartSetup();
            }
            else if (!setup.Url.IsNullOrEmpty() && setup.Sitemap.IsNullOrEmpty())
            {
                StartSitemap();
            }
            else
            {
                StartOpenhab();
            }
        }

        private void StartSetup()
        {
            // inform user to setup openhab instance and
            // select a sitemap
            _navigationService.ClearHistory();
            _navigationService.Navigate(PageTokens.SetupPage, null);
        }

        private void StartSitemap()
        {
            // inform user to setup openhab instance and
            // select a sitemap
            _navigationService.ClearHistory();
            _navigationService.Navigate(PageTokens.SitemapsPage, null);
        }

        private void StartOpenhab()
        {
            _navigationService.ClearHistory();
            _navigationService.Navigate(PageTokens.HomePage, null);
        }


        public override void OnNavigatingFrom(NavigatingFromEventArgs e, Dictionary<string, object> viewModelState, bool suspending)
        {
            base.OnNavigatingFrom(e, viewModelState, suspending);
        }
    }

    namespace Design
    {
        public class SplashPageViewModel : ViewModelBase, ISplashPageViewModel
        {

        }
    }
}
