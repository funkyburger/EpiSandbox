using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpiSandbox.Data.PageSearch
{
    public interface IPageSearcher
    {
        IEnumerable<Result> SearchPages(Query query, int pagingNumber, int pagingSize);
    }
}
