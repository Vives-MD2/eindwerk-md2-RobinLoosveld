using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Operation.Valid;
using Thunderstruck.DAL;
using Thunderstruck.DAL.DBs;
using Thunderstruck.DOMAIN.Contracts;
using Thunderstruck.DOMAIN.Helpers;
using Thunderstruck.DOMAIN.Models;

namespace Thunderstruck.BLL.Managers
{
    public class AchievementManager:IAchievement, IAchievementRain,IAchievementHighVoltage,IAchievementListening,IAchievementSpeed
    {
        //ToDo: unable to create a basic achievement object because the class is abstract
        //ToDo: might have to make e manager for each of the derived classes BUT not for the parent class
        private readonly AchievementDb _db = new AchievementDb();
        public async Task<Achievement> GetByIdAsync(int id)
        {
            if (id <= 0)
            {
                //TODO: the type needs to change and cannot be rain all the time
                return new AchievementRain()
                {
                    Tex = new ThunderstruckException("Achievement Id is lower than 1, unusable.", ExceptionTypes.Warning)
                };
            }
            var dbAchievement = await _db.GetByIdAsync(id);
            return dbAchievement;
        }

        public async Task<IEnumerable<Achievement>> GetAsync(int skip, int take)
        {
            return await _db.GetAsync(skip, take);

        }

        public async Task<Achievement> CreateAsync(Achievement entity)
        {
            //TODO: check of id is valid
            return await _db.CreateAsync(entity);
        }

        public async Task<Achievement> UpdateAsync(Achievement entity)
        {
            //TODO: check of id is valid
            return await _db.UpdateAsync(entity);
        }

        public async Task<Achievement> DeleteAsync(Achievement entity)
        {
            return await _db.DeleteAsync(entity);
        }
    }
}