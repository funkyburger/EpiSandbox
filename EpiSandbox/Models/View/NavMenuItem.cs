using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EpiSandbox.Models.View
{
    public class NavMenuItem
    {
        public string Label { get; set; }

        public string Link { get; set; }

        public bool Active { get; set; }
    }
}