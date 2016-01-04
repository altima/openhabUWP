using openhabUWP.Models;

namespace openhabUWP.Interfaces.Widgets
{
    public interface IContainerWidget : IWidget
    {
        IWidget[] Widgets { get; set; }
    }

    public interface ILinkedPageWidget : IWidget
    {
        Page LinkedPage { get; set; }
    }

    public interface IFrameWidget : ILinkedPageWidget, IContainerWidget { }

    public interface ITextWidget : ILinkedPageWidget { }

    public interface ISwitchWidget : IWidget { }

    public interface IGroupWidget : ILinkedPageWidget, IContainerWidget { }
}