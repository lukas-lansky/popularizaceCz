using PopularizaceCz.DataLayer.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PopularizaceCz.DataLayer.Repositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<CategoryDbEntity>> GetAllCategories();
    }
}