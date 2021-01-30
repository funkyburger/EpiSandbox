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
        [Route("api/search")]
        public IEnumerable<SearchHit> Get(string query, int page = 1, int pageSize = 20)
        {
            // TODO : real search
            return new SearchHit[] { 
                new SearchHit() 
                {
                    Headline = "Toto",
                    Sample = "asdasddsaasd asd sad sad dsa  asd dsa",
                    Link = "/Blah/blih/bluh"
                },
                new SearchHit()
                {
                    Headline = "Titi",
                    Sample = "asdasddsaasd asd sad sad dsa  asd dsa",
                    Link = "/Blah/blih/bluh"
                },
                new SearchHit()
                {
                    Headline = "Tata",
                    Sample = "asdasddsaasd asd sad sad dsa  asd dsa",
                    Link = "/Blah/blih/bluh"
                },
                new SearchHit()
                {
                    Headline = "Page with long headline and stuff",
                    Sample = "asdasddsaasd asd sad sad dsa  asd dsa",
                    Link = "/Blah/blih/bluh"
                }
            };
        }
    }
}