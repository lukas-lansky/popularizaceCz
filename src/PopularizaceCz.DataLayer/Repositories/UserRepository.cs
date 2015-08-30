using System.Data;
using PopularizaceCz.DataLayer.Entities;
using System.Threading.Tasks;

namespace PopularizaceCz.DataLayer.Repositories
{
    public sealed class UserRepository : IUserRepository
    {
        private IDbConnection _db;

        public UserRepository(IDbConnection db)
        {
            this._db = db;
        }

        public async Task<UserDbEntity> GetCurrentUser()
        {
            return new UserDbEntity {
                Name = "Lukáš Lánský",
                IsAdmin = true
            };
        }
    }
}