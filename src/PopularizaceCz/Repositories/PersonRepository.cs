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
    public sealed class PersonRepository : IPersonRepository
    {
        private IDbConnection _db;

        public PersonRepository(IDbConnection db)
        {
            this._db = db;
        }

        public async Task<PersonDbModel> GetById(int id)
        {
            var person = (await this._db.QueryAsync<PersonDbEntity>(
                @"SELECT * FROM [Person] WHERE [Id] = @PersonId",
                new { PersonId = id })).Single();

            var talks = await this._db.QueryAsync<TalkDbEntity>(
                @"SELECT t.* FROM [Talk] t INNER JOIN [TalkSpeaker] ts ON ts.[TalkId] = t.[Id] WHERE ts.[PersonId] = @PersonId",
                new { PersonId = id });

            return new PersonDbModel(person, talks, new List<OrganizationDbEntity>());
        }

        public async Task<IEnumerable<PersonDbModel>> GetPersonsWithMostTalks(int take = 10)
        {
            var persons = await this._db.QueryAsync<PersonDbEntity>(@"
                SELECT TOP {0} p.[Id], p.[Name], COUNT(t.[Id]) FROM [Talk] t
                INNER JOIN [TalkSpeaker] ts ON t.[Id] = ts.[TalkId]
                INNER JOIN [Person] p ON ts.[PersonId] = p.[Id]
                GROUP BY p.[Id], p.[Name]
                ORDER BY COUNT(t.[Id]) DESC".FormatWith(take));

            return await this.CreateDbModel(persons);
        }

        private async Task<IEnumerable<PersonDbModel>> CreateDbModel(IEnumerable<PersonDbEntity> pes)
        {
            var personIds = pes.Select(pe => pe.Id);

            var allTalks = await this._db.QueryAsync(string.Format(@"
SELECT t.*, ts.[PersonId]
FROM [Talk] t
INNER JOIN [TalkSpeaker] ts ON t.[Id] = ts.[TalkId]
WHERE ts.[PersonId] IN ({0})", personIds.JoinToString()));
            var groupedTalks = allTalks
                .GroupBy(r => (int)r.PersonId)
                .ToDictionary(g => g.Key, g => g.Select(d => (TalkDbEntity)d.MapTo<dynamic, TalkDbEntity>()));

            return pes.Select(pe => new PersonDbModel(pe, groupedTalks[pe.Id], new List<OrganizationDbEntity>()));
        }
    }
}