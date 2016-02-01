using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.UI.ViewManagement;
using openhabUWP.Database;
using openhabUWP.Remote.Services;
using openhabUWP.Services;

namespace openhabUWP
{
    sealed partial class App
    {
        public App()
        {
            this.InitializeComponent();
        }
        
        protected override Task OnLaunchApplicationAsync(LaunchActivatedEventArgs args)
        {
            ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size(320, 320));

            NavigationService.Navigate("Main", null);

            return Task.FromResult<object>(null);
        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            this.RegisterTypeIfMissing(typeof(ILogService), typeof(LogService), true);
            this.RegisterTypeIfMissing(typeof(IParserService), typeof(ParserService), true);
            this.RegisterTypeIfMissing(typeof(IRestService), typeof(RestService), true);
            this.RegisterTypeIfMissing(typeof(IPushClientService), typeof(PushClientService), true);
            this.RegisterTypeIfMissing(typeof(IOpenhabDatabase), typeof(OpenhabDatabase), true);

            //this.RegisterTypeIfMissing(typeof(IMainPageViewModel), typeof(MainPageViewModel), false);
        }
    }
}
