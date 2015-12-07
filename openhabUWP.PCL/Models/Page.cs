using openhabUWP.Interfaces.Common;
using openhabUWP.Interfaces.Widgets;

namespace openhabUWP.Models
{
    public class Page : IPage, ILinkedPage
    {
        public Page()
        {

        }

        public Page(string link, bool leaf)
            : this()
        {
            this.Link = link;
            this.Leaf = leaf;
        }

        public Page(string id, string title, string link, bool leaf, string icon)
            : this(link, leaf)
        {
            this.Id = id;
            this.Title = title;
            this.Icon = icon;
        }

        public string Id { get; set; }
        public string Title { get; set; }
        public string Icon { get; set; }
        public bool Leaf { get; set; }
        public string Link { get; set; }

        public IParentItem Parent { get; set; }
        public IWidget[] Widgets { get; set; }
    }
}
