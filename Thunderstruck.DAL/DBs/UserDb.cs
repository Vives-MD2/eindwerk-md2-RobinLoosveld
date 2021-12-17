using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Thunderstruck.DOMAIN.Contracts;
using Thunderstruck.DOMAIN.Models;

namespace Thunderstruck.DAL.DBs
{
    public class UserDb:IUser
    {
        private readonly ThunderstruckContext _context = new ThunderstruckContext();

        public async Task<User> GetByIdAsync(int id)
        {
            //NoTracking info: https://docs.microsoft.com/en-us/ef/core/querying/tracking
            //Tracking behavior controls if Entity Framework Core will keep information about an entity instance in its change tracker.
            //If an entity is tracked, any changes detected in the entity will be persisted to the database during SaveChanges().
            //EF Core will also fix up navigation properties between the entities in a tracking query result and the entities that are in the change tracker.
            return await _context.User.AsNoTracking()
                .Include(s => s.UserAchievements)
                .Include(x=> x.UserLocationsData)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<User>> GetAsync(int skip, int take)
        {
            return await _context.User.AsNoTracking()
                .OrderBy(x => x.Id)
                .Skip(skip).Take(take)
                .ToListAsync();
        }

        public async Task<User> CreateAsync(User entity)
        {
            _context.User.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<User> UpdateAsync(User entity)
        {
            _context.User.Attach(entity);
            _context.Entry<User>(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<User> DeleteAsync(User entity)
        {
            _context.User.Remove(
                _context.User.Single(x => x.Id == entity.Id));
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}