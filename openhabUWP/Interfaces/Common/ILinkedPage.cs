using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using openhabUWP.Interfaces.Widgets;

namespace openhabUWP.Interfaces.Common
{
    public interface ILinkedPage : IIdItem, ITitleItem, IIconItem, ILinkItem, ILeafItem
    {
    }
}
