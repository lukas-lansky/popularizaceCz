using PopularizaceCz.Database;
using PopularizaceCz.Database.Infrastructure;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Data;
using Dapper;
using PopularizaceCz.Helpers;
using System.Threading.Tasks;

namespace PopularizaceCz.Repositories
{
    public sealed class TalkRepository : ITalkRepository
    {
        private IDbConnection _db;

        public TalkRepository(IDbConnection db)
        {
            this._db = db;
        }

        public async Task<IEnumerable<TalkDbEntity>> GetUpcomingTalks(int take = 10)
        {
            return await this._db.QueryAsync<TalkDbEntity>("SELECT TOP {0} * FROM [Talk] WHERE [Start] > GETDATE() ORDER BY [Start] ASC".FormatWith(take));
        }
    }
}