using openhabUWP.Models;
using openhabUWP.Remote.Models;

namespace openhabUWP.UI.Widgets
{
    public interface IWidgetControl
    {
        void RegisterForUpdate();
        void WidgetUpdateReceived(Widget widget);
    }
}