using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EpiSandbox.Data.PageSearch
{
    public class PageSearcher : IPageSearcher
    {
        private readonly IInternalPageSearcher _internalPageSearcher;
        private readonly IQueryParser _queryParser;

        public PageSearcher(IInternalPageSearcher internalPageSearcher, IQueryParser queryParser)
        {
            _internalPageSearcher = internalPageSearcher;
            _queryParser = queryParser;
        }

        public IEnumerable<Result> SearchPages(Query query, int pagingNumber, int pagingSize)
        {
            return _internalPageSearcher.SearchPages(_queryParser.Deparse(query), pagingNumber, pagingSize)
                .Select(r => Convert(r));
        }

        private Result Convert(IContentPage contentPage)
        {
            var convertedContent = new List<string>();
            foreach(var content in contentPage.Content)
            {
                convertedContent.AddRange(ParseContent(content));
            }

            return new Result() {
                Content = convertedContent
            };
        }

        private IEnumerable<string> ParseContent(string content)
        {
            HtmlDocument resultat = new HtmlDocument();
            resultat.LoadHtml(content);

            foreach (var paragraph in resultat.DocumentNode.Descendants("p").Select(p => p.InnerText))
            {
                yield return paragraph;
            }
        }
    }
}