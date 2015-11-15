using PopularizaceCz.DataLayer.Entities;
using PopularizaceCz.DataLayer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PopularizaceCz.DataLayer.Repositories
{
    public interface ITalkRepository
    {
        Task<TalkDbModel> GetById(int id);

        Task<IEnumerable<TalkDbEntity>> GetUpcomingTalks(int take = 10);

        Task Update(TalkDbModel model);
        
        Task<int> Create(TalkDbModel model);
    }
}