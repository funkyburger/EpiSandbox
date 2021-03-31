using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EpiSandbox.Data.PageSearch
{
    public class Result
    {
        public string Headline { get; set; }
        public string Link { get; set; }
        public IEnumerable<string> Content { get; set; }
    }
}