using EpiSandbox.Data.PageSearch;
using EpiSandbox.Models;
using EPiServer.Core;
using EPiServer.Search;
using EPiServer.Search.Queries.Lucene;
using EPiServer.ServiceLocation;
using System.Collections.Generic;
using System.Linq;

namespace EpiSandbox.Data.PageSearch
{
    public class InternalPageSearcher : IInternalPageSearcher
    {
        private readonly IQueryParser _queryParser;

        public InternalPageSearcher(IQueryParser queryParser)
        {
            _queryParser = queryParser;
        }

        public IEnumerable<Result> SearchPages(Query query, int pagingNumber, int pagingSize)
        {
            foreach(var pageData in FindPages(_queryParser.Deparse(query), pagingNumber, pagingSize))
            {
                var hasContent = pageData as IHasContent;
                if(hasContent != null)
                {
                    yield return new Result() {
                        Headline = pageData.Name,
                        Link = pageData.LinkURL,
                        Content = hasContent.Content.Select(c => c.ToString())
                    };
                }
            }
        }

        private IEnumerable<PageData> FindPages(string searchQuery, int pagingNumber, int pagingSize)
        {
            GroupQuery groupQuery = new GroupQuery(LuceneOperator.AND);
            groupQuery.QueryExpressions.Add(new ContentQuery<PageData>());

            groupQuery.QueryExpressions.Add(new FieldQuery(searchQuery));

            VirtualPathQuery pathQuery = new VirtualPathQuery();
            pathQuery.AddContentNodes(ContentReference.StartPage);
            groupQuery.QueryExpressions.Add(pathQuery);

            // The access control list query will remove any pages the user doesn't have read access to
            //AccessControlListQuery aclQuery = new AccessControlListQuery();
            //aclQuery.AddAclForUser(PrincipalInfo.Current, HttpContext.Current);
            //groupQuery.QueryExpressions.Add(aclQuery);

            var searchHandler = ServiceLocator.Current.GetInstance<SearchHandler>();
            SearchResults results = searchHandler.GetSearchResults(groupQuery, pagingNumber, pagingSize);

            var contentSearchHandler = ServiceLocator.Current.GetInstance<ContentSearchHandler>();
            foreach (var hit in results.IndexResponseItems)
            {
                yield return contentSearchHandler.GetContent<PageData>(hit);
            }
        }
    }
}