using EpiSandbox.Data.PageSearch;
using EpiSandbox.Models.Pages;
using EPiServer.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpiSandbox.Data
{
    public interface ISampleGetter
    {
        [Obsolete]
        string GetSample(PageData page, string query);
        string GetSample(Result page, Query query);
    }
}
