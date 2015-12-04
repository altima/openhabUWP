using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Microsoft.Practices.Unity;
using openhabUWP.Events;
using Prism.Events;

namespace openhabUWP.UI.Widgets
{
    public interface IWidget
    {
        void RegisterForUpdate();
        void WidgetUpdateReceived(Interfaces.Widgets.IWidget widget);
    }
}