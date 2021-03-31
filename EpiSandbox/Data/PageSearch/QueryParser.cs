using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace EpiSandbox.Data.PageSearch
{
    public class QueryParser : IQueryParser
    {
        public Query Parse(string query)
        {
            return new Query() 
            { 
                Fragments = Extract(query)
            };
        }

        public string Deparse(Query query)
        {
            var builder = new StringBuilder();

            foreach (var fragment in query.Fragments)
            {
                if(builder.Length < 1)
                {
                    builder.Append($"\"{fragment}\"");
                }
                else
                {
                    builder.Append($" \"{fragment}\"");
                }
            }

            return builder.ToString();
        }

        private IList<string> Extract(string query)
        {
            var result = new List<string>();
            var inQuote = false;
            var temp = new StringBuilder();

            foreach (char c in query)
            {
                if(c == '\"')
                {
                    if (inQuote)
                    {
                        if (!string.IsNullOrWhiteSpace(temp.ToString()))
                        {
                            result.Add(temp.ToString());
                        }
                        temp.Clear();
                    }

                    inQuote = !inQuote;
                }
                else if (c == ' ' && !inQuote)
                {
                    if(temp.Length > 0)
                    {
                        result.Add(temp.ToString());
                        temp.Clear();
                    }
                }
                else
                {
                    temp.Append(c);
                }
            }

            if(temp.Length > 0)
            {
                if (inQuote)
                {
                    result.AddRange(Extract(temp.ToString()));
                }
                else
                {
                    result.Add(temp.ToString());
                }
            }

            return result;
        }
    }
}