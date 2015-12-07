using openhabUWP.Interfaces.Common;

namespace openhabUWP.Interfaces.Widgets
{
    public interface IItemWidget : IWidget
    {
        ILinkedPage LinkedPage { get; set; }
    }
}