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

        public async Task<TalkDbModel> GetById(int id)
        {
            var talk = (await this._db.QueryAsync<TalkDbEntity>(@"SELECT * FROM [Talk] t WHERE t.[Id] = @TalkId", new { TalkId = id })).Single();

            var venue = (await this._db.QueryAsync<VenueDbEntity>(@"SELECT * FROM [Venue] v WHERE v.[Id] = (SELECT [VenueId] FROM [Talk] WHERE [Id] = @TalkId)", new { TalkId = id })).Single();

            var speakers = await this._db.QueryAsync<PersonDbEntity>(@"
                SELECT *
                FROM [Person] p
                INNER JOIN [TalkSpeaker] ts ON ts.[PersonId] = p.[Id]
                WHERE ts.[TalkId] = @TalkId", new { TalkId = id });

            return new TalkDbModel(talk, venue, speakers);
        }

        public async Task<IEnumerable<TalkDbEntity>> GetUpcomingTalks(int take = 10)
        {
            return await this._db.QueryAsync<TalkDbEntity>("SELECT TOP {0} * FROM [Talk] WHERE [Start] > GETDATE() ORDER BY [Start] ASC".FormatWith(take));
        }
    }
}