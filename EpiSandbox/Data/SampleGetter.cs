using EpiSandbox.Extensions;
using EpiSandbox.Models.Pages;
using EPiServer.Core;
using HtmlAgilityPack;
using System.Linq;

namespace EpiSandbox.Data
{
    public class SampleGetter : ISampleGetter
    {
        public string GetSample(PageData page, string query)
        {
            var standardContentPage = page as StandardContentPage;
            if(standardContentPage != null)
            {
                return GetSample(standardContentPage, query);
            }

            return "<i>Could not retrieve sample.</i>";
        }

        public string GetSample(StandardContentPage page, string query)
        {
            return ExtractRelevantText(page, query).HtmlBoldAllOrSomeWords(query);
        }

        private string ExtractRelevantText(StandardContentPage page, string query)
        {
            HtmlDocument resultat = new HtmlDocument();
            resultat.LoadHtml(page.MainBody.ToHtmlString());

            foreach(var paragraph in resultat.DocumentNode.Descendants("p").Select(p => p.InnerText))
            {
                if (paragraph.ContainsAllOrSomeWords(query))
                {
                    return paragraph.CapLength(512);
                }
            }

            return resultat.DocumentNode.InnerText.CapLength(512);
        }
    }
}