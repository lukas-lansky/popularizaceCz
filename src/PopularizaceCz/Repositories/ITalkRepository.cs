using PopularizaceCz.Database;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PopularizaceCz.Repositories
{
    public interface ITalkRepository
    {
        Task<TalkDbModel> GetById(int id);

        Task<IEnumerable<TalkDbEntity>> GetUpcomingTalks(int take = 10);
    }
}