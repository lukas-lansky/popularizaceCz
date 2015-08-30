using PopularizaceCz.DataLayer.Entities;
using PopularizaceCz.DataLayer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PopularizaceCz.DataLayer.Repositories
{
    public interface IOrganizationRepository
    {
        Task<OrganizationDbModel> GetById(int id);

        Task<IDictionary<OrganizationDbEntity, int>> GetOrganizationsWithMostTalks(int take = 10);

        Task<IEnumerable<OrganizationDbEntity>> GetAllOrganizations();
    }
}