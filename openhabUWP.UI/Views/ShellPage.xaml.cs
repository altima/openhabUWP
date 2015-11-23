using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using openhabUWP.ViewModels;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace openhabUWP.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ShellPage : Page
    {
        public ShellPage(Frame rootFrame, ShellPageViewModel viewModel)
        {
            Debug.WriteLine("ShellPage()");
            this.InitializeComponent();
            this.DataContext = viewModel;
            this.RootSplitView.Content = rootFrame;

            this.ScreenSize.Text = string.Format("{0},{1}",
                Window.Current.Bounds.Width.ToString("0.00"),
                Window.Current.Bounds.Height.ToString("0.00"));

            Window.Current.SizeChanged += Current_SizeChanged;
        }

        private void Current_SizeChanged(object sender, global::Windows.UI.Core.WindowSizeChangedEventArgs e)
        {
            this.ScreenSize.Text = string.Format("{0},{1}",
                Window.Current.Bounds.Width.ToString("0.00"),
                Window.Current.Bounds.Height.ToString("0.00"));
        }
    }
}
