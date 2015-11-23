using openhabUWP.Interfaces.Common;

namespace openhabUWP.Interfaces.Widgets
{

    /// <summary>
    /// Id = WidgetId
    /// </summary>
    public interface IWidget : IIdItem, ILabelItem, IIconItem
    {
        IItem Item { get; set; }
    }
}