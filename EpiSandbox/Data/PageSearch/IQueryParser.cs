using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpiSandbox.Data.PageSearch
{
    public interface IQueryParser
    {
        Query Parse(string query);
    }
}
