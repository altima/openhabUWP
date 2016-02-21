using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Resources;
using Windows.Foundation;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Microsoft.Practices.Unity;
using openhabUWP.Database;
using openhabUWP.Remote.Services;
using openhabUWP.Services;
using Prism.Windows.AppModel;

namespace openhabUWP
{
    sealed partial class App
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="App"/> class.
        /// </summary>
        public App()
        {
            this.InitializeComponent();
        }
        
        /// <summary>
        /// Override this method with the initialization logic of your application. Here you can initialize services, repositories, and so on.
        /// </summary>
        /// <param name="args">The <see cref="T:Windows.ApplicationModel.Activation.IActivatedEventArgs" /> instance containing the event data.</param>
        /// <returns></returns>
        protected override Task OnInitializeAsync(IActivatedEventArgs args)
        {
            return base.OnInitializeAsync(args);
        }

        /// <summary>
        /// Override this method with logic that will be performed after the application is initialized. For example, navigating to the application's home page.
        /// </summary>
        /// <param name="args">The <see cref="T:Windows.ApplicationModel.Activation.LaunchActivatedEventArgs" /> instance containing the event data.</param>
        /// <returns></returns>
        protected override Task OnLaunchApplicationAsync(LaunchActivatedEventArgs args)
        {
            ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size(320, 320));

            NavigationService.Navigate(PageTokens.SplashPage, null);

            return Task.FromResult<object>(null);
        }

        /// <summary>
        /// Creates and configures the container and service locator
        /// </summary>
        protected override void ConfigureContainer()
        {
            Container.RegisterInstance<IResourceLoader>(new ResourceLoaderAdapter(new ResourceLoader()));
            Container.RegisterType<ILogService, LogService>();
            Container.RegisterType<IParserService, ParserService>();
            Container.RegisterType<IRestService, RestService>();
            Container.RegisterType<IPushClientService, PushClientService>();
            Container.RegisterType<IOpenhabDatabase, OpenhabDatabase>();

            base.ConfigureContainer();
        }
    }
}
