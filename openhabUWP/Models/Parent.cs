using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using openhabUWP.Interfaces.Common;

namespace openhabUWP.Models
{
    public class Parent : IParentItem
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public bool Leaf { get; set; }
    }
}
