using PopularizaceCz.Database;
using PopularizaceCz.Database.Infrastructure;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Data;
using Dapper;
using PopularizaceCz.Helpers;

namespace PopularizaceCz.Repositories
{
    public sealed class TalkRepository
    {
        private IDbConnection _db;

        public TalkRepository(IDbConnection db)
        {
            this._db = db;
        }

        public IEnumerable<TalkDbEntity> GetUpcomingTalks(int take = 10)
        {
            return this._db.Query<TalkDbEntity>("SELECT TOP {0} * FROM [Talk] ORDER BY [Start] DESC".FormatWith(take));
        }
    }
}