using System.Collections.Generic;
using System.Xml.Linq;
using openhabUWP.Database;
using Prism.Windows.Mvvm;
using Prism.Windows.Navigation;

namespace openhabUWP.ViewModels
{
    public interface IConnectionSetupPageViewModel
    {
        Setup CurrentSetup { get; set; }
    }

    public class SettingsPageViewModel : ViewModelBase, IConnectionSetupPageViewModel
    {
        private IOpenhabDatabase _database;
        private INavigationService _navigationService;

        private Setup _currentSetup;

        public Setup CurrentSetup
        {
            get { return _currentSetup; }
            set { SetProperty(ref _currentSetup, value); }
        }

        public SettingsPageViewModel(IOpenhabDatabase database, INavigationService navigationService)
        {
            this._database = database;
            this._navigationService = navigationService;
        }

        public override void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState)
        {
            base.OnNavigatedTo(e, viewModelState);
            Load();
        }

        public override void OnNavigatingFrom(NavigatingFromEventArgs e, Dictionary<string, object> viewModelState, bool suspending)
        {
            base.OnNavigatingFrom(e, viewModelState, suspending);
            if (!suspending)
            {
                Save();
            }
        }

        private void Load()
        {
            CurrentSetup = _database.GetSetup();
        }

        private void Save()
        {
            _database.UpdateSetup(CurrentSetup);
        }
    }

    namespace Design
    {
        public class SettingsPageViewModel : ViewModelBase, IConnectionSetupPageViewModel
        {
            public Setup CurrentSetup { get; set; }

            public SettingsPageViewModel()
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