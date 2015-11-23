using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using openhabUWP.Interfaces;

namespace openhabUWP.Models
{
    public class Openhab : IOpenhab
    {
        public Openhab()
        {
            Links = new Link[0];
        }

        public Link[] Links { get; set; }
    }
}
