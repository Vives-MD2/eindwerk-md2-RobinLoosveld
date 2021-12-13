using System.Collections.Generic;
using System.Threading.Tasks;
using Thunderstruck.DOMAIN.Contracts;
using Thunderstruck.DOMAIN.Models;

namespace Thunderstruck.DAL.DBs
{
    public class LocationDataDb:ILocationData
    {
        private readonly ThunderstruckContext _context = new ThunderstruckContext();
        public Task<LocationData> GetByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<LocationData>> GetAsync(int skip, int take)
        {
            throw new System.NotImplementedException();
        }

        public Task<LocationData> CreateAsync(LocationData entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<LocationData> UpdateAsync(LocationData entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<LocationData> DeleteAsync(LocationData entity)
        {
            throw new System.NotImplementedException();
        }
    }
}