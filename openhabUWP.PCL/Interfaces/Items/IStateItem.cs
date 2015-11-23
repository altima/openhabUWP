using openhabUWP.Interfaces.Common;

namespace openhabUWP.Interfaces.Items
{
    public interface IStateItem : INameItem, ILinkItem, IItem
    {

    }

    public interface IStateItem<T> : IStateItem
    {
        T State { get; set; }
    }
}
