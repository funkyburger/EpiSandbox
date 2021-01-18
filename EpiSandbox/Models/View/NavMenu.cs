using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EpiSandbox.Models.View
{
    [Obsolete]
    public class NavMenu
    {
        public IEnumerable<NavMenuItem> HeadItems { get; set; }

        public IEnumerable<NavMenuItem> SubItems { get; set; }
    }
}