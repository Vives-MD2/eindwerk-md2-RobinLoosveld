using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using Thunderstruck.DAL.DBs;
using Thunderstruck.DOMAIN.Contracts;
using Thunderstruck.DOMAIN.Models;

namespace Thunderstruck.BLL.Managers
{
    public class UserAchievementManager:IUserAchievement
    {
        private readonly UserAchievementDb _db = new UserAchievementDb();
        public async Task<UserAchievement> GetByIdAsync(int userId, int achievementId)
        {
            return await _db.GetByIdAsync(userId, achievementId);
        }
        public async Task<UserAchievement> GetByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<UserAchievement>> GetAsync(int skip, int take)
        {
            return await _db.GetAsync(skip, take);
        }

        public async Task<UserAchievement> CreateAsync(UserAchievement entity)
        {
            return await _db.CreateAsync(entity);
        }

        public async Task<UserAchievement> UpdateAsync(UserAchievement entity)
        {
            return await _db.UpdateAsync(entity);
        }

        public async Task<UserAchievement> DeleteAsync(UserAchievement entity)
        {
            return await _db.DeleteAsync(entity);
        }


    }
}