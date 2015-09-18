using System.Collections.Generic;
using System.Data;
using Dapper;
using System.Threading.Tasks;
using PopularizaceCz.DataLayer.Entities;
using System.Linq;
using PopularizaceCz.Helpers;
using PopularizaceCz.DataLayer.Models;

namespace PopularizaceCz.DataLayer.Repositories
{
    public sealed class CategoryRepository : ICategoryRepository
    {
        private IDbConnection _db;

        public CategoryRepository(IDbConnection db)
        {
            this._db = db;
        }

        public class CategoryMissingException : AppException { }

        public async Task<CategoryDbModel> GetById(int id)
        {
            var categoryEntity = (await this._db
                .QueryAsync<CategoryDbEntity>(
                    "SELECT [c].* FROM [Category] [c] WHERE [c].[Id]=@CategoryId",
                    new { CategoryId = id }))
                .SingleOrDefault();

            if (categoryEntity == null)
            {
                throw new CategoryMissingException();
            }

            var talks = await this._db.QueryAsync<TalkDbEntity>(
                @"SELECT [t].*
                FROM [Talk] [t]
                INNER JOIN [TalkCategory] [tc] ON [tc].[TalkId]=[t].[Id]
                WHERE [tc].[CategoryId]=@CategoryId",
                new { CategoryId = id });

            return new CategoryDbModel(categoryEntity, null, null, talks.ToList());
        }

        public async Task<IEnumerable<CategoryDbEntity>> GetAllCategories()
        {
            return await this._db.QueryAsync<CategoryDbEntity>(@"SELECT c.* FROM [Category] c ORDER BY c.[Name]");
        }
        
        public async Task<IDictionary<CategoryDbEntity, int>> GetCategoriesWithMostTalks(int take = 10)
        {
            var cats = await this._db.QueryAsync(@"
                SELECT TOP {0} c.*, COUNT(t.[Id]) AS TalkCount FROM [Talk] t
                INNER JOIN [TalkCategory] tc ON t.[Id] = tc.[TalkId]
                INNER JOIN [Category] c ON tc.[CategoryId] = c.[Id]
                GROUP BY c.[Id], c.[Name]
                ORDER BY COUNT(c.[Id]) DESC".FormatWith(take));

            return cats.ToDictionary(
                p => new CategoryDbEntity { Id = p.Id, Name = p.Name }, // TODO.
                p => (int)p.TalkCount);
        }
    }
}