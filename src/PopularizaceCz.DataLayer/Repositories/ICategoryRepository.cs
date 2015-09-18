using PopularizaceCz.DataLayer.Entities;
using PopularizaceCz.DataLayer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PopularizaceCz.DataLayer.Repositories
{
    public interface ICategoryRepository
    {
        Task<CategoryDbModel> GetById(int id);

        Task<IEnumerable<CategoryDbEntity>> GetAllCategories();

        Task<IDictionary<CategoryDbEntity, int>> GetCategoriesWithMostTalks(int take = 10);
    }
}