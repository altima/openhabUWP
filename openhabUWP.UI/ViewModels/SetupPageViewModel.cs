using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System.Xml.Linq;
using openhabUWP.Database;
using openhabUWP.Events;
using openhabUWP.Helper;
using openhabUWP.Remote.Services;
using Prism.Commands;
using Prism.Events;
using Prism.Windows.Mvvm;
using Prism.Windows.Navigation;

namespace openhabUWP.ViewModels
{
    public interface ISetupPageViewModel
    {
        bool IsDemoModeOn { get; set; }
        ObservableCollection<string> FoundServers { get; set; }
        Setup CurrentSetup { get; set; }
        bool IsOk { get; set; }
        bool IsFoundServerFlyoutVisible { get; set; }

        ICommand SearchCommand { get; set; }
        ICommand CheckCommand { get; set; }
        ICommand NextCommand { get; set; }
        ICommand FoundItemTappedCommand { get; set; }
    }

    public class SetupPageViewModel : ViewModelBase, ISetupPageViewModel
    {
        private IOpenhabDatabase _database;
        private INavigationService _navigationService;
        private IRestService _restService;
        private IEventAggregator _eventAggregator;

        private bool _isDemoModeOn;
        private ObservableCollection<string> _foundServers;
        private Setup _currentSetup;
        private bool _isOk;
        private bool _isFoundServerFlyoutVisible;

        public bool IsDemoModeOn
        {
            get { return _isDemoModeOn; }
            set
            {
                SetProperty(ref _isDemoModeOn, value);
                DemoMode();
            }
        }

        private void DemoMode()
        {
            if (IsDemoModeOn)
            {
                CurrentSetup.Url = "http://demo.openhab.org:9080";
                CurrentSetup.RemoteUrl = "http://demo.openhab.org:9080";
                OnPropertyChanged(() => CurrentSetup);
            }
            else
            {
                CurrentSetup.Url = "";
                CurrentSetup.RemoteUrl = "";
                CurrentSetup.Username = "";
                CurrentSetup.Password = "";
                OnPropertyChanged(() => CurrentSetup);
            }
        }

        public ObservableCollection<string> FoundServers
        {
            get { return _foundServers; }
            set { SetProperty(ref _foundServers, value); }
        }
        public Setup CurrentSetup
        {
            get { return _currentSetup; }
            set { SetProperty(ref _currentSetup, value); }
        }
        public bool IsOk
        {
            get { return _isOk; }
            set { SetProperty(ref _isOk, value); }
        }
        public bool IsFoundServerFlyoutVisible
        {
            get { return _isFoundServerFlyoutVisible; }
            set { SetProperty(ref _isFoundServerFlyoutVisible, value); }
        }

        public ICommand SearchCommand { get; set; }
        public ICommand CheckCommand { get; set; }
        public ICommand NextCommand { get; set; }
        public ICommand FoundItemTappedCommand { get; set; }

        public SetupPageViewModel(IOpenhabDatabase database, INavigationService navigationService, IRestService restService, IEventAggregator eventAggregator)
        {
            this._database = database;
            this._navigationService = navigationService;
            this._restService = restService;
            _eventAggregator = eventAggregator;

            SearchCommand = new DelegateCommand(Search);
            CheckCommand = new DelegateCommand(Check);
            NextCommand = new DelegateCommand(Next);
            FoundItemTappedCommand = new DelegateCommand<string>(ServerSelected);
        }

        private async void Search()
        {
            var servers = await _restService.FindLocalServersAsync();
            FoundServers = new ObservableCollection<string>(servers);

            if (FoundServers.Any()) IsFoundServerFlyoutVisible = true;
        }

        private async void Check()
        {
            IsOk = false;
            _restService.SetAuthentication(CurrentSetup.Username, CurrentSetup.Password);

            Uri uri;
            if (!CurrentSetup.Url.IsNullOrEmpty())
            {
                if (Uri.TryCreate(CurrentSetup.Url, UriKind.Absolute, out uri) &&
                    await _restService.Ping(CurrentSetup.Url))
                {
                    IsOk = true;
                }
                else
                {
                    /*show error*/
                    /*Please check the entered url, username and password and the connection to the server.*/
                }
            }
            if (!CurrentSetup.RemoteUrl.IsNullOrEmpty())
            {

                if (Uri.TryCreate(CurrentSetup.RemoteUrl, UriKind.Absolute, out uri) &&
                    await _restService.Ping(CurrentSetup.Url))
                {
                    IsOk = true;
                }
                else
                {
                    /*show error*/
                    /*Please check the entered url, username and password and the connection to the server.*/
                }
            }
        }

        private void Next()
        {
            _navigationService.Navigate(PageTokens.SitemapsPage, null);
        }

        public override void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState)
        {
            base.OnNavigatedTo(e, viewModelState);
            _eventAggregator.GetEvent<SetupEvents.ServerItemTappedEvent>().Unsubscribe(ServerSelected);
            _eventAggregator.GetEvent<SetupEvents.ServerItemTappedEvent>().Subscribe(ServerSelected);
            Load();
        }

        private void ServerSelected(string obj)
        {
            if (obj.IsNullOrEmpty()) return;

            if (this.CurrentSetup == null) this.CurrentSetup = new Setup();
            this.CurrentSetup.Url = obj;
            OnPropertyChanged(() => CurrentSetup);

            IsFoundServerFlyoutVisible = false;
        }

        public override void OnNavigatingFrom(NavigatingFromEventArgs e, Dictionary<string, object> viewModelState, bool suspending)
        {
            base.OnNavigatingFrom(e, viewModelState, suspending);
            if (!suspending)
            {
                _eventAggregator.GetEvent<SetupEvents.ServerItemTappedEvent>().Unsubscribe(ServerSelected);
                Save();
            }
        }

        private void Load()
        {
            CurrentSetup = _database.GetSetup();
        }

        private void Save()
        {
            if (IsDemoModeOn)
            {
                CurrentSetup.Url = "http://demo.openhab.org:9080";
                CurrentSetup.RemoteUrl = "http://demo.openhab.org:9080";
            }
            _database.UpdateSetup(CurrentSetup);
        }
    }

    namespace Design
    {
        public class SetupPageViewModel : ViewModelBase, ISetupPageViewModel
        {
            public bool IsDemoModeOn { get; set; }
            public ObservableCollection<string> FoundServers { get; set; }
            public Setup CurrentSetup { get; set; }
            public bool IsOk { get; set; }
            public bool IsFoundServerFlyoutVisible { get; set; }

            public ICommand SearchCommand { get; set; }
            public ICommand CheckCommand { get; set; }
            public ICommand NextCommand { get; set; }
            public ICommand FoundItemTappedCommand { get; set; }

            public SetupPageViewModel()
            {
                CurrentSetup = new Setup()
                {
                    Url = "http://192.168.178.3:8080",
                    RemoteUrl = "http://myremotehostname.tld:8080",
                    Username = "jdoe",
                    Password = "jdoe1234"
                };
            }
        }
    }
}