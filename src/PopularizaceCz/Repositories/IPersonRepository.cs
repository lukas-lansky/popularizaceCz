using PopularizaceCz.Database;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PopularizaceCz.Repositories
{
    public interface IPersonRepository
    {
        Task<PersonDbModel> GetById(int id);

        Task<IEnumerable<PersonDbModel>> GetPersonsWithMostTalks(int take = 10);
    }
}