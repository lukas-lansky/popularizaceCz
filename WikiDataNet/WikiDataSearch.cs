using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WikiDataNet
{
    public class WikiDataSearch
    {
        public WikiDataSearch()
        {
            this.Limit = 7;
        }

        public string Query { get; set; }

        public int Limit { get; set; }

        public SearchResults Run()
        {
            if (string.IsNullOrWhiteSpace(this.Query))
            {
                throw new Exception("Query should not be empty.");
            }

            return null;
        }

        private async Task<IEnumerable<dynamic>> GetSearchResult(string query, int limit)
        {
            var jsonString = await new WebClient().DownloadStringAsync("");
        }
    }
}
