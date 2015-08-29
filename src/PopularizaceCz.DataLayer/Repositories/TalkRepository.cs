using System.Linq;
using System.Collections.Generic;
using System.Data;
using Dapper;
using PopularizaceCz.Helpers;
using System.Threading.Tasks;
using PopularizaceCz.DataLayer.Models;
using PopularizaceCz.DataLayer.Entities;
using System;

namespace PopularizaceCz.DataLayer.Repositories
{
    public sealed class TalkRepository : ITalkRepository
    {
        private IDbConnection _db;

        public TalkRepository(IDbConnection db)
        {
            this._db = db;
        }

        public class TalkMissingException : AppException { }

        public async Task<TalkDbModel> GetById(int id)
        {
            var talk = (await this._db.QueryAsync<TalkDbEntity>(@"SELECT * FROM [Talk] t WHERE t.[Id] = @TalkId", new { TalkId = id })).SingleOrDefault();
            
            if (talk == null)
            {
                throw new TalkMissingException();
            }

            var venue = (await this._db.QueryAsync<VenueDbEntity>(@"SELECT * FROM [Venue] v WHERE v.[Id] = (SELECT [VenueId] FROM [Talk] WHERE [Id] = @TalkId)", new { TalkId = id })).Single();

            var speakers = await this._db.QueryAsync<PersonDbEntity>(@"
                SELECT p.*
                FROM [Person] p
                INNER JOIN [TalkSpeaker] ts ON ts.[PersonId] = p.[Id]
                WHERE ts.[TalkId] = @TalkId", new { TalkId = id });

            return new TalkDbModel(talk, venue, speakers);
        }

        public async Task<IEnumerable<TalkDbEntity>> GetUpcomingTalks(int take = 10)
        {
            return await this._db.QueryAsync<TalkDbEntity>(@"
				SELECT TOP {0} *
				FROM [Talk]
				WHERE [Start] > GETDATE()
				ORDER BY [Start] ASC".FormatWith(take));
        }

        public async Task Update(TalkDbModel model)
        {
            await this._db.ExecuteAsync(
                "UPDATE [Talk] SET [Name]=@Name, [Start]=@Start WHERE [Id]=@TalkId",
                new {
                    Name = model.Name,
                    Start = model.Start,
                    TalkId = model.Id });

            await this._db.ExecuteAsync("DELETE FROM [TalkSpeaker] WHERE [TalkId]=@TalkId", new { TalkId = model.Id });

            foreach (var speaker in model.Speakers)
            {
                await this._db.ExecuteAsync(
                    "INSERT INTO [TalkSpeaker] ([TalkId], [PersonId]) VALUES (@TalkId, @PersonId)",
                    new { TalkId = model.Id, PersonId = speaker.Id });
            }
        }
    }
}