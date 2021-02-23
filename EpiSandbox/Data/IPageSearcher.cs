using EpiSandbox.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpiSandbox.Data
{
    public interface IPageSearcher
    {
        IEnumerable<SearchHit> SearchPages(string query, int pagingNumber, int pagingSize);
    }
}
