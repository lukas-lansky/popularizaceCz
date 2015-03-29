using Dapper;
using PopularizaceCz.Import.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WikiDataNet;

namespace PopularizaceCz.Import.WikiData
{
    public class WikiDataImporter : IImport
    {
        public async Task<string> Import()
        {
            var personNames = await this.GetNames();

            foreach (var name in personNames)
            {
                var searchResults = new WikiDataSearch { Limit = 2, Query = name }.Run();

                if (searchResults.Count != 1) continue;

                var searchResult = searchResults.First();
            }

            return "";
        }

        private async Task<IEnumerable<string>> GetNames()
        {
            IEnumerable<string> personNames = new List<string>();

            using (var dbc = new SqlConnection("Server=.;Database=PopularizaceCz;Trusted_Connection=True;MultipleActiveResultSets=true"))
            {
                personNames = (await dbc.QueryAsync("SELECT p.[Name] FROM [Person] p")).Select(r => (string)r.Name);
            }

            return personNames;
        }
    }
}
