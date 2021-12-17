using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Thunderstruck.DOMAIN.Contracts;
using Thunderstruck.DOMAIN.Models;

namespace Thunderstruck.DAL.DBs
{
    public class LocationDataDb:ILocationData
    {
        private readonly ThunderstruckContext _context = new ThunderstruckContext();
        public async Task<LocationData> GetByIdAsync(int id)
        {
            return await _context.LocationData.AsNoTracking()
                .Include(x => x.UserLocationsData)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<LocationData>> GetAsync(int skip, int take)
        {
            return await _context.LocationData.AsNoTracking()
                .OrderBy(x => x.Id)
                .Skip(skip).Take(take)
                .ToListAsync();
        }

        public async Task<LocationData> CreateAsync(LocationData entity)
        {
            _context.LocationData.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<LocationData> UpdateAsync(LocationData entity)
        {
            _context.LocationData.Attach(entity);
            _context.Entry<LocationData>(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<LocationData> DeleteAsync(LocationData entity)
        {
            _context.LocationData.Remove(
                _context.LocationData.Single(x => x.Id == entity.Id));
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}