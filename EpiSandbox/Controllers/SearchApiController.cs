using EpiSandbox.Data;
using EpiSandbox.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EpiSandbox.Controllers
{
    public class SearchApiController : ApiController
    {
        private readonly IPageSearcher _pageSearcher;

        public SearchApiController(IPageSearcher pageSearcher)
        {
            _pageSearcher = pageSearcher;
        }

        [Route("api/search")]
        public IEnumerable<SearchHit> Get(string q, int page = 1, int pageSize = 20)
        {
            return _pageSearcher.SearchPages(q, page, pageSize);
        }
    }
}