using System.Collections.ObjectModel;
using Prism.Commands;
using Prism.Windows.AppModel;
using Prism.Windows.Mvvm;
using Prism.Windows.Navigation;

namespace openhabUWP.ViewModels
{
    public class MenuViewModel : ViewModelBase
    {
        private INavigationService _navigationService;
        private bool _canNavigateToMain = false;
        private bool _canNavigateToConenctionSetup = true;

        public MenuViewModel(INavigationService navigationService, IResourceLoader resourceLoader)
        {
            _navigationService = navigationService;

            Commands = new ObservableCollection<MenuItemViewModel>
            {
                new MenuItemViewModel { DisplayName = resourceLoader.GetString("MenuView_btnMain"), FontIcon = "\ue10f", Command = new DelegateCommand(NavigateToMainPage, CanNavigateToMainPage) },
                new MenuItemViewModel { DisplayName = resourceLoader.GetString("MenuView_btnConnectionSetup"), FontIcon = "\ue115", Command = new DelegateCommand(NavigateToSettings, CanNavigateToSettings) },
            };
        }

        public ObservableCollection<MenuItemViewModel> Commands { get; set; }

        /*MainPage*/
        private void NavigateToMainPage()
        {
            if (CanNavigateToMainPage())
            {
                if (_navigationService.Navigate(PageTokens.MainPage, null))
                {
                    _canNavigateToMain = false;
                    _canNavigateToConenctionSetup = true;
                    RaiseCanExecuteChanged();
                }
            }
        }

        private bool CanNavigateToMainPage()
        {
            return _canNavigateToMain;
        }

        /*SettingsPage*/
        private void NavigateToSettings()
        {
            if (CanNavigateToSettings())
            {
                if (_navigationService.Navigate(PageTokens.SettingsPage, null))
                {
                    _canNavigateToMain = true;
                    _canNavigateToConenctionSetup = false;
                    RaiseCanExecuteChanged();
                }
            }
        }

        private bool CanNavigateToSettings()
        {
            return _canNavigateToConenctionSetup;
        }

        /*Event*/
        private void RaiseCanExecuteChanged()
        {
            foreach (var item in Commands)
            {
                (item.Command as DelegateCommand).RaiseCanExecuteChanged();
            }
        }
    }
}