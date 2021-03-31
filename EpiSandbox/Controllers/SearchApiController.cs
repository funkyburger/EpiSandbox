using EpiSandbox.Data;
using EpiSandbox.Data.PageSearch;
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
        private readonly ISampleGetter _sampleGetter;
        private readonly IQueryParser _queryParser;
        
        public SearchApiController(IPageSearcher pageSearcher, ISampleGetter sampleGetter, IQueryParser queryParser)
        {
            _pageSearcher = pageSearcher;
            _sampleGetter = sampleGetter;
            _queryParser = queryParser;
        }

        [Route("api/search")]
        public IEnumerable<SearchHit> Get(string q, int page = 1, int pageSize = 20)
        {
            var query = _queryParser.Parse(q);

            var searchResults = _pageSearcher.SearchPages(query, page, pageSize);

            return searchResults.Select(r => new SearchHit() { 
                Headline = r.Headline,
                Link = r.Link,
                Sample = _sampleGetter.GetSample(r, query)
            });
        }
    }
}