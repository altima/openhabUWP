using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using openhabUWP.Interfaces.Common;
using openhabUWP.Interfaces.Widgets;

namespace openhabUWP.Models
{
    public class Page : IPage, ILinkedPage
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Icon { get; set; }
        public bool Leaf { get; set; }
        public string Link { get; set; }
        public IParentItem Parent { get; set; }
        public IWidget[] Widgets { get; set; }
    }
}
