using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO.Converters;
using Thunderstruck.BLL.Managers;
using Thunderstruck.DOMAIN.Models;
using Thunderstruck.UI.ResponseModels.WeatherModels;

namespace Thunderstruck.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationDataController : ControllerBase
    {
        private readonly LocationDataManager _ldManager = new LocationDataManager();
       
        private readonly IOptions<JsonOptions> _jsonOptions;
        //geolocation
        private readonly NtsGeometryServices _geometryServices = new NtsGeometryServices();

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById([FromQuery(Name = "id")] int id)
        {
            try
            {
                var dbLocationData = await _ldManager.GetByIdAsync(id);
                return Ok(new JsonResult(dbLocationData));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("Get")]
        public async Task<IActionResult> Get([FromQuery(Name = "skip")] int skip, [FromQuery(Name = "take")] int take)
        {
            try
            {
                if (take == 0)
                {
                    take = 1;
                }

                IEnumerable<LocationData> dbLocationsData = await _ldManager.GetAsync(skip, take);
                return Ok(new JsonResult(dbLocationsData));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] LocationDataWithDouble locationData)
        {
            // (A spatial reference identifier (SRID) is a unique identifier associated with a specific coordinate system, tolerance, and resolution.)
            // A Point datatype needs an SRID to work, the most common is 4326
            // If this isn't included, the data will not be correctly added to the database or not be added at all.

            var factory =_geometryServices.CreateGeometryFactory(srid: 4326);

            try
            {
                if (locationData is null)
                {
                    //return user = new User()
                    //{
                    //    Tex = new ThunderstruckException("No user object found.", ExceptionTypes.Fatal)
                    //};
                    throw new NullReferenceException();
                }

                var point = factory.CreatePoint(new Coordinate(locationData.XLongitude, locationData.YLatitude));
              
                var mappedLocationData = new LocationData
                {
                    LocationName = locationData.LocationName,
                    TimeStamp = locationData.TimeStamp,
                    Location = point
                };

                
                var dbLocationData = await _ldManager.CreateAsync(mappedLocationData);
                return Ok(new JsonResult(dbLocationData));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //TODO: check if update is needed in project
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] LocationData locationData)
        {
            try
            {
                if (locationData is null)
                {
                    throw new NullReferenceException();
                }

                var dbLocationData = await _ldManager.UpdateAsync(locationData);
                return Ok(new JsonResult(dbLocationData));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //TODO: check if delete is needed in project
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromBody] LocationData locationData)
        {
            try
            {
                if (locationData is null)
                {
                    throw new NullReferenceException();
                }
                var dbLocationData = await _ldManager.DeleteAsync(locationData);
                return Ok(new JsonResult(dbLocationData));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
