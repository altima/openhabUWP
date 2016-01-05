using openhabUWP.Interfaces.Widgets;
using openhabUWP.Models;

namespace openhabUWP.Interfaces.Common
{
    public interface IPage
    {
        string Id { get; set; }
        bool Leaf { get; set; }
        string Link { get; set; }
        string Title { get; set; }
        string Icon { get; set; }

        Page Parent { get; set; }
        IWidget[] Widgets { get; set; }
    }
}
