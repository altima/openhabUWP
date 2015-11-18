using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using openhabUWP.Interfaces.Services;
using openhabUWP.Services;

namespace openhabUWP.UI
{
    sealed partial class App
    {

        public App()
        {
            this.InitializeComponent();
        }

        protected override Task OnLaunchApplicationAsync(LaunchActivatedEventArgs args)
        {
            NavigationService.Navigate("Main", null);
            return Task.FromResult<object>(null);
        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            this.RegisterTypeIfMissing(typeof(ILogService), typeof(LogService), false);
            this.RegisterTypeIfMissing(typeof(IParserService), typeof(ParserService), false);
            this.RegisterTypeIfMissing(typeof(IRestService), typeof(RestService), true);
        }
    }
}
