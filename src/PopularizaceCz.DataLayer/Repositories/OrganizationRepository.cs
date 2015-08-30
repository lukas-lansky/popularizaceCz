using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using System.Threading.Tasks;
using PopularizaceCz.DataLayer.Entities;
using PopularizaceCz.DataLayer.Models;

namespace PopularizaceCz.DataLayer.Repositories
{
    public sealed class OrganizationRepository : IOrganizationRepository
    {
        private IDbConnection _db;

        public OrganizationRepository(IDbConnection db)
        {
            this._db = db;
        }

        public async Task<OrganizationDbModel> GetById(int id)
        {
            var org = (await this._db.QueryAsync<OrganizationDbEntity>(
                @"SELECT * FROM [Organization] WHERE [Id] = @OrganizationId",
                new { OrganizationId = id })).Single();

            var talks = await this._db.QueryAsync<TalkDbEntity>(
                @"SELECT t.* FROM [Talk] t INNER JOIN [TalkOrganizer] [to] ON [to].[TalkId] = t.[Id] WHERE [to].[OrganizationId] = @OrganizationId",
                new { OrganizationId = id });

            return new OrganizationDbModel(org, talks);
        }

        public async Task<IEnumerable<OrganizationDbEntity>> GetAllOrganizations()
        {
            return await this._db.QueryAsync<OrganizationDbEntity>(@"SELECT p.* FROM [Organization] p ORDER BY p.[Name]");
        }
    }
}