using openhabUWP.Interfaces.Widgets;

namespace openhabUWP.Interfaces.Common
{
    public interface IPage : IIdItem, ITitleItem, IIconItem, ILeafItem, ILinkItem
    {
        IParentItem Parent { get; set; }
        IWidget[] Widgets { get; set; }
    }
}
