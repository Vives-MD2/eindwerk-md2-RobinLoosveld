using System.Collections.Generic;
using System.Threading.Tasks;
using Thunderstruck.DOMAIN.Contracts;
using Thunderstruck.DOMAIN.Models;

namespace Thunderstruck.DAL.DBs
{
    public class UserAchievementDb:IUserAchievement
    {
        private readonly ThunderstruckContext _context = new ThunderstruckContext();
        public Task<UserAchievement> GetByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<UserAchievement>> GetAsync(int skip, int take)
        {
            throw new System.NotImplementedException();
        }

        public Task<UserAchievement> CreateAsync(UserAchievement entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<UserAchievement> UpdateAsync(UserAchievement entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<UserAchievement> DeleteAsync(UserAchievement entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<UserAchievement> GetByIdAsync(int userId, int achievementId)
        {
            throw new System.NotImplementedException();
        }
    }
}