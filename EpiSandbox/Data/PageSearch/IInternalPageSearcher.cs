using EpiSandbox.Data.PageSearch;
using EpiSandbox.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpiSandbox.Data.PageSearch
{
    public interface IInternalPageSearcher
    {
        IEnumerable<IContentPage> SearchPages(string query, int pagingNumber, int pagingSize);
    }
}
