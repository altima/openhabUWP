using System.ComponentModel;
using Windows.UI.Xaml;
using openhabUWP.ViewModels;
using Prism.Windows.Mvvm;

namespace openhabUWP.Views
{
    public sealed partial class SettingsPage : SessionStateAwarePage, INotifyPropertyChanged
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsPage"/> class.
        /// </summary>
        public SettingsPage()
        {
            this.InitializeComponent();
            DataContextChanged += MainPage_DataContextChanged;
        }

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets the concrete data context.
        /// </summary>
        /// <value>
        /// The concrete data context.
        /// </value>
        public SettingsPageViewModel ConcreteDataContext
        {
            get
            {
                return DataContext as SettingsPageViewModel;
            }
        }

        /// <summary>
        /// Mains the page_ data context changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="DataContextChangedEventArgs"/> instance containing the event data.</param>
        private void MainPage_DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            var propertyChanged = PropertyChanged;
            if (propertyChanged != null)
            {
                propertyChanged(this, new PropertyChangedEventArgs(nameof(ConcreteDataContext)));
            }
        }
    }
}
