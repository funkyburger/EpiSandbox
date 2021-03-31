using EpiSandbox.Data.PageSearch;
using EpiSandbox.Models.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpiSandbox.Data
{
    public interface ISampleGetter
    {
        string GetSample(Result page, Query query);
    }
}
