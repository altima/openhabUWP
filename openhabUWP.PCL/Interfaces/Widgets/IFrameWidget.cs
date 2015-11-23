using openhabUWP.Interfaces.Common;

namespace openhabUWP.Interfaces.Widgets
{
    public interface IFrameWidget : IWidget
    {
        IWidget[] Widgets { get; set; }
        ILinkedPage LinkedPage { get; set; }
    }

    public interface ITextWidget : IWidget
    {
        ILinkedPage LinkedPage { get; set; }
    }

    public interface ISwitchWidget : IWidget { }
}