using System.Collections.Generic;
using System.Data;
using Dapper;
using System.Threading.Tasks;
using PopularizaceCz.DataLayer.Entities;

namespace PopularizaceCz.DataLayer.Repositories
{
    public sealed class CategoryRepository : ICategoryRepository
    {
        private IDbConnection _db;

        public CategoryRepository(IDbConnection db)
        {
            this._db = db;
        }
        
        public async Task<IEnumerable<CategoryDbEntity>> GetAllCategories()
        {
            return await this._db.QueryAsync<CategoryDbEntity>(@"SELECT c.* FROM [Category] c ORDER BY c.[Name]");
        }
    }
}