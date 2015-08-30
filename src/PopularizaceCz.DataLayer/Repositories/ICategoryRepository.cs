using PopularizaceCz.DataLayer.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PopularizaceCz.DataLayer.Repositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<CategoryDbEntity>> GetAllCategories();

        Task<IDictionary<CategoryDbEntity, int>> GetCategoriesWithMostTalks(int take = 10);
    }
}