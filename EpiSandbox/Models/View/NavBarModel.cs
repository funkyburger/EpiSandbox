using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EpiSandbox.Models.View
{
    public class NavBarModel
    {
        public IDictionary<LinkModel, IEnumerable<LinkModel>> Links { get; set; }
    }
}