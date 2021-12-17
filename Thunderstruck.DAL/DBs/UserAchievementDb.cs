using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Thunderstruck.DOMAIN.Contracts;
using Thunderstruck.DOMAIN.Models;

namespace Thunderstruck.DAL.DBs
{
    public class UserAchievementDb:IUserAchievement
    {
        private readonly ThunderstruckContext _context = new ThunderstruckContext();
        public async Task<UserAchievement> GetByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<UserAchievement>> GetAsync(int skip, int take)
        {
            return await _context.UserAchievement.AsNoTracking()
                .OrderBy(x => x.Id)
                .Skip(skip).Take(take)
                .ToListAsync();
        }

        public async Task<UserAchievement> CreateAsync(UserAchievement entity)
        {
            _context.UserAchievement.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<UserAchievement> UpdateAsync(UserAchievement entity)
        {
            _context.UserAchievement.Attach(entity);
            _context.Entry<UserAchievement>(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<UserAchievement> DeleteAsync(UserAchievement entity)
        {
            _context.UserAchievement.Remove(
                _context.UserAchievement.Single(x => x.Id == entity.Id));
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<UserAchievement> GetByIdAsync(int userId, int achievementId)
        { 
            return await _context.UserAchievement.AsNoTracking()
                .SingleOrDefaultAsync(x => x.UserId == userId && x.AchievementId == achievementId);
        }
    }
}