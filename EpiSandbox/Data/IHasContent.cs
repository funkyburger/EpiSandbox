using EPiServer.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpiSandbox.Data
{
    public interface IHasContent
    {
        IEnumerable<XhtmlString> Content { get; }
    }
}
