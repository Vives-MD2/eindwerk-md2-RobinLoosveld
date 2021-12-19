using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using Thunderstruck.DOMAIN.Contracts;
using Thunderstruck.DOMAIN.Models;

namespace Thunderstruck.DAL.DBs
{
    public class AchievementRainDb:IAchievementRain
    {
        public Task<Achievement> GetByIdAsync(int id)
        {
            throw new System.NotImplementedException();

        }

        public Task<IEnumerable<Achievement>> GetAsync(int skip, int take)
        {
            throw new System.NotImplementedException();
        }

        public Task<Achievement> CreateAsync(Achievement entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<Achievement> UpdateAsync(Achievement entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<Achievement> DeleteAsync(Achievement entity)
        {
            throw new System.NotImplementedException();
        }
    }
}