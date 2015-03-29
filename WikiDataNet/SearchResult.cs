using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WikiDataNet
{
    public class SearchResult
    {
        public string Id { get; private set; }

        public string Url { get; private set; }

        public string Label { get; private set; }

        public string Description { get; private set; }
    }
}
