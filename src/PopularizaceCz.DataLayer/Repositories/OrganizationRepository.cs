using System.Collections.Generic;
using System.Data;
using Dapper;
using System.Threading.Tasks;
using PopularizaceCz.DataLayer.Entities;

namespace PopularizaceCz.DataLayer.Repositories
{
    public sealed class OrganizationRepository : IOrganizationRepository
    {
        private IDbConnection _db;

        public OrganizationRepository(IDbConnection db)
        {
            this._db = db;
        }

        public async Task<IEnumerable<OrganizationDbEntity>> GetAllOrganizations()
        {
            return await this._db.QueryAsync<OrganizationDbEntity>(@"SELECT p.* FROM [Organization] p ORDER BY p.[Name]");
        }
    }
}