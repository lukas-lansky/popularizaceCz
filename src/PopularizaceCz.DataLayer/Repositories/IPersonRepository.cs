using PopularizaceCz.DataLayer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PopularizaceCz.DataLayer.Repositories
{
    public interface IPersonRepository
    {
        Task<PersonDbModel> GetById(int id);

        Task<IEnumerable<PersonDbModel>> GetPersonsWithMostTalks(int take = 10);
    }
}