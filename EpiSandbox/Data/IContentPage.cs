using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpiSandbox.Data
{
    public interface IContentPage
    {
        string Headline { get; }
        string Link { get; }
        IEnumerable<string> Content { get; }
    }
}
