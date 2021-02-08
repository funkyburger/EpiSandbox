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
        public IEnumerable<SearchHit> Get(string q, int page = 1, int pageSize = 20)
        {
            if(page < 5)
            {
                for (int i = 0; i < pageSize; i++)
                {
                    yield return new SearchHit()
                    {
                        Headline = "Lorem ipsum",
                        Sample = "On sait depuis longtemps que travailler avec du texte lisible et contenant du sens est source de distractions, et empêche de se concentrer sur la mise en page elle-même. L'avantage du Lorem Ipsum sur un texte générique comme 'Du texte. Du texte. Du texte.' est qu'il possède une distribution de lettres plus ou moins normale, et en tout cas comparable avec celle du français standard. De nombreuses suites logicielles de mise en page ou éditeurs de sites Web ont fait du Lorem Ipsum leur faux texte par défaut, et une recherche pour 'Lorem Ipsum' vous conduira vers de nombreux sites qui n'en sont encore qu'à leur phase de construction. Plusieurs versions sont apparues avec le temps, parfois par accident, souvent intentionnellement (histoire d'y rajouter de petits clins d'oeil, voire des phrases embarassantes).",
                        Link = "/Blah/blih/bluh"
                    };
                }
            }
        }
    }
}