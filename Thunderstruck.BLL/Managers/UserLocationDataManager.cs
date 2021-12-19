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
            return await _db.GetByIdAsync(userId, locationDataId);
        }
        public async Task<IEnumerable<UserLocationData>> GetAsync(int skip, int take)
        {
            return await _db.GetAsync(skip, take);
        }

        public async Task<UserLocationData> CreateAsync(UserLocationData entity)
        {
            return await _db.CreateAsync(entity);
        }

        public async Task<UserLocationData> UpdateAsync(UserLocationData entity)
        {
            return await _db.UpdateAsync(entity);
        }

        public async Task<UserLocationData> DeleteAsync(UserLocationData entity)
        {
            return await _db.DeleteAsync(entity);
        }
        //ToDo: remove the unworthy
        public async Task<UserLocationData> GetByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}