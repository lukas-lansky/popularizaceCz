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

            var organizers = await this._db.QueryAsync<OrganizationDbEntity>(@"
                SELECT [o].*
                FROM [Organization] [o]
                INNER JOIN [TalkOrganizer] [to] ON [to].[OrganizationId] = [o].[Id]
                WHERE [to].[TalkId] = @TalkId", new { TalkId = id });

            var categories = await this._db.QueryAsync<CategoryDbEntity>(@"
                SELECT c.*
                FROM [Category] c
                INNER JOIN [TalkCategory] tc ON tc.[CategoryId] = c.[Id]
                WHERE tc.[TalkId] = @TalkId", new { TalkId = id });

            var recordings = await this._db.QueryAsync<TalkRecordingDbEntity>(@"
                SELECT r.*
                FROM [TalkRecording] r
                WHERE r.[TalkId]=@TalkId", new { TalkId = id });

            return new TalkDbModel(talk, venue, speakers, organizers, categories, recordings);
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

            // speakers

            await this._db.ExecuteAsync("DELETE FROM [TalkSpeaker] WHERE [TalkId]=@TalkId", new { TalkId = model.Id });

            foreach (var speaker in model.Speakers)
            {
                await this._db.ExecuteAsync(
                    "INSERT INTO [TalkSpeaker] ([TalkId], [PersonId]) VALUES (@TalkId, @PersonId)",
                    new { TalkId = model.Id, PersonId = speaker.Id });
            }

            // organizers

            await this._db.ExecuteAsync("DELETE FROM [TalkOrganizers] WHERE [TalkId]=@TalkId", new { TalkId = model.Id });

            foreach (var organizer in model.Organizers)
            {
                await this._db.ExecuteAsync(
                    "INSERT INTO [TalkOrganizers] ([TalkId], [OrganizationId]) VALUES (@TalkId, @OrganizationId)",
                    new { TalkId = model.Id, OrganizationId = organizer.Id });
            }

            // recordings

            await this._db.ExecuteAsync("DELETE FROM [TalkRecording] WHERE [TalkId]=@TalkId", new { TalkId = model.Id });

            foreach (var recording in model.Recordings.Where(r => !string.IsNullOrEmpty(r.Url) || !string.IsNullOrEmpty(r.YouTubeVideoId)))
            {
                await this._db.ExecuteAsync(
                    "INSERT INTO [TalkRecording] ([TalkId], [Url], [YouTubeVideoId]) VALUES (@TalkId, @Url, @YouTubeVideoId)",
                    new { TalkId = model.Id, Url = recording.Url, YouTubeVideoId = recording.YouTubeVideoId });
            }
        }
    }
}