using System.Collections.Generic;
using System.Threading.Tasks;
using Thunderstruck.DAL.DBs;
using Thunderstruck.DOMAIN.Contracts;
using Thunderstruck.DOMAIN.Models;

namespace Thunderstruck.BLL.Managers
{
    public class UserLocationDataManager:IUserLocationData
    {
        private readonly UserLocationDataDb _db = new UserLocationDataDb();

        public async Task<UserLocationData> GetByIdAsync(int userId, int locationDataId)
        {
            throw new System.NotImplementedException();
        }
        public async Task<IEnumerable<UserLocationData>> GetAsync(int skip, int take)
        {
            throw new System.NotImplementedException();
        }

        public async Task<UserLocationData> CreateAsync(UserLocationData entity)
        {
            throw new System.NotImplementedException();
        }

        public async Task<UserLocationData> UpdateAsync(UserLocationData entity)
        {
            throw new System.NotImplementedException();
        }

        public async Task<UserLocationData> DeleteAsync(UserLocationData entity)
        {
            throw new System.NotImplementedException();
        }

        public async Task<UserLocationData> GetByIdAsync(int id)
        {
            return await _db.
        }
    }
}