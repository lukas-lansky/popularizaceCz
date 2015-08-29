using PopularizaceCz.DataLayer.Entities;
using PopularizaceCz.DataLayer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PopularizaceCz.DataLayer.Repositories
{
    public interface IOrganizationRepository
    {
        Task<IEnumerable<OrganizationDbEntity>> GetAllOrganizations();
    }
}