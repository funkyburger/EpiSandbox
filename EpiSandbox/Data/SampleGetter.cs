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
            var contentPage = page as IContentPage;
            if(contentPage != null)
            {
                return GetSample(contentPage, query);
            }

            return "<i>Could not retrieve sample.</i>";
        }

        private string GetSample(IContentPage page, string query)
        {
            return ExtractRelevantText(page.Content.First(), query).HtmlBoldAllOrSomeWords(query);
        }

        private string ExtractRelevantText(XhtmlString xhtml, string query)
        {
            HtmlDocument resultat = new HtmlDocument();
            resultat.LoadHtml(xhtml.ToHtmlString());

            foreach (var paragraph in resultat.DocumentNode.Descendants("p").Select(p => p.InnerText))
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