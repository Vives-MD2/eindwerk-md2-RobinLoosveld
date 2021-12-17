using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Thunderstruck.DOMAIN.Contracts;
using Thunderstruck.DOMAIN.Models;

namespace Thunderstruck.DAL.DBs
{
    public class UserLocationDataDb:IUserLocationData
    {
        private readonly ThunderstruckContext _context = new ThunderstruckContext();
        public Task<UserLocationData> GetByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<UserLocationData>> GetAsync(int skip, int take)
        {
            return await _context.UsersLocationData.AsNoTracking()
                .OrderBy(x => x.UserId)
                .Skip(skip).Take(take)
                .ToListAsync();
        }

        public async Task<UserLocationData> CreateAsync(UserLocationData entity)
        {
            _context.UsersLocationData.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<UserLocationData> UpdateAsync(UserLocationData entity)
        {
            _context.UsersLocationData.Attach(entity);
            _context.Entry<UserLocationData>(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<UserLocationData> DeleteAsync(UserLocationData entity)
        {
            _context.UsersLocationData.Remove(
                _context.UsersLocationData.Single(x => x.UserId == entity.UserId));
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<UserLocationData> GetByIdAsync(int userId, int locationDataId)
        {
            return await _context.UsersLocationData.AsNoTracking()
                .SingleOrDefaultAsync(x => x.UserId == userId && x.LocationDataId == locationDataId);
        }
    }
}