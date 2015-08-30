using PopularizaceCz.DataLayer.Entities;
using PopularizaceCz.DataLayer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PopularizaceCz.DataLayer.Repositories
{
    public interface IPersonRepository
    {
        Task<PersonDbModel> GetById(int id);

        Task<IDictionary<PersonDbEntity, int>> GetPersonsWithMostTalks(int take = 10);

        Task<IEnumerable<PersonDbEntity>> GetAllPersons();
    }
}