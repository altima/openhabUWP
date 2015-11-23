using openhabUWP.Interfaces.Common;

namespace openhabUWP.Interfaces.Widgets
{
    public interface IItemWidget : IWidget
    {
        IItem Item { get; set; }
        ILinkedPage LinkedPage { get; set; }
    }
}