using System.Collections.Generic;
using System.Threading.Tasks;
using Thunderstruck.DAL.DBs;
using Thunderstruck.DOMAIN.Contracts;
using Thunderstruck.DOMAIN.Helpers;
using Thunderstruck.DOMAIN.Models;

namespace Thunderstruck.BLL.Managers
{
    public class LocationDataManager :ILocationData
    {
        private readonly LocationDataDb _db = new LocationDataDb();
        public async Task<LocationData> GetByIdAsync(int id)
        {
            if (id <= 0)
            {
                return new LocationData()
                {
                    Tex = new ThunderstruckException("LocationData Id is lower than 1, unusable.", ExceptionTypes.Warning)
                };
            }
            var locationData = await _db.GetByIdAsync(id);
            return locationData ?? new LocationData()
            {
                Tex = new ThunderstruckException("No LocationData found.", ExceptionTypes.Warning)
            };
        }

        public async Task<IEnumerable<LocationData>> GetAsync(int skip, int take)
        {
            return await _db.GetAsync(skip, take);
        }

        public async Task<LocationData> CreateAsync(LocationData entity)
        {
            //TODO: check of location is valid
            return await _db.CreateAsync(entity);
        }

        public async Task<LocationData> UpdateAsync(LocationData entity)
        {
            //TODO: check of location is valid
            return await _db.UpdateAsync(entity);
        }

        public async Task<LocationData> DeleteAsync(LocationData entity)
        {
            return await _db.DeleteAsync(entity);
        }
        //TODO: Add method to validate location
    }
}