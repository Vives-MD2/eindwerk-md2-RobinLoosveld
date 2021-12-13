using System.Collections.Generic;
using System.Threading.Tasks;
using Thunderstruck.DOMAIN.Contracts;

namespace Thunderstruck.DAL.DBs
{
    public class AchievementDb:IAchievement
    {
        private readonly ThunderstruckContext _context = new ThunderstruckContext();
        public Task<IAchievement> GetByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<IAchievement>> GetAsync(int skip, int take)
        {
            throw new System.NotImplementedException();
        }

        public Task<IAchievement> CreateAsync(IAchievement entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<IAchievement> UpdateAsync(IAchievement entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<IAchievement> DeleteAsync(IAchievement entity)
        {
            throw new System.NotImplementedException();
        }
    }
}