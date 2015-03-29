using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WikiDataNet
{
    public class SearchResults : IEnumerable<SearchResult>
    {
        public int Count
        {
            get
            {
                return this._results.Count;
            }
        }

        public string Query { get; private set; }

        private List<SearchResult> _results;
        
        public SearchResults(IEnumerable<SearchResult> results, string query)
        {
            this._results = results.ToList();
            this.Query = query;
        }

        public IEnumerator<SearchResult> GetEnumerator()
        {
            foreach (var sr in this._results)
            {
                yield return sr;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
