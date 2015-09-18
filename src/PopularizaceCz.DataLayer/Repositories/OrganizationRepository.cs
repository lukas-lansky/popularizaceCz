using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using System.Threading.Tasks;
using PopularizaceCz.DataLayer.Entities;
using PopularizaceCz.DataLayer.Models;
using PopularizaceCz.Helpers;
using EmitMapper;
using System;

namespace PopularizaceCz.DataLayer.Repositories
{
    public sealed class OrganizationRepository : IOrganizationRepository
    {
        private IDbConnection _db;

        public OrganizationRepository(IDbConnection db)
        {
            this._db = db;
        }

        public class OrganizationMissingException : AppException { }

        public async Task<OrganizationDbModel> GetById(int id)
        {
            var org = (await this._db.QueryAsync<OrganizationDbEntity>(
                @"SELECT * FROM [Organization] WHERE [Id] = @OrganizationId",
                new { OrganizationId = id })).SingleOrDefault();

            if (org == null)
            {
                throw new OrganizationMissingException();
            }

            var talks = await this._db.QueryAsync<TalkDbEntity>(
                @"SELECT t.* FROM [Talk] t INNER JOIN [TalkOrganizer] [to] ON [to].[TalkId] = t.[Id] WHERE [to].[OrganizationId] = @OrganizationId",
                new { OrganizationId = id });

            return new OrganizationDbModel(org, talks);
        }

        public async Task<IDictionary<OrganizationDbEntity, int>> GetOrganizationsWithMostTalks(int take = 10)
        {
            var orgs = await this._db.QueryAsync(@"
                SELECT TOP {0} o.*, COUNT(t.[Id]) AS TalkCount FROM [Talk] t
                INNER JOIN [TalkOrganizer] [to] ON t.[Id] = [to].[TalkId]
                INNER JOIN [Organization] o ON [to].[OrganizationId] = o.[Id]
                GROUP BY o.[Id], o.[Name], o.[Url]
                ORDER BY COUNT(t.[Id]) DESC".FormatWith(take));
            
            return orgs.ToDictionary(
                p => new OrganizationDbEntity { Id = p.Id, Name = p.Name }, // TODO.
                p => (int)p.TalkCount);
        }

        public async Task<IEnumerable<OrganizationDbEntity>> GetAllOrganizations()
        {
            return await this._db.QueryAsync<OrganizationDbEntity>(@"SELECT p.* FROM [Organization] p ORDER BY p.[Name]");
        }
    }
}