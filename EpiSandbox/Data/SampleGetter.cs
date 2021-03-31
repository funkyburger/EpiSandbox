using EpiSandbox.Data.PageSearch;
using EpiSandbox.Extensions;
using EpiSandbox.Models.Pages;
using HtmlAgilityPack;
using System.Linq;

namespace EpiSandbox.Data
{
    public class SampleGetter : ISampleGetter
    {
        public string GetSample(Result page, Query query)
        {
            if(page.Content == null)
            {
                // TODO log warning
                return null;
            }
            
            foreach(var content in page.Content)
            {
                if(query.Fragments.Any(f => content.ContainsAllOrSomeWords(f)))
                {
                    return content.CapLength(512).HtmlBoldWords(query.Fragments);
                }
            }

            // TODO test
            return page.Content.FirstOrDefault()?.CapLength(512);
        }
    }
}