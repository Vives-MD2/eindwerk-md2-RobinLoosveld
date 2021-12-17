using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Thunderstruck.DOMAIN.Contracts;
using Thunderstruck.DOMAIN.Models;

namespace Thunderstruck.DAL.DBs
{
    public class AchievementDb:IAchievement
    {
        private readonly ThunderstruckContext _context = new ThunderstruckContext();
        public async Task<Achievement> GetByIdAsync(int id)
        {
            return await _context.Achievement.AsNoTracking()
                .Include(x => x.UserAchievements)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Achievement>> GetAsync(int skip, int take)
        {
            return await _context.Achievement.AsNoTracking()
                .OrderBy(x => x.Id)
                .Skip(skip).Take(take)
                .ToListAsync();
        }

        public async Task<Achievement> CreateAsync(Achievement entity)
        {
            _context.Achievement.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Achievement> UpdateAsync(Achievement entity)
        {
            _context.Achievement.Attach(entity);
            _context.Entry<Achievement>(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<Achievement> DeleteAsync(Achievement entity)
        {
            _context.Achievement.Remove(
                _context.Achievement.Single(x => x.Id == entity.Id));
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}