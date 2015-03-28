using PopularizaceCz.Database;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PopularizaceCz.Repositories
{
    public interface IPersonRepository
    {
        Task<IEnumerable<PersonDbModel>> GetPersonsWithMostTalks(int take = 10);
    }
}