using PopularizaceCz.Database;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PopularizaceCz.Repositories
{
    public interface ITalkRepository
    {
        Task<IEnumerable<TalkDbEntity>> GetUpcomingTalks(int take = 10);
    }
}